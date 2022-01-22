using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private GameObject[] planets;
    private Rigidbody2D rigid2d;
    private CircleCollider2D collider;
    public PlanetInputAction controls;
    public bool locked;
    public Vector2 myForce { get; set; }
    // energy loss parameters
    public float lossInterval;
    public float lossAmount;
    public float initialEnergy;
    public int bonusBufferSize;
    public float fragileTime;
    // bonus effect
    private int combo;
    private List<int> currentBonus;
    public List<int> targetBonus;
    // player's statistics
    private float currentEnergy;
    private bool fractured;
    private float fragileCountDown;
    private void Awake()
    {
        controls = new PlanetInputAction();
        Application.targetFrameRate = 60;
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

   // Start is called before the first frame update
    void Start()
    {
        // configure input callbacks
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
                anim.enabled = true;
                anim.SetBool("SelfRotation", true);
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
                anim.enabled = true;
                anim.SetBool("SelfRotation", true);
            }
        };
        controls.GravityControl.LockPlanetA.canceled += _ => {
            locked = false;
            anim.SetBool("SelfRotation", false);
            anim.enabled = false;
        };
        controls.GravityControl.LockPlanetB.canceled += _ => {
            locked = false;
            anim.SetBool("SelfRotation", false);
            anim.enabled = false;
        };
        // initialize gameobject components
        planets = GameObject.FindGameObjectsWithTag("Planet");
        rigid2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        // initialize players' states
        combo = 0;
        currentBonus = new List<int>();
        targetBonus = new List<int>();
        // player's statistics
        currentEnergy = initialEnergy;
        fractured = false;
    }
    
    // Update is called once per frame
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
            var vec2 = new Vector2(planet.transform.position.x, planet.transform.position.y);
            vec2 = vec2 +  Mathf.Cos(theta) * dir0 + Mathf.Sin(theta) * dir1;
            transform.position = new Vector3(vec2.x, vec2.y, transform.position.z);
            theta += GetAngularVelocity(theta) * Time.fixedDeltaTime * angularVelocity;
            // transform.eulerAngles = new Vector3(0.0f, 0.0f, selfRotationTheta);
            // selfRotationTheta += Time.fixedDeltaTime * selfRotationVelocity;
        }
    }

    private void Update()
    {
        if (fractured)
        {
            fragileCountDown -= Time.deltaTime;
            if (fragileCountDown < 1e-3f)
                fractured = false;
        }
    }
    // helper functions
    private void GameOver() 
    { 
        transform.position = new Vector3(0.0f, 0.0f, transform.position.z);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        rigid2d.angularVelocity = 0.0f;
        rigid2d.velocity = new Vector2(0.0f, 0.0f);
    }
    // energy loss 
    private IEnumerable EnergyLoss()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(lossInterval);
            currentEnergy -= lossAmount;
        }

    }
    private void EnterFragileMode()
    {
        fractured = true;
        fragileCountDown = fragileTime;
    }
    private void Reward(int combo)
    {

    }

    private void GetBonus()
    {
        bool equals = true;
        for (int i = 0; i < currentBonus.Count; ++i)
            equals &= (currentBonus[i] == targetBonus[i]);
        if (equals)
        {
            // get Bonus according to combo
            Reward(combo);
            ++combo;
        }
        else
        {
            Reward(combo);
            combo = 0;
        }
        currentBonus.Clear();
        for (int i = 0; i < bonusBufferSize; ++i)
            targetBonus[i] = Random.Range(0, 2);
    }
    private void GetItem(int id)
    {
        currentBonus.Add(id);
        if (currentBonus.Count == bonusBufferSize)
            GetBonus();
    }
    private void TakeDamage()
    {
        if (fractured)
        {
            int energyCount = (int)currentEnergy;
            currentEnergy = Mathf.Floor(energyCount);
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
        if (collision.gameObject.tag == "Planet")
        {
            GameOver();
        }

        if (collision.gameObject.tag == "Meteorite")
        {
            TakeDamage();
        }

        if (collision.gameObject.tag == "Item")
        {
            
        }
    }
}
