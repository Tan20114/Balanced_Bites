using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableAcrossScene : SingletonPersist<VariableAcrossScene>
{
    #region PlayerData
    public static List<PlayerInfo> playerInfo;

    public static float playerHighScore;

    public static bool completeTutorial = false;
    #endregion

    #region VolumeData
    public static float masterVolume;
    public static float bgmVolume;
    public static float sfxVolume;
    #endregion

    private void Start()
    {
        if(playerInfo == null)
            playerInfo = new List<PlayerInfo>();
        LoadData();
    }

    public static void SaveData()
    {
        SaveSys.Save();
    }

    public static void LoadData()
    {
        Data data = SaveSys.Load();

        if(data != null)
        {
            playerInfo = data.playerInfo;
            playerHighScore = data.highScore;
            completeTutorial = data.completeTutorial;

            /*masterVolume = data.masterVolume;
            bgmVolume = data.bgmVolume;
            sfxVolume = data.sfxVolume;*/
        }
    }

    public static void DeleteData()
    {
        SaveSys.DeleteData();
        playerInfo.Clear();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
