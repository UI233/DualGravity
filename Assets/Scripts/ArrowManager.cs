using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    GameObject[] planets;
    GameObject player;
    GameObject[] arrows;
    Canvas canvas;
    Camera mainCamera;
    RectTransform canvasRect;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canvas = GetComponentInParent<Canvas>();
        arrows = new GameObject[2] { GameObject.Find("ArrowA"), GameObject.Find("ArrowB") };
        planets = new GameObject[2] { GameObject.Find("PlanetA"), GameObject.Find("PlanetB") };
        player = GameObject.FindGameObjectWithTag("Player");
        canvasRect = canvas.GetComponent<RectTransform>();
    }
    private float lineIntersectionWithBox(Vector2 line, Vector2 box)
    {
        const float eps = 1e-2f;
        if (box.x != 0.0f)
        {
            if (Mathf.Abs(line.x) > eps)
            {
                float t = box.x / line.x;
                return t;
            }
            else
                return -2.0f;
        }
        else if (box.y != 0.0f)
        {
            if (Mathf.Abs(line.y) > eps)
            {
                float t = box.y / line.y;
                return t;
            }
            else
                return -2.0f;
        }
        else 
            return -2.0f;
    }

    bool isInBox(Vector2 pos)
    {
        if (pos.x >= -1.0f && pos.x <= 1.0f && pos.y >= -1.0f && pos.y <= 1.0f)
            return true;
        return false;
    }
    private Vector2 lineIntersectionWithBounding(Vector2 line, out int index)
    {
        float t0 = lineIntersectionWithBox(line, new Vector2(0.0f, 1.0f));
        float t1 = lineIntersectionWithBox(line, new Vector2(0.0f, -1.0f));
        float t2 = lineIntersectionWithBox(line, new Vector2(1.0f, 0.0f));
        float t3 = lineIntersectionWithBox(line, new Vector2(-1.0f, 0.0f));
        if (t0 > 0.0f && isInBox(t0 * line))
        {
            index = 0;
            return t0 * line;
        }
        else if (t1 > 0.0f && isInBox(t1 * line))
        {
            index = 1;
            return t1 * line;
        }
        else if (t2 > 0.0f && isInBox(t2 * line))
        {
            index = 2;
            return t2 * line;
        }
        else if (t3 > 0.0f && isInBox(t3 * line))
        {
            index = 3;
            return t3 * line;
        }
        index = -1;
        return new Vector2(0.0f, 0.0f);
    }
    // Check whether the planet is in camera.
    bool isInView(GameObject obj)
    {
        var view = Camera.main.WorldToViewportPoint(obj.transform.position);
        if (view.x > 0.0f && view.x <= 1.0f && view.y > 0.0f && view.y <= 1.0f)
            return true;
        else
            return false;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < planets.Length; ++i)
        {
            if (!isInView(planets[i]))
            {
                arrows[i].SetActive(true);
                float width = canvas.GetComponent<RectTransform>().rect.width;
                float height = canvas.GetComponent<RectTransform>().rect.height;
                var dir = (planets[i].transform.position - player.transform.position).normalized;
                int index = -1;
                var posInRect = lineIntersectionWithBounding(dir, out index);

                if (posInRect.sqrMagnitude != 0.0f)
                {
                    var rect = arrows[i].GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(posInRect.x * width / 2, posInRect.y * height / 2);
                    if (index == 0)
                    {
                        rect.eulerAngles = new Vector3(0, 0, 90);
                    }
                    else if (index == 1)
                    {
                        rect.eulerAngles = new Vector3(0, 0, -90);
                    }
                    else if (index == 2)
                    {
                        rect.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (index == 3)
                    {
                        rect.eulerAngles = new Vector3(0, 0, 180);
                    }
                }
            }
            else
                arrows[i].SetActive(false);
        }
        
    }
}
