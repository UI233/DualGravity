using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMovement : MonoBehaviour
{
    [SerializeField]
    Image energy;
    [SerializeField]
    Image energyBall;
    [SerializeField]
    Sprite BrokenEnergyBall;
    [SerializeField]
    Sprite IntackEnergyBall;
    [SerializeField]
    float speed;

    //int healthLevel;  // 0 -- 10
    [SerializeField]
    int batteryNum;
    [SerializeField]
    GameObject battery1;
    [SerializeField]
    GameObject battery2;
    [SerializeField]
    GameObject battery3;
    [SerializeField]
    GameObject battery4;

    [SerializeField]
    Player player;

    bool lastFractureState;

    private void Start()
    {
        //healthLevel = 10;
        batteryNum = 4;
        lastFractureState = false;
    }

    private void Update()
    {
        UpdateHP();
        UpdateBatteryNum();
        UpdateFracture();
    }

    void UpdateHP()
    {
        // Horizontal
        energy.rectTransform.position = new Vector3(energy.rectTransform.position.x + speed * Time.deltaTime,
            energy.rectTransform.position.y, energy.rectTransform.position.z);
        if (energy.rectTransform.position.x > 284.33f)
        {
            energy.rectTransform.position = new Vector3(-80,
                energy.rectTransform.position.y, energy.rectTransform.position.z);
        }

        // Vertical
        //Debug.Log(energy.rectTransform.position.y);
        energy.rectTransform.position = new Vector3(energy.rectTransform.position.x,
            0 + 100 * (player.currentEnergy - Mathf.FloorToInt(player.currentEnergy)), energy.rectTransform.position.z);
    }

    void UpdateBatteryNum()
    {
        batteryNum = Mathf.FloorToInt(player.currentEnergy - 1e-4f);
        switch (batteryNum)
        {
            case 0:
                battery1.SetActive(false);
                battery2.SetActive(false);
                battery3.SetActive(false);
                battery4.SetActive(false);
                break;
            case 1:
                battery1.SetActive(true);
                battery2.SetActive(false);
                battery3.SetActive(false);
                battery4.SetActive(false);
                break;
            case 2:
                battery1.SetActive(true);
                battery2.SetActive(true);
                battery3.SetActive(false);
                battery4.SetActive(false);
                break;
            case 3:
                battery1.SetActive(true);
                battery2.SetActive(true);
                battery3.SetActive(true);
                battery4.SetActive(false);
                break;
            case 4:
                battery1.SetActive(true);
                battery2.SetActive(true);
                battery3.SetActive(true);
                battery4.SetActive(true);
                break;
            default:
                break;
        }
    }

    void DecreaseHP()
    {

    }

    void IncreaseHP()
    {

    }

    void UpdateFracture()
    {
        //if (lastFractureState)
        //{
        //    energyBall.sprite = BrokenEnergyBall;
        //    energyBall.color = new Color(0.8f, 0, 0);
        //}
        //else
        //{
        //    energyBall.sprite = BrokenEnergyBall;
        //    energyBall.color = new Color(0.8f, 0, 0);
        //}
        if (player.fractured != lastFractureState)
        {
            if (lastFractureState)
            {
                energyBall.sprite = IntackEnergyBall;
                energyBall.color = new Color(1, 1, 1);
            }
            else
            {
                energyBall.sprite = BrokenEnergyBall;
                energyBall.color = new Color(0.8f, 0, 0);
            }
        }

        lastFractureState = player.fractured;
    } 

    //public void SwitchEnergyBall()
    //{
    //    if (energyBall.sprite == IntackEnergyBall)
    //    {
    //        energyBall.sprite = BrokenEnergyBall;
    //        energyBall.color = new Color(0.8f, 0, 0);
    //    }
    //    else
    //    {
    //        energyBall.sprite = IntackEnergyBall;
    //        energyBall.color = new Color(1, 1, 1);
    //    }
    //}
}
