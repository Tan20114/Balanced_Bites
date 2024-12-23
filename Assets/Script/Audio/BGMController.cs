using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    AudioSource speaker;
    [SerializeField] List<AudioClip> bgm;

    void Start()
    {
        speaker = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        speaker.clip = scene.buildIndex == 2 ? bgm[0] : bgm[1];
        if(!speaker.isPlaying)
        {
            speaker.Play();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
