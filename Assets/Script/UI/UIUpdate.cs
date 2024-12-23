using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    SceneLoader sl;
    ScoreSystem ss;
    TutorialScript ts;

    #region Slider Field
    [Header("Slider Field")]
    [SerializeField] Slider healthinessSlider;
    [SerializeField] Image sliderHandle;
    [SerializeField] List<Image> bg;
    [SerializeField] Image handle;
    [SerializeField] TextMeshProUGUI minTxt;
    [SerializeField] TextMeshProUGUI maxTxt;
    #endregion

    #region GameOver Section
    [Header("GameOver")]
    [SerializeField] Image gameOverScreen;
    [SerializeField] TextMeshProUGUI finalScoreTxt;
    [SerializeField] TextMeshProUGUI newHighScore;
    [SerializeField] TMP_InputField playerInputField;
    [SerializeField] GameObject playerInputCaution;
    [SerializeField] Button toMainButt;
    #endregion

    #region Statistic
    #region Candy Statistic
    [Header("Candy Count")]
    [SerializeField] TextMeshProUGUI chocoTxt;
    [SerializeField] TextMeshProUGUI hardTxt;
    [SerializeField] TextMeshProUGUI caramelTxt;
    [SerializeField] TextMeshProUGUI gummyTxt;
    [SerializeField] TextMeshProUGUI lollipopTxt;
    [SerializeField] TextMeshProUGUI sugarTxt;
    #endregion
    #region Healthy Statistic
    [Header("Healthy Count")]
    [SerializeField] TextMeshProUGUI vegeTxt;
    [SerializeField] TextMeshProUGUI youghurtTxt;
    [SerializeField] TextMeshProUGUI berryTxt;
    [SerializeField] TextMeshProUGUI nutTxt;
    [SerializeField] TextMeshProUGUI lemonTxt;
    [SerializeField] TextMeshProUGUI waterTxt;
    #endregion
    [SerializeField] Image statisticScreen;
    [SerializeField] Button statisticOpen;
    [SerializeField] Button statisticClose;
    #endregion

    #region Game Status
    [Header("Game Status")]
    [SerializeField] TextMeshProUGUI HealthinessTxt;
    [SerializeField] TextMeshProUGUI TimerTxt;
    #endregion

    #region Score Timer
    float minute;
    float second;
    #endregion

    #region Pause
    [Header("Pause")]
    [SerializeField] Image pauseScreen;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButt;
    [SerializeField] Button tutorialButt;
    #endregion

    private void Start()
    {
        sl = FindAnyObjectByType<SceneLoader>();
        ss = FindAnyObjectByType<ScoreSystem>();
        ts = FindAnyObjectByType<TutorialScript>();
        healthinessSlider.maxValue = GameManager.Instance.maxHealthiness;
        healthinessSlider.minValue = GameManager.Instance.minHealthiness;

        pauseButton.onClick.AddListener(PauseUI);
        resumeButton.onClick.AddListener(UnpauseUI);
        tutorialButt.onClick.AddListener(TutorialOpen);
        

        statisticOpen.onClick.AddListener(StaticOpen);
        statisticClose.onClick.AddListener(StaticClose);

        mainMenuButt.onClick.AddListener(ToMainMenu);
        toMainButt.onClick.AddListener(GameManager.Instance.ToMainMenu);
    }

    void Update()
    {
        ValueSet();
        GamePlayUI();
        GameOverUI();
        StatisticUpdate();
    }

    void ValueSet()
    {
        minute = ScoreSystem.Instance.minScore;
        second = ScoreSystem.Instance.secScore;
    }

    void GamePlayUI()
    {
        #region Text
        HealthinessTxt.text = string.Concat("Healthiness : ", healthinessSlider.value.ToString());

        int minToShow = ((int)minute);
        int secToShow = ((int)second);
        TimerTxt.text = string.Format("{0:00}:{1:00}", minToShow, secToShow);
        #endregion

        #region Slider
        healthinessSlider.value = GameManager.Instance.Healthiness;
        healthinessSlider.maxValue = GameManager.Instance.MaxHealthiness;
        healthinessSlider.minValue = GameManager.Instance.MinHealthiness;
        minTxt.text = GameManager.Instance.minHealthiness.ToString();
        maxTxt.text = GameManager.Instance.maxHealthiness.ToString();

        #region Color Change
        #region SliderHandle
        if(healthinessSlider.value != 0)
            sliderHandle.color = Color.Lerp(Color.green, Color.red, TCalculator());
        else
            sliderHandle.color = Color.green;
        #endregion
        #region Slider BG
        float t = TCalculator();
        float satValue = Mathf.Lerp(.5f, 1, t);
        float alphaValue = Mathf.Lerp(.5f, 1, t);

        if (healthinessSlider.value > 0)
        {
            bg[1].material.SetFloat("_Saturation", 0);
            bg[1].material.SetFloat("_Alpha", .25f);
            bg[0].material.SetFloat("_Saturation", satValue);
            bg[0].material.SetFloat("_Alpha", alphaValue);
        }
        else if (healthinessSlider.value < 0)
        {
            bg[1].material.SetFloat("_Saturation", satValue);
            bg[1].material.SetFloat("_Alpha", alphaValue);
            bg[0].material.SetFloat("_Saturation", 0);
            bg[0].material.SetFloat("_Alpha", .25f);
        }
        else
        {
            foreach (Image b in bg)
            {
                b.material.SetFloat("_Saturation", 0);
                b.material.SetFloat("_Alpha", .25f);
            }
        }
        #endregion
        #endregion
        #endregion Slider
    }

    void GameOverUI()
    {
        if(GameManager.Instance.IsGameOverCheck(GameManager.Instance.Healthiness))
        {
            gameOverScreen.gameObject.SetActive(true);
            InputEmptyCheck();

            int minToShow = ((int)minute);
            int secToShow = ((int)second);
            finalScoreTxt.text = string.Format("Time : " + "{0:00}:{1:00}", minToShow, secToShow);
            if(GameManager.Instance.IsHighScore())
            {
                VariableAcrossScene.playerHighScore = ScoreSystem.Instance.allSecScore;
                newHighScore.gameObject.SetActive(true);
            }
            TextTransfer();
        }
        else
        {
            gameOverScreen.gameObject.SetActive(false);
            newHighScore.gameObject.SetActive(false);
        }
    }

    float TCalculator()
    {
        return Mathf.Abs((float)GameManager.Instance.Healthiness) / (float)GameManager.Instance.maxHealthiness;
    }

    void InputEmptyCheck()
    {
        GameManager.Instance.inputIsEmpty = string.IsNullOrEmpty(playerInputField.text);
    }

    public void EmptyCaution()
    {
        playerInputCaution.gameObject.SetActive(true);
    }

    void TextTransfer()
    {
        GameManager.Instance.playerName = playerInputField.text;
        int minToShow = ((int)minute);
        int secToShow = ((int)second);
        GameManager.Instance.playerTime = string.Format("{0:00}:{1:00}", minToShow, secToShow);
    }

    void StatisticUpdate()
    {
        #region Candy
        if(GameManager.Instance.candyStorage.ContainsKey(CandyType.Chocolate))
            chocoTxt.text = GameManager.Instance.candyStorage[CandyType.Chocolate].Count.ToString();
        if(GameManager.Instance.candyStorage.ContainsKey(CandyType.HardCandy))
            hardTxt.text = GameManager.Instance.candyStorage[CandyType.HardCandy].Count.ToString();
        if (GameManager.Instance.candyStorage.ContainsKey(CandyType.Caramel))
            caramelTxt.text = GameManager.Instance.candyStorage[CandyType.Caramel].Count.ToString();
        if (GameManager.Instance.candyStorage.ContainsKey(CandyType.GummyBear))
            gummyTxt.text = GameManager.Instance.candyStorage[CandyType.GummyBear].Count.ToString();
        if (GameManager.Instance.candyStorage.ContainsKey(CandyType.Lollipop))
            lollipopTxt.text = GameManager.Instance.candyStorage[CandyType.Lollipop].Count.ToString();
        if (GameManager.Instance.candyStorage.ContainsKey(CandyType.Sugar))
            sugarTxt.text = GameManager.Instance.candyStorage[CandyType.Sugar].Count.ToString();
        #endregion
        #region Healthy
        if (GameManager.Instance.healthyStorage.ContainsKey(HealthyType.Vegetable))
            vegeTxt.text = GameManager.Instance.healthyStorage[HealthyType.Vegetable].Count.ToString();
        if (GameManager.Instance.healthyStorage.ContainsKey(HealthyType.Youghurt))
            youghurtTxt.text = GameManager.Instance.healthyStorage[HealthyType.Youghurt].Count.ToString();
        if (GameManager.Instance.healthyStorage.ContainsKey(HealthyType.Berry))
            berryTxt.text = GameManager.Instance.healthyStorage[HealthyType.Berry].Count.ToString();
        if (GameManager.Instance.healthyStorage.ContainsKey(HealthyType.Nuts))
            nutTxt.text = GameManager.Instance.healthyStorage[HealthyType.Nuts].Count.ToString();
        if (GameManager.Instance.healthyStorage.ContainsKey(HealthyType.Lemon))
            lemonTxt.text = GameManager.Instance.healthyStorage[HealthyType.Lemon].Count.ToString();
        if (GameManager.Instance.healthyStorage.ContainsKey(HealthyType.Water))
            waterTxt.text = GameManager.Instance.healthyStorage[HealthyType.Water].Count.ToString();
        #endregion
    }

    void StaticOpen()
    {
        statisticScreen.gameObject.SetActive(true);
    }

    void StaticClose()
    {
        statisticScreen.gameObject.SetActive(false);
    }

    void PauseUI()
    {
        GameManager.Instance.isPausing = true;
        pauseScreen.gameObject.SetActive(true);
    }

    void UnpauseUI()
    {
        GameManager.Instance.isPausing = false;
        pauseScreen.gameObject.SetActive(false);
    }

    void TutorialOpen()
    {
        ts.OpenTutorial();
    }

    void ToMainMenu()
    {
        StartCoroutine(sl.LoadScene(1));
    }
}
