using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreOpen : MonoBehaviour
{
    SceneLoader sl;
    void Start()
    {
        sl = FindAnyObjectByType<SceneLoader>();
        StartCoroutine(sl.LoadScene(1));
    }
}
