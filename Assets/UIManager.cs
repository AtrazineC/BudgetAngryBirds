using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static int score = 0;
    public static int EnemiesAlive = 0;
    public static int BallsUsed = 0;

    public GameObject completeScreen;

    public Text scoreTextWin;
    public Text ballText;

    public GameObject loseScreen;

    public Text scoreTextLose;
    public Text restartTimerLose;

    public void levelComplete() {

        StartCoroutine(FinishWin());

    }

    public void levelFail() {

        StartCoroutine(FinishLose());

    }

    public void restartLevel() {
        score = 0;
        EnemiesAlive = 0;
        BallsUsed = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FinishWin () {
        yield return new WaitForSeconds(1.3f);
        scoreTextWin.text = (score).ToString("");
        ballText.text = (3 - BallsUsed).ToString("");
        completeScreen.SetActive(true);

        yield return new WaitForSeconds(3f);

        restartLevel();
    }

    IEnumerator FinishLose () {
        loseScreen.SetActive(true);

        for (int i = 4; i > 0; i--) {
            restartTimerLose.text = (i).ToString("");
            yield return new WaitForSeconds(1f);
        }

        restartLevel();
    }
}
