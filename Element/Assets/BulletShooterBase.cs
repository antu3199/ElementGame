using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

// Bullet shooter base
// Reference: https://assetstore.unity.com/packages/tools/integration/uni-bullet-hell-19088
public abstract class BulletShooterBase : MonoBehaviour
{
  [Header("=== Bullet Base Settings ===")]
  // Sets a "Prefab" (the sprite that we will shoot)
  public Bullet m_bulletPrefab;
  // The number of bullets we will shoot
  public int m_bulletNum = 10;
  // Speed of the bullet
  public float m_bulletSpeed = 2f;
  // Acceleration of the bullet."
  public float m_accelerationSpeed = 0f;
  // Do we have a speed limiter?
  public bool m_useMaxSpeed = false;
  // Sets the max speed (Valid only if m_useMaxSpeed is enabled)
  public float m_maxSpeed = 0f;
  // Do we have a min speed?
  public bool m_useMinSpeed = false;
  //  Sets minimum speed (valid only if useMinSpeed is checked)
  public float m_minSpeed = 0f;
  // For turning bullets, the accleration
  public float m_accelerationTurn = 0f;
  // TODO: Pause/resume
  public bool m_usePauseAndResume = false;
  // "Set a time to pause bullet."
  public float m_pauseTime = 0f;
  // "Set a time to resume bullet."
  public float m_resumeTime = 0f;
  // Automatically destroy after an amount of time
  public bool m_useAutoRelease = false;
  // Set a time to destroy
  public float m_autoReleaseTime = 10f;

  // Whether or not angle will be inherited
  public bool m_inheritAngle = true;


  // Callback to be fired when shot is fired
  public UnityEvent m_shotFiredCallbackEvents = new UnityEvent();
  // Callback to be fired when shot is finished.
  public UnityEvent m_shotFinishedCallbackEvents = new UnityEvent();

  // Flag for shooting
  protected bool m_shooting;

  public bool shooting { get { return m_shooting; } }

  // Flag for lockon
  public virtual bool lockOnShot { get { return false; } }

  // Sets shooting to false
  protected virtual void OnDisable()
  {
    m_shooting = false;
  }

  // "Main" function that shoots a bullet based on bullet pattern
  public abstract void Shot();

  // Invoke callback for fired shot
  protected void FiredShot()
  {
    m_shotFiredCallbackEvents.Invoke();
  }

  // Invoke callback for finished shot
  protected void FinishedShot()
  {
    m_shooting = false;
    m_shotFinishedCallbackEvents.Invoke();
  }

  // Get bullet from ObjectPool
  protected Bullet GetBullet(Vector3 position, bool forceInstantiate = false)
  {
    if (m_bulletPrefab == null)
    {
      Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
      return null;
    }

    Bullet bullet = m_bulletPrefab.GetPooledInstance<Bullet>(position);
    if (bullet == null)
    {
      return null;
    }

    return bullet;
  }

  // Shoot the bullet 
  protected void ShotBullet(Bullet bullet, float speed, float angle,
                             bool homing = false, Transform homingTarget = null, float homingAngleSpeed = 0f,
                             bool wave = false, float waveSpeed = 0f, float waveRangeSize = 0f)
  {
    if (bullet == null)
    {
      return;
    }
    bullet.Shot(this,
                speed, angle, m_accelerationSpeed, m_accelerationTurn,
                homing, homingTarget, homingAngleSpeed,
                wave, waveSpeed, waveRangeSize,
                m_usePauseAndResume, m_pauseTime, m_resumeTime,
                m_useAutoRelease, m_autoReleaseTime,
                Utils2D.AXIS.X_AND_Y, m_inheritAngle,
                m_useMaxSpeed, m_maxSpeed, m_useMinSpeed, m_minSpeed);
  }
}