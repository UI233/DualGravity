using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlanetInputAction controls;
    private GameObject[] planets;
    private Rigidbody2D rigid2d;
    private CircleCollider2D collider;
    private bool locked;
    private void Awake()
    {
        controls = new PlanetInputAction();
        Application.targetFrameRate = 60;
    }
    private void OnEnable()
    {
        controls.Enable();
        locked = false;
    }

    private float theta;
    private Vector2 dir0;
    private Vector2 dir1;
    private GameObject planet;
    public float angularVelocity = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        controls.GravityControl.LockPlanetA.performed += _ =>
        {
            locked = true;
            planet = planets[0];
            dir0 = transform.position - planets[0].transform.position;
            dir1 = Vector3.Cross(dir0, new Vector3(0, 0, 1));
            theta = 0;
        };
        controls.GravityControl.LockPlanetB.performed += _ =>
        {
            locked = true;
            planet = planets[1];
            dir0 = transform.position - planets[1].transform.position;
            dir1 = Vector3.Cross(dir0, new Vector3(0, 0, 1));
            theta = 0;
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
        }
        else
        {
            var vec2 = new Vector2(planet.transform.position.x, planet.transform.position.y);
            transform.position = vec2 +  Mathf.Cos(theta) * dir0 + Mathf.Sin(theta) * dir1; 
            theta += angularVelocity;
        }

    }

    // todo: clean temporary code
    //private void Update()
    //{
    //    var origin = planets[1].transform.position;
    //    var dir = new Vector3(0.8f * Mathf.Cos(theta), 0.8f * Mathf.Sin(theta), 0.0f);
    //    transform.position = origin + dir;
    //    theta += 0.050f;
    //}

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
