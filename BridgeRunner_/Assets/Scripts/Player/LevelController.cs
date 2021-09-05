using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public bool gameActive;

    int currentLevel;

    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;
    public Text scoreText, finishScoreText, currentLevelText, nextLevelText, startingMenuMoneyText, gameoverMenuMoneyText, finishGameMenuMoneyText;
    public Slider levelProgressBar;
    public float maxDistance;
    public GameObject finishLine;
    public AudioSource gameMusicAudioSource;
    public AudioClip victoryClip, gameOverClip;

    private void Start()
    {
        gameMusicAudioSource = Camera.main.GetComponent<AudioSource>();
        instance = this;
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        if (SceneManager.GetActiveScene().name != "Level " + currentLevel)
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
        else
        {
            currentLevelText.text = (currentLevel + 1).ToString();
            nextLevelText.text = (currentLevel + 2).ToString();
            UpdateMoneyText();
        }
    }
    private void Update()
    {
        if(gameActive)
        {
        PlayerController player = PlayerController.instance;
        float distance = finishLine.transform.position.z - PlayerController.instance.transform.position.z;
        levelProgressBar.value = 1 - (distance / maxDistance);
        }
    }
    public void StartLevel()
    {
        maxDistance = finishLine.transform.position.z - PlayerController.instance.transform.position.z;
        PlayerController.instance.ChangeSpeed(PlayerController.instance.runningSpeed);
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        PlayerAnimatons.instance.myAnimator.SetBool("Running", true);
        gameActive = true;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + (currentLevel + 1));
    }
    public void GameOver()
    {
        UpdateMoneyText();
        gameMusicAudioSource.Stop();
        gameMusicAudioSource.PlayOneShot(gameOverClip);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameActive = false;
    }
    public void FinishGame()
    {
        GiveMoneyToPlayer(PlayerScore.instance.score);
        gameMusicAudioSource.Stop();
        gameMusicAudioSource.PlayOneShot(victoryClip);
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        finishScoreText.text = PlayerScore.instance.score.ToString();
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        gameActive = false;
    }
    public void UpdateMoneyText()
    {
        int money = PlayerPrefs.GetInt("money");
        startingMenuMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        gameoverMenuMoneyText.text = PlayerPrefs.GetInt("money").ToString();
        finishGameMenuMoneyText.text = PlayerPrefs.GetInt("money").ToString();
    }
    public void GiveMoneyToPlayer(int increment)
    {
        int money = PlayerPrefs.GetInt("money");
        money = Mathf.Max(0, money + increment);
        PlayerPrefs.SetInt("money", money);
        UpdateMoneyText();
    }
}
