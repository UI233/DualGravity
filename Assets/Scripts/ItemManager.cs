using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ItemManager : MonoBehaviour
{
    // todo: how to randomlly respawn the item
    protected Queue<GameObject> objectsPool;
    protected List<GameObject> activePool;
    public float rClean;
    public float rMin, rMax;
    public int maxCount;
    public float spawnSpeed;
    public GameObject[] _prefab;
    public GameObject player;
    private float spawnNum;
    public float depth;
    IEnumerator CleanItem()
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
    // Start is called before the first frame update
    protected void Start()
    {
        objectsPool = new Queue<GameObject>();
        activePool = new List<GameObject>();
        StartCoroutine(CleanItem());
        spawnNum = 0.0f;
    }
    virtual protected void SpawnObject(GameObject item, Vector3 initPos)
    {
        item.SetActive(true);
        initPos.z = depth;
        item.transform.position = initPos;
    }
    protected Vector2 GetRandomPosition()
    {
        float radius = Random.Range(rMin, rMax);
        float theta = Random.Range(0.0f, Mathf.PI * 2.0f);
        Vector2 pos = radius * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta))
                    + new Vector2(player.transform.position.x, player.transform.position.y);
        return pos;
    }
    protected void DisableObject(GameObject item)
    {
        item.SetActive(false);
        if (objectsPool.Count < maxCount)
            objectsPool.Enqueue(item);
        else
            Destroy(item);
    }
    protected bool InstantiateNewItem()
    {

        GameObject item = null;
        Vector2 pos = GetRandomPosition();
        // return from object pool
        if (objectsPool.Count != 0)
            item = objectsPool.Dequeue();
        else
        {
            item = Instantiate<GameObject>(_prefab[Random.Range(0, _prefab.Length)], transform);
            item.GetComponent<Item>().manager = gameObject;
        }

        if (item != null)
        {
            SpawnObject(item, pos);
            activePool.Add(item);
            return true;
        }
        else
            return false;
    }



    // Update is called once per frame
    protected void Update()
    {
        spawnNum += spawnSpeed * Time.deltaTime;
        if (activePool.Count + spawnNum > maxCount)
            spawnNum = maxCount - activePool.Count;
        while (spawnNum >= 1.0f)
        {
            InstantiateNewItem();
            spawnNum -= 1.0f;
        }
    }
    public void Recycle(GameObject item)
    {
        activePool.Remove(item);
        item.SetActive(false);
        if (objectsPool.Count < maxCount)
            objectsPool.Enqueue(item);
        else
            Destroy(item);
    }
}
