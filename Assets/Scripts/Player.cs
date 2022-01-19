using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject[] planets;
    Rigidbody2D rigid2d;
    CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        rigid2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 totalForce = new Vector2(0.0f, 0.0f);
        foreach (var planet in planets)
        {
            var force = planet.GetComponent<Planet>().GetGravity(transform.position);
            totalForce += force;
        }
        rigid2d.AddForce(totalForce, ForceMode2D.Force);
        if (rigid2d.velocity.magnitude > 4.0f)
            rigid2d.velocity = rigid2d.velocity.normalized * 4.0f;
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
