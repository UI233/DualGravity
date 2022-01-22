using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : ItemManager
{
    public void DestroyAllMeteorites()
    {
        foreach (var meteorite in activePool)
            DisableObject(meteorite);
        activePool.Clear();
    }
}
