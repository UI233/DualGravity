using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlanetInputAction controls;
    private GameObject[] planets;
    private Rigidbody2D rigid2d;
    private CircleCollider2D collider;
    public bool locked;
    public Vector2 myForce { get; set; }

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
            }
        };
        controls.GravityControl.LockPlanetA.canceled += _ => locked = false;
        controls.GravityControl.LockPlanetB.canceled += _ => locked = false;
        planets = GameObject.FindGameObjectsWithTag("Planet");
        rigid2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
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
            theta += GetAngularVelocity(theta) * Time.fixedDeltaTime * angularVelocity;
            transform.position = vec2 +  Mathf.Cos(theta) * dir0 + Mathf.Sin(theta) * dir1; 
        }
    }

    private void GameOver() 
    { 
        transform.position = new Vector3(0.0f, 0.0f, transform.position.z);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        rigid2d.angularVelocity = 0.0f;
        rigid2d.velocity = new Vector2(0.0f, 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            GameOver();
        }
    }
}
