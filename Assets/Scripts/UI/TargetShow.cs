using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetShow : MonoBehaviour
{
    [SerializeField]
    Sprite blueStone;
    [SerializeField]
    Sprite redStone;

    [SerializeField]
    Image[] image;

    [SerializeField]
    Player player;

    private void Update()
    {
        for (int i = 0; i < player.targetBonus.Length; i++)
        {
            if (player.targetBonus[i] == 0) // blue 
            {
                image[i].sprite = blueStone;
            }
            else
            {
                image[i].sprite = redStone;
            }
        }

        if (player.currentBonus.Count == 0)
        {
            for (int i = 0; i < player.targetBonus.Length; i++)
            {
                image[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < player.currentBonus.Count; i++)
            {
                image[i].enabled = false;
            }
        }
    }
}
