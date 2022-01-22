using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    Transform startText;


    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SolarSystem");
    }

    private void OnMouseOver()
    {
        startText.eulerAngles = new Vector3(startText.eulerAngles.x, startText.eulerAngles.y, startText.eulerAngles.z + 0.01f);
    }
}
