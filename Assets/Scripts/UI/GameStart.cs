using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    private void OnMouseDown()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SolarSystem");
    }

    //private void OnMouseOver()
    //{
    //    if ((int)Time.time % 2 == 0)
    //    {
    //        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 0.1f);
    //    }
    //    else
    //    {
    //        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 0.1f);
    //    }
    //}
}
