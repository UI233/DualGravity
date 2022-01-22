using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrail : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        //transform.rotation = Quaternion.Lerp(player.transform.rotation, transform.rotation, 0.5f);
        if (Mathf.Abs(transform.eulerAngles.z - player.transform.eulerAngles.z) > 5f)
        {
            //if (Mathf.Abs(transform.eulerAngles.z - player.transform.eulerAngles.z) > 180f)
            //{
            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 5f);
            //}
            //else
            //{
            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 5f);
            //}
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 5f);
        }
        else
        {
            transform.rotation = player.transform.rotation;
        }
    }
}
