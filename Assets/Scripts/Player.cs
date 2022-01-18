using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject[] planets;
    Rigidbody2D rigid2d;
    // Start is called before the first frame update
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        rigid2d = GetComponent<Rigidbody2D>();
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
    }
}
