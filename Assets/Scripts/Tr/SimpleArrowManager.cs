using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleArrowManager : MonoBehaviour
{
    GameObject[] planets;
    GameObject player;
    [SerializeField]
    GameObject[] arrows;
    // Start is called before the first frame update
    void Start()
    {
        planets = new GameObject[2] { GameObject.Find("PlanetA"), GameObject.Find("PlanetB") };
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        UpdateIntersectionPoint();

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < planets.Length; ++i)
        {
            Gizmos.DrawLine(planets[i].transform.position, player.transform.position);
        }
    }
    private void UpdateIntersectionPoint()
    {
        for (int i = 0; i < planets.Length; ++i)
        {
            var dir = (planets[i].transform.position - player.transform.position).normalized;
            RaycastHit hitInfo;
            Physics.Raycast(player.transform.position, dir, out hitInfo);
            arrows[i].transform.position = hitInfo.point;
        }
    }
}
