
using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public GameObject player;
    public GameObject LoadCanvas;
    public List<GameObject> levels;
    public TMP_Text holdSpaceText; 
    [SerializeField] private float blinkSpeed = 3f;
    private int currentLevelIndex = 0;

    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    public static event Action OnReset;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        progressAmount = 0;
        progressSlider.value = 0;
        Gems.OnGemCollect += IncreaseProgressAmount;
        HoldToLoadLevel.OnHoldComplete += LoadNextLevel;
        PlayerHealth.OnPlayedDied += GameOverScreen;
        LoadCanvas.SetActive(false);
        gameOverScreen.SetActive(false);
        victoryScreen.SetActive(false);
        holdSpaceText.gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        Gems.OnGemCollect -= IncreaseProgressAmount;
        HoldToLoadLevel.OnHoldComplete -= LoadNextLevel;
        PlayerHealth.OnPlayedDied -= GameOverScreen;
    }

    void Update()
    {
      
        if (LoadCanvas.activeSelf && holdSpaceText != null)
        {
            float alpha = Mathf.Lerp(0.2f, 1f, (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f);
            Color color = holdSpaceText.color;
            color.a = alpha;
            holdSpaceText.color = color;
        }
    }
    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        MusicManager.PauseBackgroundMusic();
        Time.timeScale = 0;

    }

    void VictoryScreen()
    {
        victoryScreen.SetActive(true);
        holdSpaceText.gameObject.SetActive(false);
        MusicManager.PauseBackgroundMusic();
        Time.timeScale = 0;
    }


    public void ResetGame()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        victoryScreen.SetActive(false);
        MusicManager.PlayBackgroundMusic(true);
        OnReset?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        


    }

    public void LoadStartMenu()
    {
        Time.timeScale = 1f;
        victoryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        MusicManager.PlayBackgroundMusic(true);
        SceneManager.LoadScene("Start Scene");
    }
    void IncreaseProgressAmount(int amount){
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if(progressAmount >= 100)
        {
            LoadCanvas.SetActive(true);
            holdSpaceText.gameObject.SetActive(true);
        }

    }
    void LoadLevel(int level)
    {
        LoadCanvas.SetActive(false);
        holdSpaceText.gameObject.SetActive(false);

        levels[currentLevelIndex].gameObject.SetActive(false);
        levels[level].gameObject.SetActive(true);

        player.transform.position = new Vector3(0, 0, 0);

        currentLevelIndex = level;
        progressAmount = 0;
        progressSlider.value = 0;
    }

    // Update is called once per frame
    void LoadNextLevel()
    {
        if (currentLevelIndex == levels.Count - 1)
        {
            LoadCanvas.SetActive(false);
            VictoryScreen();
        }
        else
        {
            LoadLevel(currentLevelIndex+1);
        }
       
    }
}
