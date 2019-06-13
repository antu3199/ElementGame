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
  public List<BulletShooterBase> m_shotObjs;

  // "Set a delay time to starting next shot pattern"
  public float m_afterDelay;

  private bool m_shooting;

  // Returns whether or not we are shooting
  public bool shooting { get { return m_shooting; } }

  private void Start()
  {
    m_shotObjs = new List<BulletShooterBase>(gameObject.GetComponentsInChildren<BulletShooterBase>());
    StartShotRoutine();
  }

  private void OnDisable()
  {
    m_shooting = false;
  }

  /// <summary>
  /// Start the shot routine.
  /// </summary>
  public void StartShotRoutine()
  {
    if (m_shooting)
    {
      Debug.LogWarning("Already shooting.");
      return;
    }
    m_shooting = true;

    StartCoroutine(ShotCoroutine());
  }


  // Starts a shot coroutine, which periodically shoots the pattern
  private IEnumerator ShotCoroutine()
  {

    while (true)
    {
      if (m_shotObjs[this.shotIndex] != null)
      {
        m_shotObjs[this.shotIndex].Shot();
      }

      yield return new WaitForSeconds(m_afterDelay);

      if (m_shooting == false)
      {
        break;
      }
    }

    m_shooting = false;
  }

  // Forcefully stop the shot coroutine
  public void StopShotRoutine()
  {
    m_shooting = false;
  }
}