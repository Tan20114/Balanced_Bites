using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CandyType
{
    None, Chocolate, HardCandy, Caramel, Lollipop, Sugar, GummyBear
}

public class CandyManager : Singleton<CandyManager>
{
    public List<GameObject> candyPrefab;

    public Transform lollipopPivot;

    private void Start()
    {
        RandomLollipopPivot();

        //Instantiate(candyPrefab[4], lollipopPivot,false);
    }

    public Transform RandomLollipopPivot()
    {
        int randomValue = Random.Range(0, 2);

        switch (randomValue)
        {
            case 0:
                {
                    lollipopPivot = GameObject.FindWithTag("LeftPivot").transform;
                    break;
                }
            case 1:
                {
                    lollipopPivot = GameObject.FindWithTag("RightPivot").transform;
                    break;
                }
            default:
                {
                    break;
                }
        }

        return lollipopPivot;
    }

    public GameObject RandomCandyToSpawn()
    {
        float index = Random.Range(0, 101);

        GameObject prefab = null;

        if (index < 40)
        {
            prefab = candyPrefab[0];
        }
        else if (index < 60)
        {
            prefab = candyPrefab[1];
        }
        else if (index < 80)
        {
            prefab = candyPrefab[2];
        }
        else if (index < 95)
        {
            prefab = candyPrefab[3];
        }
        else if (index < 99)
        {
            prefab = candyPrefab[4];
        }
        else if (index <= 100)
        {
            prefab = candyPrefab[5];
        }

        return prefab;
    }

}
