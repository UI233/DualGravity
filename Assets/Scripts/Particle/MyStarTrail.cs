using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStarTrail : MonoBehaviour
{
    [SerializeField]
    GameObject[] stars;

    [SerializeField]
    GameObject player;
    Rigidbody2D plyaerRb;

    [SerializeField]
    float spawnTime = 0.5f;

    float timer;

    private void Start()
    {
        timer = Time.time;
        plyaerRb = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 根据速度计算spawnTime
        if(plyaerRb.velocity.magnitude > 2)
        {
            spawnTime = 1 / plyaerRb.velocity.magnitude;
        }
        else
        {
            spawnTime = 0.5f;
        }

        if (Time.time - timer > spawnTime)
        {
            GameObject g = Instantiate(stars[Random.Range(0, 6)], transform);
            g.transform.position = player.transform.position;
            timer = Time.time;
        }
    }
}
