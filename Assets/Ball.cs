using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
  public UIManager UIManager;

  public Rigidbody2D rb;
  public Rigidbody2D hook;
  public GameObject nextBall;

  public float releaseTime = 0.15f;
  private float currentDragDistance = 2.25f;
  private float maxDragDistance = 2.75f;
  private float minDragDistance = 0.4f;

  private bool aiming = true;
  private float angle = (float)(Math.PI);
  private bool direction = true;
  private Vector3 yeet;

  private double originX = -5.31f;
  private double originY = -1.95f;

  void Update() {

    if (Input.GetKey("space")) {
      if (aiming) {

        if (angle >= 9*Math.PI/7) {
          direction = false;
        } else if (angle <= 5*Math.PI/7) {
          direction = true;
        }

        if (direction) {
          angle = angle + 0.016f;
        } else {
          angle = angle - 0.016f;
        }
        
        Vector3 originPosition = new Vector3((float)originX, (float)originY, 0);
        Vector3 offset = new Vector3((float)(Math.Cos(angle)), (float)(Math.Sin(angle)), 0);
        offset.Normalize();
        Vector3 desiredPosition = originPosition + (currentDragDistance * offset);
        transform.position = desiredPosition;

      } else {

        if (currentDragDistance >= maxDragDistance) {
          direction = false;
        } else if (currentDragDistance <= minDragDistance) {
          direction = true;
        }

        if (direction) {
          currentDragDistance = (currentDragDistance + 0.035f);
        } else {
          currentDragDistance = (currentDragDistance - 0.035f);
        }

        Vector3 originPosition = new Vector3((float)originX, (float)originY, 0);
        Vector3 desiredPosition = originPosition + (currentDragDistance * yeet);
        transform.position = desiredPosition;

      }
    }

    if (Input.GetKeyUp(KeyCode.Space)) {
      if (aiming) {
        aiming = false;
        yeet = new Vector3((float)(maxDragDistance * Math.Cos(angle)), (float)(maxDragDistance * Math.Sin(angle)), 0);
        yeet.Normalize();
      } else {
        rb.isKinematic = false;
        rb.velocity = - (currentDragDistance / maxDragDistance) * yeet * 20f;
        StartCoroutine(Release());
      }
    }

  }

  IEnumerator Release ()
  {
    UIManager.BallsUsed++;

    yield return new WaitForSeconds(releaseTime);
    this.enabled = false;

    yield return new WaitForSeconds(2f);
    if (nextBall != null && UIManager.EnemiesAlive != 0) {
      nextBall.SetActive(true);
    } else {
      yield return new WaitForSeconds(3f);
      if (UIManager.EnemiesAlive != 0) {
        UIManager.levelFail();
      }
    }

  }

}
