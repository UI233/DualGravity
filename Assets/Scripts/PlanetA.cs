using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetA : Planet
{
    protected new void Awake()
    {
        base.Awake();
        controls.GravityControl.PlanetA.performed +=
            change =>
            {
                gm = change.ReadValue<float>();
            };
        controls.GravityControl.PlanetA.canceled += _ => gm = 0.0f;
    }
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().lockAbleA = true;
            SetNear(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().lockAbleA = false;
            SetNear(false);
        }
    }
}
