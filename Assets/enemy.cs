using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
  public UIManager UIManager;

  public GameObject deathEffect;
  public Text scoreText;

  public float health = 4f;
  public int pointValue = 100;
  public bool alive = true;

  void Start ()
  {
    UIManager.EnemiesAlive++;
  }

  void OnCollisionEnter2D (Collision2D colInfo)
  {
    if (colInfo.relativeVelocity.magnitude > health) {
      Die();
    }

  }

  void Die()
  {
    if (alive == true) {

      alive = false;

      Instantiate(deathEffect, transform.position, Quaternion.identity);

      UIManager.EnemiesAlive --;
      if (UIManager.EnemiesAlive <= 0)
        UIManager.levelComplete();

      Destroy(gameObject);

      UIManager.score = UIManager.score + pointValue;
      scoreText.text = "Score: " + (UIManager.score).ToString("");
      
    }

  }

}
