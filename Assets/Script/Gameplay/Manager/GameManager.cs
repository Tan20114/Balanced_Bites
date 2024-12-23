using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct PlayerInfo
{
    public string name;
    public float score;
    public string timeScore;
}

public class GameManager : Singleton<GameManager>
{
    #region Reference
    ScoreSystem scoreSystem;
    public Player player;
    UIUpdate uu;
    SceneLoader sl;
    TutorialScript ts;

    public Dictionary<CandyType, List<GameObject>> candyStorage = new Dictionary<CandyType, List<GameObject>>();
    public Dictionary<HealthyType, List<GameObject>> healthyStorage = new Dictionary<HealthyType, List<GameObject>>();
    #endregion

    #region Player
    [Header("Player")]
    public GameObject playerObj;
    public List<Transform> spawnPointList;
    public Transform respawnPos;
    public string playerName;
    public string playerTime;
    public bool inputIsEmpty = true;
    #endregion

    #region Level
    [Header("Level System")]
    public int level = 1;
    public int levelEXP = 1;
    public int currentEXP = 0;
    #endregion

    #region Healthiness set
    [Header("Healthiness Value Setting")]
    public int minHealthiness;
    public int maxHealthiness;

    public int MaxHealthiness
    {
        get { return maxHealthiness; }
        set
        {
            maxHealthiness = maxHealthiness > 10 ? 10 : value; 
        }
    }
    public int MinHealthiness
    {
        get { return minHealthiness; }
        set
        {
            minHealthiness = minHealthiness < -10 ? -10 : value;
        }
    }
    float elaspedTime = 0;
    [SerializeField] float resetTime;
    #endregion

    #region Player Status
    [Header("Player Statuses")]
    public int healthiness;
    public int Healthiness
    {
        get
        {
            return healthiness;
        }
        set
        {
            if(healthiness > maxHealthiness)
            {
                healthiness = maxHealthiness;
            }
            else if(healthiness < minHealthiness)
            {
                healthiness = -maxHealthiness;
            }
            else
            {
                healthiness = value;
            }
        }
    }
    public float playerMoveSpeed;
    public float PlayerMoveSpeed
    {
        get
        {
            return playerMoveSpeed;
        }
        set
        {
            if(value < 0)
            {
                value = 0;
                playerMoveSpeed = value;
            }
            else if (value > 10)
            {
                value = 10;
                playerMoveSpeed = value;
            }
            else
            {
                playerMoveSpeed = value;
            }
        }
    }
    #endregion

    [Header("Game Stutus")]
    public bool isPausing = false;

    private void Start()
    {
        player = playerObj.GetComponent<Player>();
        scoreSystem = gameObject.GetComponent<ScoreSystem>();
        uu = FindAnyObjectByType<UIUpdate>();
        sl = FindAnyObjectByType<SceneLoader>();
        ts = FindAnyObjectByType<TutorialScript>();

        Physics2D.IgnoreLayerCollision(0, 0);
        StartCoroutine(WaitForTrans());
    }

    private void Update()
    {
        BorderRestrict();
        if(IsGameOverCheck(Healthiness))
        {
            scoreSystem.enabled = false;
            Time.timeScale = 0;
            Restart();
        }
        else
            PauseSys();
        HealthinessReset();
    }

    void BorderRestrict()
    {
        if(playerObj.transform.position.x <= -9)
        {
            playerObj.transform.position = new Vector3 (-9, playerObj.transform.position.y, 0);
        }
        else if(playerObj.transform.position.x >= 9)
        {
            playerObj.transform.position = new Vector3(9, playerObj.transform.position.y, 0);
        }
    }

    public bool IsGameOverCheck(int healthiness)
    {
        return healthiness >= maxHealthiness || healthiness <= minHealthiness;
    }

    public void Restart()
    {
        Scene sceneToLoad = SceneManager.GetActiveScene();

        PlayerInfo info = new PlayerInfo();
        {
            info.name = playerName;
            info.score = scoreSystem.allSecScore;
            info.timeScore = playerTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !inputIsEmpty)
        {
            Time.timeScale = 1;
            VariableAcrossScene.playerInfo.Add(info);
            SceneManager.LoadScene(sceneToLoad.name);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            uu.EmptyCaution();
        }
    }

    public bool IsHighScore()
    {
        return ScoreSystem.Instance.allSecScore > VariableAcrossScene.playerHighScore;
    }

    public void ObjectStore(CandyType candy, GameObject obj)
    {
        if (!candyStorage.ContainsKey(candy))
        {
            candyStorage[candy] = new List<GameObject>();
        }
        candyStorage[candy].Add(obj);
    }

    public void ObjectStore(HealthyType healthy, GameObject obj)
    {
        if (!healthyStorage.ContainsKey(healthy))
        {
            healthyStorage[healthy] = new List<GameObject>();
        }
        healthyStorage[healthy].Add(obj);
    }

    public void HealthinessReset()
    {
        if (maxHealthiness < 10 || minHealthiness > -10)
        {

            elaspedTime += Time.deltaTime;
            Debug.Log((int)elaspedTime + "/" + resetTime);

            if (elaspedTime >= resetTime)
            {
                elaspedTime = 0;
                maxHealthiness++;
                minHealthiness--;
            }

        }
    }

    public void PauseSys()
    {
        if (isPausing)
            Pause();
        else
            Unpause();
    }

    public void Pause()
    {
        scoreSystem.enabled = false;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        scoreSystem.enabled = true;
        Time.timeScale = 1;
    }

    IEnumerator WaitForTrans()
    {
        isPausing = true;
        if (!VariableAcrossScene.completeTutorial)
        {
            ts.OpenTutorial();
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);
            isPausing = false;
        }
    }

    public void ToMainMenu()
    {
        PlayerInfo info = new PlayerInfo();
        {
            info.name = playerName;
            info.score = scoreSystem.allSecScore;
            info.timeScore = playerTime;
        }

        if (!inputIsEmpty)
        {
            Time.timeScale = 1;
            VariableAcrossScene.playerInfo.Add(info);
            StartCoroutine(sl.LoadScene(1));
        }
        else
        {
            uu.EmptyCaution();
        }
    }
}
