using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    Animator trans;

    [SerializeField] List<GameObject> pages;
    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;
    [SerializeField] Button closeButton;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject pauseScreen;

    private void Start()
    {
        trans = GetComponent<Animator>();

        closeButton.onClick.AddListener(Close);
        leftArrow.onClick.AddListener(LeftPage);
        rightArrow.onClick.AddListener(RightPage);
    }

    public void OpenTutorial()
    {
        tutorialScreen.SetActive(true);
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[0].SetActive(true);
    }

    private void Update()
    {
        ButtonCheck();
    }

    void Close()
    {
        tutorialScreen.SetActive(false);
        VariableAcrossScene.completeTutorial = true;
        GameManager.Instance.isPausing = pauseScreen.activeSelf? true : false;
    }

    void LeftPage()
    {
        int pageI = -1;
        for(int i = 0; i < pages.Count; i++)
        {
            if (pages[i].activeSelf)
            {
                pageI = i;
            }
        }
        pages[pageI].SetActive(false);
        pages[pageI-1].SetActive(true);
    }

    void RightPage()
    {
        int pageI = -1;
        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i].activeSelf)
            {
                pageI = i;
            }
        }
        pages[pageI].SetActive(false);
        pages[pageI + 1].SetActive(true);
    }

    void ButtonCheck()
    {
        ButtonUI bur = rightArrow.GetComponent<ButtonUI>();
        if (pages[6].activeSelf)
        {
            rightArrow.interactable = false;
            bur.enabled = false;
        }
        else
        {
            rightArrow.interactable = true;
            bur.enabled = true;
        }
        ButtonUI bul = leftArrow.GetComponent<ButtonUI>();
        if (pages[0].activeSelf)
        {
            leftArrow.interactable = false;
            bul.enabled = false;
        }
        else
        {
            leftArrow.interactable = true;
            bul.enabled = true;
        }
    }
}
