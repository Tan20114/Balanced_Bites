using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoardSys : MonoBehaviour
{
    SceneLoader sl;

    [SerializeField] Button closeButt;

    [SerializeField] GameObject playerTrack;
    [SerializeField] RectTransform content;
    [SerializeField] List<GameObject> playerList;
    [SerializeField] Vector2 anchPos;
    int index = 0;

    private void Start()
    {
        sl = FindAnyObjectByType<SceneLoader>();
        closeButt.onClick.AddListener(ToMain);
        QuickSort(VariableAcrossScene.playerInfo, 0, VariableAcrossScene.playerInfo.Count - 1);
        LeaderBoardList();
    }

    void Update()
    {
        float maxHeight = playerList.Count * 150f;

        content.sizeDelta = new Vector2(content.sizeDelta.x, maxHeight);
    }

    GameObject SpawnPlayerTrack(Vector2 pos, RectTransform parent)
    {
        GameObject obj = Instantiate(playerTrack, parent);
        RectTransform rct = obj.GetComponent<RectTransform>();

        rct.anchoredPosition = pos;
        rct.localScale = Vector2.one;

        return obj;
    }

    void LeaderBoardList()
    {
        if(VariableAcrossScene.playerInfo.Count > 0)
        {
            foreach (PlayerInfo pi in VariableAcrossScene.playerInfo)
            {
                GameObject playerTrack = SpawnPlayerTrack(anchPos, content);
                playerList.Add(playerTrack);
                anchPos.y -= 150;

                playerList[index].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (index + 1).ToString();
                playerList[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = pi.name;
                playerList[index].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = pi.timeScore;
                index++;
            }
        }
    }
    #region Sort
    public static void QuickSort(List<PlayerInfo> playerList, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(playerList, low, high);
    
            QuickSort(playerList, low, pi - 1);
            QuickSort(playerList, pi + 1, high);
        }
    }

    private static int Partition(List<PlayerInfo> playerList, int low, int high)
    {
        PlayerInfo pivot = playerList[high];
        int i = (low - 1); 
        for (int j = low; j < high; j++)
        {
            if (playerList[j].score > pivot.score)
            {
                i++;
    
                PlayerInfo temp = playerList[i];
                playerList[i] = playerList[j];
                playerList[j] = temp;
            }
        }

        PlayerInfo temp1 = playerList[i + 1];
        playerList[i + 1] = playerList[high];
        playerList[high] = temp1;
    
        return i + 1;
    }
    #endregion
    void ToMain()
    {
        StartCoroutine(sl.LoadScene(1));
    }
}
