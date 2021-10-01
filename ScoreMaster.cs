using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMaster : MonoBehaviour
{
    private List<Invader> invaders;

    public TMPro.TextMeshProUGUI curScoreTxt;
    public TMPro.TextMeshProUGUI highScoreTxt;
    public TMPro.TextMeshProUGUI lifeText;

    public static TMPro.TextMeshProUGUI gameOverText;
    public static List<GameObject> playerLives;

    //Referenced in Invader script, OnTriggerEnter2D function
    public static int curScore;
    public static int highScore;

    string highScorePref = "High Score";

    void Start()
    {
        curScore = 0000;
        highScore = PlayerPrefs.GetInt(highScorePref);
        highScoreTxt.text = highScore.ToString();

        playerLives = new List<GameObject>();
        playerLives.AddRange(GameObject.FindGameObjectsWithTag("Life"));
        gameOverText = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TMPro.TextMeshProUGUI>();

    }


    void Update()
    {
        curScoreTxt.text = curScore.ToString();
        lifeText.text = Player.livesLeft.ToString();

        if (Player._dead)
        {
            if (curScore > highScore)
            {
                WriteHighScore(highScorePref, curScore);
            }
        }
        
    }

   public static IEnumerator EnableGameOverText()
    {
        gameOverText.GetComponent<Text>().enabled = true;
        gameOverText.enabled = true;
        yield return new WaitUntil(() => gameOverText.text == gameOverText.GetComponent<Text>().txt);
        PauseGame();
    }

    public static IEnumerator PauseAndWait()
    {
        PauseGame();
        yield return new WaitForSecondsRealtime(2);
        ResumeGame();
    }

    public static void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;           
        }       
    }

    public static void ResumeGame()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;            
        }       
    }

    void WriteHighScore(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

}
