using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    // todo: how to randomlly respawn the meteorite
    Queue<GameObject> objectsPool;
    List<GameObject> activePool;
    public float rClean;
    public float rMin, rMax;
    public int maxCount;
    public float spawnSpeed;
    public GameObject _prefab;
    public GameObject player;
    private float spawnNum;
    // Start is called before the first frame update
    void Start()
    {
        objectsPool = new Queue<GameObject>();
        activePool = new List<GameObject>();
        StartCoroutine("CleanMeteorite");
        spawnNum = 0.0f;
    }
    void SpawnObject(GameObject meteorite, Vector3 initPos)
    {
        meteorite.SetActive(true);
        meteorite.transform.position = initPos;
    }
    void DisableObject(GameObject meteorite)
    {
        meteorite.SetActive(false);
        if (objectsPool.Count < maxCount)
            objectsPool.Enqueue(meteorite);
        else
            Destroy(meteorite);
    }
    bool InstantiateNewMeteorite()
    {
        float radius = Random.Range(rMin, rMax);
        float theta = Random.Range(0.0f, Mathf.PI * 2.0f);
        Vector2 pos = radius * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta))
                    + new Vector2(player.transform.position.x, player.transform.position.y);
        GameObject meteorite = null;
        // return from object pool
        if (objectsPool.Count != 0)
            meteorite = objectsPool.Dequeue();
        else
            meteorite = Instantiate<GameObject>(_prefab);

        if (meteorite != null)
        {
            SpawnObject(meteorite, pos);
            activePool.Add(meteorite);
            return true;
        }
        else
            return false;
    }
    
    IEnumerator CleanMeteorite()
    {
        for (; ; )
        {
            if (activePool.Count >= maxCount)
            {
                foreach (var obj in activePool.FindAll(elem => (elem.transform.position - player.transform.position).magnitude > rClean))
                    DisableObject(obj);
                activePool.RemoveAll(elem => (elem.transform.position - player.transform.position).magnitude > rClean);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        spawnNum += spawnSpeed * Time.deltaTime;
        if (activePool.Count + spawnNum > maxCount)
            spawnNum = maxCount - activePool.Count;
        while (spawnNum >= 1.0f)
        {
            InstantiateNewMeteorite();
            spawnNum -= 1.0f;
        }
    }
}
