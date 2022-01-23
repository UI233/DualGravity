using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SolarSystem");
    }
}
