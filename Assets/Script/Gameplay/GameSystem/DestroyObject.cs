using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    ObjectSpawner os;

    [SerializeField] int maxObjCount;

    public List<GameObject> destroyed = new List<GameObject>();

    private void Start()
    {
        os = FindAnyObjectByType<ObjectSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SetActive(false);
        destroyed.Add(collision.gameObject);
    }

    private void Update()
    {
        maxObjCount = os.spawnCount;

        if(playerNotCollect())
        {
            GameManager.Instance.maxHealthiness--;
            GameManager.Instance.minHealthiness++;
            foreach(GameObject obj in destroyed)
                Destroy(obj);
            destroyed.Clear();
        }
    }

    bool playerNotCollect()
    {
        return destroyed.Count >= maxObjCount;
    }
}
