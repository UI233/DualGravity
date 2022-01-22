using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : MonoBehaviour
{
    Collider2D collider;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Disapear()
    {
        manager.GetComponent<ItemManager>().Recycle(gameObject);
    }
}
