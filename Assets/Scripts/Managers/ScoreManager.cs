using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("External References")]
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text currentScoreText;

    private int score;

    public int Highscore
    {
        get
        {
            return PlayerPrefs.GetInt("Highscore", 0);
        }

        set
        {
            PlayerPrefs.SetInt("Highscore", value);
            highscoreText.text = value.ToString();
            PlayerPrefs.Save();
        }
    }

    public int Score
    {
        get 
        { 
            return score; 
        }

        set
        {
            if(score != value)
            {
                score = value;
                currentScoreText.text = value.ToString();

                if(score > Highscore)
                {
                    Highscore = score;
                }
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventManager.Instance.onGameStarted.AddListener(Init);
        EventManager.Instance.onPlayerDied.AddListener(ShowFinalScores);
    }

    private void OnDisable()
    {
        EventManager.Instance.onGameStarted.RemoveListener(Init);
        EventManager.Instance.onPlayerDied.RemoveListener(ShowFinalScores);
    }

    private void Init()
    {
        Score = 0;
        score = 0;
        currentScoreText.text = score.ToString();
        highscoreText.text = string.Empty;
    }

    private void ShowFinalScores()
    {
        if(score == Highscore)
        {
            highscoreText.text = "BEST!";
        }
        else
        {
            highscoreText.text = Highscore.ToString();
        }
    }
}
