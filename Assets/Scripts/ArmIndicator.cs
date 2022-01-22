using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmIndicator : MonoBehaviour
{
    Player player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var dir = player.myForce.normalized;
        float angle = Mathf.Atan2(-dir.x, dir.y) * 360.0f / (2.0f * Mathf.PI);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -0.44f);
    }
}
