using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combos : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    TextMesh combo;
    private void Update()
    {
        combo.text = player.score.ToString();
    }
}
