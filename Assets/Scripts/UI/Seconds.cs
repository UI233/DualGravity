using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seconds : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    TextMesh seconds;
    private void Update()
    {
        seconds.text = ((int)(Time.time - player.startTime)).ToString();
    }
}
