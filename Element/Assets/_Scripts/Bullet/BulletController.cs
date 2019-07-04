﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;


// Controller used for shooting bullets
public sealed class BulletController : MonoBehaviour
{
  public int shotIndex = 0;

  // "Set a shot pattern
  public List<BulletShooterBase> shotObjs;

  // "Set a delay time to starting next shot pattern"
  public float afterDelay = 1f;

  private bool isShooting;

  // Returns whether or not we are shooting
  public bool shooting { get { return isShooting; } }

  private void Start()
  {
    this.shotObjs = new List<BulletShooterBase>(gameObject.GetComponentsInChildren<BulletShooterBase>());
    StartShotRoutine();
  }

  private void OnDisable()
  {
    this.isShooting = false;
  }

  /// Start the shot routine.
  public void StartShotRoutine()
  {
    if (this.isShooting)
    {
      Debug.LogWarning("Already shooting.");
      return;
    }
    this.isShooting = true;

    StartCoroutine(ShotCoroutine());
  }


  // Starts a shot coroutine, which periodically shoots the pattern
  private IEnumerator ShotCoroutine()
  {

    while (true)
    {
	  if (this.shotIndex == 3) {
		shotObjs[3].Shot();
		shotObjs[1].Shot();
	  } else if (shotObjs[this.shotIndex] != null) {
        shotObjs[this.shotIndex].Shot();
      }
      yield return new WaitForSeconds(afterDelay);

      if (isShooting == false)
      {
        break;
      }
    }

    isShooting = false;
  }

  // Forcefully stop the shot coroutine
  public void StopShotRoutine()
  {
    isShooting = false;
  }
}