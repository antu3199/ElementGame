using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Ubh shot ctrl.
/// </summary>
[AddComponentMenu("UniBulletHell/Controller/Shot Controller")]
public sealed class BulletController :MonoBehaviour
{
    public int shotIndex = 0;
     // "Set a shot pattern component (inherits UbhBaseShot)."
    public List<BulletShooterBase> m_shotObjs;
    // "Set a delay time to starting next shot pattern. (sec)"
    public float m_afterDelay;

    [Space(10)]

    // "Set a callback method after shot routine."

    private bool m_shooting;

    /// <summary>
    /// is shooting flag.
    /// </summary>
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

        yield break;
    }

    /// <summary>
    /// Stop the shot routine.
    /// </summary>
    public void StopShotRoutine()
    {
        m_shooting = false;
    }
}