using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangingMenu : MonoBehaviour
{
    SceneLoader sl;

    [SerializeField] Button startGame;
    [SerializeField] Button leaderBoard;
    [SerializeField] Button exitButton;

    private void Start()
    {
        sl = FindAnyObjectByType<SceneLoader>();
        startGame.onClick.AddListener(StartGame);
        leaderBoard.onClick.AddListener(ToBoard);
        exitButton.onClick.AddListener(ByeBye);
    }

    void StartGame()
    {
        StartCoroutine(sl.LoadScene(2));
    }

    void ToBoard()
    {
        StartCoroutine(sl.LoadScene(3));
    }

    void ByeBye()
    {
        VariableAcrossScene.SaveData();
        StartCoroutine(sl.ExitGame());
        Debug.Log("Quit");
    }
}
