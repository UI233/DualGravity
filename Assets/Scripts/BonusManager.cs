using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : ItemManager
{
    // Start is called before the first frame update
    override protected void SpawnObject(GameObject item, Vector3 initPos)
    {
        item.SetActive(true);
        initPos.z = depth;
        const float threshold = 1.5f;
        bool ok = false;
        int maxTemp = 5;
        while (!ok && maxTemp > 0)
        {
            --maxTemp;
            bool achived = true;
            foreach (var obj in activePool)
            {
                achived &= ((obj.transform.position - initPos).magnitude > threshold);
            }
            if (!achived)
                initPos = GetRandomPosition();
            ok = achived;
        }
        item.transform.position = initPos;
    }
}
