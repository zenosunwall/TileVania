using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float deathDelay = 2f;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    int score = 0;

    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int score)
    {
        this.score += score;
        UpdateScoreText();
    }

    private void TakeLife()
    {
        playerLives--;
        UpdateLivesText();
        StartCoroutine(DelayOnDeath());
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private IEnumerator DelayOnDeath()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateLivesText()
    {
        livesText.text = playerLives.ToString();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
