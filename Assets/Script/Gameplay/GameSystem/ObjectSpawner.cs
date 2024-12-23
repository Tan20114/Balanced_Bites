using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    DestroyObject ds;

    [SerializeField] List<Transform> spawnPointList;
    [SerializeField] List<GameObject> lollipopPoint;
    [SerializeField] float spawnTime;
    public int spawnCount;

    private void Start()
    {
        ds = FindAnyObjectByType<DestroyObject>();
        StartCoroutine(SpawnTimer(spawnTime));
    }

    private void Update()
    {
        switch (GameManager.Instance.level)
        {
            case 1:
                {
                    spawnCount = 3;
                    spawnTime = 3f;
                    break;
                }
            case 2:
                {
                    spawnCount = 4;
                    spawnTime = 2.5f;
                    break;
                }
            case 3:
                {
                    spawnCount = 5;
                    spawnTime = 2.5f;
                    break;
                }
            case 4:
                {
                    spawnCount = 5;
                    spawnTime = 2f;
                    break;
                }
            case 5:
                {
                    spawnCount = 6;
                    spawnTime = 2f;
                    break;
                }
            case 6:
                {
                    spawnCount = 6;
                    spawnTime = 2f;
                    break;
                }
        }
    }

    HashSet<Transform> RandomSpawnPoint()
    {
        HashSet<Transform> tmp = new HashSet<Transform>();

        do
        {
            int spawnPointIndex = Random.Range(0, spawnPointList.Count);

            tmp.Add(spawnPointList[spawnPointIndex]);
        }
        while (tmp.Count < spawnCount);

        return tmp;
    }

    int RandomSpawnType()
    {
        return Random.Range(0, 2);
    }

    [SerializeField] HashSet<GameObject> objsToSpawn = new HashSet<GameObject>();
    private IEnumerator SpawnTimer(float countDown)
    {
        foreach (GameObject item in ds.destroyed)
            Destroy(item);
        foreach (GameObject point in lollipopPoint)
            point.transform.eulerAngles = new Vector3 (0, 0, 0);
        ds.destroyed.Clear();
        GameObject objToSpawn = null;


        foreach (Transform spawnPoint in RandomSpawnPoint())
        {
            switch(RandomSpawnType())
            {
                case 0:
                    { 
                        objToSpawn = CandyManager.Instance.RandomCandyToSpawn();
                        if(objToSpawn == CandyManager.Instance.candyPrefab[4])
                        {
                            if (!objsToSpawn.Contains(CandyManager.Instance.candyPrefab[4]))
                                objsToSpawn.Add(objToSpawn);
                        }
                        else
                            objsToSpawn.Add(objToSpawn);
                        break;
                    }
                case 1:
                    {
                        objToSpawn = HealthyManager.Instance.RandomHealthyToSpawn();
                        objsToSpawn.Add(objToSpawn);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            foreach (GameObject obj in objsToSpawn)
            {
                if (objToSpawn != CandyManager.Instance.candyPrefab[4])
                    Instantiate(obj, spawnPoint.position, Quaternion.identity);
                else
                    Instantiate(obj, CandyManager.Instance.RandomLollipopPivot(),false);
            }
            objsToSpawn.Clear();
        }
        yield return new WaitForSeconds(countDown);
        StartCoroutine(SpawnTimer(countDown));
    }
}
