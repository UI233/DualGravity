using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetForceIndicator : MonoBehaviour
{
    Player player;
    RectTransform rect;
    Canvas canvas;
    RectTransform canvasRect;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float eps = 1e-3f;
        Vector2 dir = player.myForce.normalized;
        if (dir.sqrMagnitude < eps)
            rect.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        else
        {
            rect.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            rect.anchoredPosition = dir * canvasRect.rect.height * 0.1f;
            float angle = Mathf.Atan2(dir.x, -dir.y) * 360.0f / (2.0f * Mathf.PI);
            rect.eulerAngles = new Vector3(0.0f, 0.0f, angle);
            // todo: modify scale according to force magnitude
            float scale = 0.0f;
        }
    }
}
