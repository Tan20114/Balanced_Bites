using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthyType
{
    None, Vegetable, Youghurt, Lemon, Berry, Nuts, Water
}

public class HealthyManager : Singleton<HealthyManager>
{
    public List<GameObject> healthyPrefab;

    public GameObject RandomHealthyToSpawn()
    {
        float index = Random.Range(0, 101);

        GameObject prefab = null;

        if (index < 40)
        {
            prefab = healthyPrefab[0];
        }
        else if (index < 60)
        {
            prefab = healthyPrefab[1];
        }
        else if (index < 80)
        {
            prefab = healthyPrefab[2];
        }
        else if (index < 95)
        {
            prefab = healthyPrefab[3];
        }
        else if (index < 99)
        {
            prefab = healthyPrefab[4];
        }
        else if (index <= 100)
        {
            prefab = healthyPrefab[5];
        }

        return prefab;
    }
}
