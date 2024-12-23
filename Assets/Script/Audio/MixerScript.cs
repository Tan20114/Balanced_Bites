using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerScript : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("master"))
            LoadMaster();
        else
            SetMaster();

        if (PlayerPrefs.HasKey("bgm"))
            LoadBGM();
        else
            SetBGM();

        if (PlayerPrefs.HasKey("sfx"))
            LoadSFX();
        else
            SetSFX();
    }

    public void SetMaster()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("master",  volume);
        PlayerPrefs.Save();
    }

    void LoadMaster()
    {
        masterSlider.value = PlayerPrefs.GetFloat("master",.5f);
        SetMaster();
    }

    public void SetBGM()
    {
        float volume = bgmSlider.value;
        mixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgm", volume);
        PlayerPrefs.Save();
    }

    void LoadBGM()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgm", .5f);
        SetBGM();
    }

    public void SetSFX()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("sfx", Mathf .Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx", volume );
        PlayerPrefs.Save();
    }

    void LoadSFX()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfx", .5f);
        SetSFX();
    }
}