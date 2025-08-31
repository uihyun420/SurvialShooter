using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting.Dependencies.Sqlite;

public class Ui : MonoBehaviour
{
    public bool isGameOver = false;
    public int score = 0;

    public TMP_Text scoreText;

    public GameObject pause;
    public GameObject gameOverUi;
    public GameObject hitPanel;

    public void ScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void OnEnable()
    {
        score = 0;
    }
    private void Awake()
    {
        isGameOver = false;
        pause.SetActive(false);
        ResetScore();
        ScoreText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isGameOver = !isGameOver;
            pause.SetActive(isGameOver);
            if (isGameOver)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }

      
    }
    public void AddScore(int amount)
    {
        if(!isGameOver)
        {
            score+= amount;
            Debug.Log("10Á¡ È¹µæ");
            ScoreText();
        }
    }

    public void ResetScore()
    {
        score = 0;
        ScoreText();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        isGameOver = false;
        pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverUi.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(Restart(3f));
    }

    private IEnumerator Restart(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HitEffect()
    {
        StartCoroutine(CoHitEffect(0.1f));
    }

    private IEnumerator CoHitEffect(float delay)
    {
        hitPanel.SetActive(true);
        yield return new WaitForSeconds(delay);
        hitPanel.SetActive(false);
    }
}
