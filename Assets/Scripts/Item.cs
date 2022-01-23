using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : MonoBehaviour
{
    protected Collider2D collider;
    public GameObject manager;
    // Start is called before the first frame update
    protected void Start()
    {
        collider = GetComponent<Collider2D>();
        // collider.enabled = false ;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
    
    public void Disapear()
    {
        manager.GetComponent<ItemManager>().Recycle(gameObject);
    }
}
