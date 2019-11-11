using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
  public GameObject HUD;

  public static int score = 0;
  public static int EnemiesAlive = 0;
  public static int BallsUsed = 0;

  public GameObject completeScreen;

  public Text scoreTextWin;
  public Text ballText;

  public GameObject loseScreen;

  public Text restartTimerLose;

  public LevelManager LevelManager;

  public GameObject LevelStartScreen;
  public Text levelText;

  void Start()
  {
    levelText.text = SceneManager.GetActiveScene().name;
    LevelStartScreen.SetActive(true);
    FindObjectOfType<AudioManager>().Play("Main");
    StartCoroutine(P300_create());
  }

  IEnumerator P300_create()
  {
    yield return new WaitForSeconds(0.3f);
    HUD.SetActive(true);
  }

  public void levelComplete()
  {
    StartCoroutine(FinishWin());
    HUD.SetActive(false);
  }

  public void levelFail()
  {
    StartCoroutine(FinishLose());
    HUD.SetActive(false);
  }

  public void restartLevel()
  {

    score = 0;
    EnemiesAlive = 0;
    BallsUsed = 0;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

  }

  public void nextLevel()
  {

    score = 0;
    EnemiesAlive = 0;
    BallsUsed = 0;
    LevelManager.nextLevel();

  }

  IEnumerator FinishWin()
  {
    yield return new WaitForSeconds(1.3f);
    FindObjectOfType<AudioManager>().StopPlaying("Main");
    FindObjectOfType<AudioManager>().Play("Win");
    scoreTextWin.text = (score).ToString("");
    ballText.text = (3 - BallsUsed).ToString("");
    completeScreen.SetActive(true);

    yield return new WaitForSeconds(4f);

    nextLevel();
  }

  IEnumerator FinishLose()
  {
    loseScreen.SetActive(true);

    for (int i = 4; i > 0; i--)
    {
      restartTimerLose.text = (i).ToString("");
      yield return new WaitForSeconds(1f);
    }

    restartLevel();
  }
}
