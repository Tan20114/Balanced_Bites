using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    #region PlayerData
    public float highScore;

    public List<PlayerInfo> playerInfo;

    public bool completeTutorial;
    #endregion

    #region VolumeData
    /*public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;*/
    #endregion

    public Data ()
    {
        #region PlayerData
        highScore = VariableAcrossScene.playerHighScore;

        playerInfo = VariableAcrossScene.playerInfo;

        completeTutorial = VariableAcrossScene.completeTutorial;
        #endregion

        #region VolumeData
        /*masterVolume = VariableAcrossScene.masterVolume;
        bgmVolume = VariableAcrossScene.bgmVolume;
        sfxVolume = VariableAcrossScene.sfxVolume;*/
        #endregion
    }
}
