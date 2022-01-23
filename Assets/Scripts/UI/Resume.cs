using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject pausePanel;
    private void OnMouseDown()
    {
        canvas.SetActive(!canvas.activeInHierarchy);
        pausePanel.SetActive(!canvas.activeInHierarchy);
        pausePanel.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
