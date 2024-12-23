using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : Singleton<ScoreSystem>
{
    #region Timer
    public float secScore;
    public float minScore;

    public float allSecScore = 0f;
    #endregion

    private void Update()
    {
        allSecScore += Time.deltaTime;
        secScore += Time.deltaTime;
        if (secScore >= 60)
        {
            secScore = 0;
            minScore++;
            GameManager.Instance.currentEXP++;
        }
        LevelUp();
    }

    private void LevelUp()
    {
        if(GameManager.Instance.currentEXP == GameManager.Instance.levelEXP)
        {
            GameManager.Instance.level++;
            GameManager.Instance.currentEXP = 0;
        }
    }
}
