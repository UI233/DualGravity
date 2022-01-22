using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    float timeStart;
    SpriteRenderer sprite;

    void Start()
    {
        timeStart = Time.time;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(1, 1, 1, (3 - Time.time + timeStart) / 3);
        if(Time.time - timeStart > 3f)
        {
            Destroy(gameObject);
        }
    }
}
