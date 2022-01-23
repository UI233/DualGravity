using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartVideoController : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlayer;
    [SerializeField]
    AudioSource audioSource;

    float timer;

    private void Start()
    {
        timer = Time.time;
    }

    private void Update()
    {
        if (Time.time - timer > videoPlayer.length)
        {
            gameObject.SetActive(false);
            audioSource.Play();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        gameObject.SetActive(false);
        audioSource.Play();
    }

}
