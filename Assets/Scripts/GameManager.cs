using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    int score = 0;
    public Text scoreText;
    public Text highScoreText;
    public Text timerText;
    public GameObject gameOverMenu;
    public Text gameOverText;
    float timer;


    void Start()
    {
        if(PlayerPrefs.HasKey("HIGHSCORE"))
        {
            highScoreText.text = PlayerPrefs.GetInt("HIGHSCORE").ToString();
        }
        timer = 181f;
        Time.timeScale = 0f;
    }

    void Update()
    {
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);
        scoreText.text = score.ToString();
        timerText.text = min.ToString() + ":" + sec.ToString("D2");
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            GameOver();
        }
    }

    public void AddScore(int _points)
    {
        score += _points;
    }

    public void GameOver()
    {
        if(PlayerPrefs.HasKey("HIGHSCORE"))
        {
            if(score > PlayerPrefs.GetInt("HIGHSCORE"))
            {
                SetHighScore();
                gameOverText.text = "New High Score!";
            }
        }
        else
        {
            SetHighScore();
        }
        gameOverMenu.SetActive(true);
        PauseGame();
    }

    void SetHighScore ()
    {
        PlayerPrefs.SetInt("HIGHSCORE", score);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

}
