using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    Scene curScene;
    GameObject player;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        curScene = SceneManager.GetActiveScene();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (curScene.name.Equals("StartScreen"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("SpaceInvaders");
            }
        }
        else if (curScene.name.Equals("SpaceInvaders"))
        {
            if (Player._dead)
            {
                StartCoroutine(EndGame());
            }
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene("StartScreen");
    }
}
