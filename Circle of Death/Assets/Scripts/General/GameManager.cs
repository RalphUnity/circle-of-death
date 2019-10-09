using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    //public float restartDelay = 1f;

    public GameObject gameOverUI;
    public EnemyBoss enemyBoss;
    public GameObject nextLevelNotice;
    public GameObject greenPortalLevel;

    void Update()
    {
        NextLevel();
    }
    public void EndGame()
    {

        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            StartCoroutine(GameOver());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        if (enemyBoss.startHealth <= 0)
        {
            StartCoroutine(ProceedToNextLevel());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        gameOverUI.SetActive(true);
    }

    IEnumerator ProceedToNextLevel()
    {
        yield return new WaitForSeconds(2f);
        nextLevelNotice.SetActive(true);
        greenPortalLevel.SetActive(true);

    }

}
