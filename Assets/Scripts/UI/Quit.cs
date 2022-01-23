using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        Application.Quit();
    }
}
