using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkLine : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform planetATransform;
    [SerializeField]
    Transform planetBTransform;

    [SerializeField]
    LineRenderer redLineRender;
    [SerializeField]
    LineRenderer blueLineRender;

    private void Update()
    {
        redLineRender.SetPosition(0, playerTransform.position);
        blueLineRender.SetPosition(0, playerTransform.position);
        redLineRender.SetPosition(1, planetATransform.position);
        blueLineRender.SetPosition(1, planetBTransform.position);
    }
}
