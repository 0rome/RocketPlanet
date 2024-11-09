using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text currentScoreText;
    public Text bestScoreText;

    private int currentScore;
    private int bestScore;

    private void Start()
    {
        if (PlayerPrefs.HasKey("BestScore") == false)
        {
            PlayerPrefs.SetInt("BestScore",0);
        }
        
    }

    
    public void TouchPlanet()
    {
        currentScore += 1;
        currentScoreText.text = currentScore.ToString();
    }
    public void Death()
    {
        if (currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);

            bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
