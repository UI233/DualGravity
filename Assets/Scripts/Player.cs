using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // my components
    private Animator anim;
    [SerializeField]
    private Animator shellAnim;
    [SerializeField]
    private Animator superComboAnimator;
    private GameObject[] planets;
    private Rigidbody2D rigid2d;
    private CircleCollider2D collider;
    public PlanetInputAction controls;
    public bool locked;
    public GameObject InvincibleShell;
    public GameObject PlayerLight;
    public Vector2 myForce { get; set; }
    // helper component
    public MeteoriteManager manager;
    // energy loss parameters
    public float itemBonus;
    public float lossInterval;
    public float lossAmount;
    public float initialEnergy;
    public int bonusBufferSize;
    public float fragileTime;
    public float energyLimit;
    // reward parameter
    public float[] comboBonus;
    public int maxCombo;
    // bonus effect
    [SerializeField]
    public int combo;
    public List<int> currentBonus;
    public int[] targetBonus;
    // player's statistics
    [SerializeField]
    public float currentEnergy;
    [SerializeField]
    public bool fractured;
    [SerializeField]
    private float fragileCountDown;
    // pause
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject pausePanel;
    // gameover
    [SerializeField]
    GameObject gameOverPanel;
    // score
    public int score;
    public float startTime;
    public float invincibleTime;
    public float flashingTime;
    private float invincibleCountDown;
    private bool invincible;
    private void Awake()
    {
        controls = new PlanetInputAction();
        planets = GameObject.FindGameObjectsWithTag("Planet");
        Application.targetFrameRate = 60;
        startTime = Time.time;
    }
    private void OnEnable()
    {
        controls.Enable();
        locked = false;
        lockAbleA = false;
        lockAbleB = false;
    }
    // control the orbit of the Player
    private float theta;
    private Vector2 dir0;
    private Vector2 dir1;
    private GameObject planet;
    public float angularVelocity;
    public float selfRotationVelocity;
    private float selfRotationTheta;
    public bool lockAbleA;
    public bool lockAbleB;
    private float GetAngularVelocity(float angle)
    {
        angle /= Mathf.PI;
        angle %= 2.0f;
        float velocity;
        if (angle >= 0.0f && angle < 1.0f)
            velocity =  2.0f * Mathf.Sqrt(angle);
        else if (angle < 2.0f)
            velocity = 2.0f * Mathf.Sqrt(2.0f - angle);
        else
            velocity = 0.0f;
        // avoid stucking in the singularity
        const float eps = 1e-4f;
        if (velocity < eps)
            velocity = eps;
        return velocity;
    }
    private void ConfigInput()
    {
        controls.GravityControl.LockPlanetA.performed += _ =>
        {
            if (lockAbleA)
            {
                // initialize locking variables
                locked = true;
                planet = planets[0];
                float radius = (transform.position - planet.transform.position).magnitude;
                dir0 = new Vector3(0.0f, radius, 0.0f);
                dir1 = new Vector3(-radius, 0.0f, 0.0f);
                // todo: make the apex be slowest point 
                Vector3 dir = (transform.position - planet.transform.position).normalized;
                theta = Mathf.Atan2(-dir.x, dir.y) + 2.0f * Mathf.PI;
                // anim.enabled = true;
                anim.SetBool("Lock", true);
                planet.GetComponent<Planet>().SetLock(true);
                selfRotationTheta = 0.0f;
            }
        };
        controls.GravityControl.LockPlanetB.performed += _ =>
        {
            if (lockAbleB)
            {
                locked = true;
                planet = planets[1];
                float radius = (transform.position - planet.transform.position).magnitude;
                dir0 = new Vector3(0.0f, radius, 0.0f);
                dir1 = new Vector3(-radius, 0.0f, 0.0f);
                // todo: make the apex be slowest point 
                Vector3 dir = (transform.position - planet.transform.position).normalized;
                theta = Mathf.Atan2(-dir.x, dir.y) + 2.0f * Mathf.PI;
                // anim.enabled = true;
                anim.SetBool("Lock", true);
                planet.GetComponent<Planet>().SetLock(true);
                selfRotationTheta = 0.0f;
            }
        };
        controls.GravityControl.LockPlanetA.canceled += _ => {
            locked = false;
            anim.SetBool("Lock", false);
            // anim.enabled = false;
            planet.GetComponent<Planet>().SetLock(false);
        };
        controls.GravityControl.LockPlanetB.canceled += _ => {
            locked = false;
            anim.SetBool("Lock", false);
            // anim.enabled = false;
            planet.GetComponent<Planet>().SetLock(false);
        };
        controls.GravityControl.Pause.started += _ => {
            canvas.SetActive(!canvas.activeInHierarchy);
            pausePanel.SetActive(!canvas.activeInHierarchy);
            pausePanel.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        };
    }
    private void GetMyComponents()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        //anim.enabled = false;
    }
    private void InitPlayerState()
    {
        // initialize players' states
        combo = 0;
        score = 0;
        currentBonus = new List<int>();
        targetBonus = new int[bonusBufferSize];
        // player's statistics
        currentEnergy = initialEnergy;
        fractured = false;
        GenerateTargetBonus();
        invincible = false;
        InvincibleShell.SetActive(false);
        PlayerLight.SetActive(true);
        if (maxCombo != comboBonus.Length)
            throw new System.IndexOutOfRangeException("Combo does not match");
        // Energy Loss
    }
   // Start is called before the first frame update
    void Start()
    {
        ConfigInput();
        GetMyComponents();
        InitPlayerState();
        StartCoroutine(EnergyLoss());
    }
    
    void FixedUpdate()
    {
        // this function processes objects' transformation
        if (!locked)
        {
            Vector2 netForce = new Vector2(0.0f, 0.0f);
            foreach (var planet in planets)
            {
                var force = planet.GetComponent<Planet>().GetGravity(transform.position);
                netForce += force;
            }
            rigid2d.AddForce(netForce, ForceMode2D.Force);
            if (rigid2d.velocity.magnitude > 4.0f)
                rigid2d.velocity = rigid2d.velocity.normalized * 4.0f;
            myForce = netForce;
            var dir = rigid2d.velocity.normalized;
            float angle = Mathf.Atan2(-dir.x, dir.y) * 360.0f / (2.0f * Mathf.PI);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
        }
        else
        {
            // self rotation in locked mode
            var vec2 = new Vector2(planet.transform.position.x, planet.transform.position.y);
            vec2 = vec2 +  Mathf.Cos(theta) * dir0 + Mathf.Sin(theta) * dir1;
            transform.position = new Vector3(vec2.x, vec2.y, transform.position.z);
            theta += GetAngularVelocity(theta) * Time.fixedDeltaTime * angularVelocity;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, selfRotationTheta);
            selfRotationTheta += Time.fixedDeltaTime * selfRotationVelocity;
        }
    }

    void DisableInvincible()
    {
        shellAnim.SetBool("Flashing", false);
        InvincibleShell.SetActive(false);
        PlayerLight.SetActive(true);
        invincible = false;
    }

    void EnableInvincible()
    {
        InvincibleShell.SetActive(true);
        PlayerLight.SetActive(false);
        invincible = true;
        invincibleCountDown = invincibleTime;
    }
    // Update is called once per frame
    private void Update()
    {
        // fragile state's recovery
        if (fractured)
        {
            fragileCountDown -= Time.deltaTime;
            if (fragileCountDown < 1e-3f)
            {
                fractured = false;
            }
        }

        if (invincible)
        {
            invincibleCountDown -= Time.deltaTime;
            if (invincibleCountDown < flashingTime)
                shellAnim.SetBool("Flashing", true);
            if (invincibleCountDown < 1e-3f)
                DisableInvincible();
        }

        if (currentEnergy < 1e-4)
            GameOver();
    }

    void GenerateTargetBonus()
    {
        for (int i = 0; i < bonusBufferSize; ++i)
            targetBonus[i] = Random.Range(0, 2);
    }
    private void ResetPlayer()
    {
        transform.position = new Vector3(0.0f, 0.0f, transform.position.z);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        rigid2d.angularVelocity = 0.0f;
        rigid2d.velocity = new Vector2(0.0f, 0.0f);
        InitPlayerState();
    }
    // helper functions
    public void GameOver() 
    {
        Time.timeScale = 0;
        gameOverPanel.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
        gameOverPanel.SetActive(true);
        canvas.SetActive(false);
        // todo: change scene
        AudioManager.instance.AudioPlay(AudioManager.instance.death);
    }
    // energy loss 
    private IEnumerator EnergyLoss()
    {
        for (; ; )
        {
            //Debug.Log("enegy:" + currentEnergy);
            yield return new WaitForSeconds(lossInterval);
            //if (Mathf.FloorToInt(currentEnergy) != Mathf.FloorToInt(currentEnergy - lossAmount))
            //    AudioManager.instance.AudioPlay(AudioManager.instance.vanish);
            currentEnergy -= lossAmount;
        }

    }
    private void GetBonus()
    {
        bool equals = true;
        if (equals)
        {
            currentEnergy = Mathf.Min(comboBonus[combo] + currentEnergy, energyLimit);
            ++combo;
            score += combo;
            anim.SetTrigger("Combo");
            if (combo == maxCombo)
            {
                AudioManager.instance.AudioPlay(AudioManager.instance.combo);
                superComboAnimator.SetTrigger("SuperCombo");
                manager.DestroyAllMeteorites();
                combo = 0;
            }
            else
            {
                AudioManager.instance.AudioPlay(AudioManager.instance.finish_target);
                superComboAnimator.SetTrigger("Combo");
            }

            if (combo == maxCombo - 1)
                EnableInvincible();
        }
        else
            combo = 0;
        currentBonus.Clear();
        GenerateTargetBonus();
    }
    private void GetItem(int id)
    {
        currentBonus.Add(id);
        currentEnergy = Mathf.Min(currentEnergy + itemBonus, energyLimit);
        if (currentBonus[currentBonus.Count - 1] != targetBonus[currentBonus.Count - 1])
        {
            anim.SetTrigger("Dis");
            AudioManager.instance.AudioPlay(AudioManager.instance.eat_wrong);
            combo = 0;
            currentBonus.Clear();
            GenerateTargetBonus();
        }
        else if (currentBonus.Count == bonusBufferSize)
            GetBonus();
        else
        {
            AudioManager.instance.AudioPlay(AudioManager.instance.eat_right);
            anim.SetTrigger("Happy");
        }
    }
    private void TakeDamage()
    {
        if (fractured)
        {
            currentEnergy = Mathf.Floor(currentEnergy);
            fractured = false;
        }
        else
        {
            fragileCountDown = fragileTime;
            fractured = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet" && !invincible)
        {
            collision.gameObject.GetComponent<Planet>().SetCollid();
            anim.SetTrigger("Hit");
            AudioManager.instance.AudioPlay(AudioManager.instance.fracture);
            TakeDamage();
            // ResetPlayer();
        }

        if (collision.gameObject.tag == "Meteorite" && !invincible)
        {
            anim.SetTrigger("Hit");
            AudioManager.instance.AudioPlay(AudioManager.instance.fracture);
            TakeDamage();
            collision.gameObject.GetComponent<Item>().Disapear();
        }

        if (collision.gameObject.tag == "Item")
        {
            GetItem(collision.gameObject.GetComponent<BonusItem>().bonusType);
            collision.gameObject.GetComponent<BonusItem>().Disapear();
        }
    }
}
