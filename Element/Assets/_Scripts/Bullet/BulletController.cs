using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;


// Controller used for shooting bullets
// Reference: https://assetstore.unity.com/packages/tools/integration/uni-bullet-hell-19088
public sealed class BulletController : MonoBehaviour
{
  public int shotIndex = 0;

  // "Set a shot pattern
  public List<BulletShooterBase> shotObjs;

  // "Set a delay time to starting next shot pattern"
  public float afterDelay;

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

  /// <summary>
  /// Start the shot routine.
  /// </summary>
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
      if (shotObjs[this.shotIndex] != null)
      {
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