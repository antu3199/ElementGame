using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

// Bullet shooter base
public abstract class BulletShooterBase : MonoBehaviour
{
  [Header("=== Bullet Base Settings ===")]
  // Sets a "Prefab" (the sprite that we will shoot)
  public Bullet bulletPrefab;
  // The number of bullets we will shoot
  public int bulletNum = 10;
  // Speed of the bullet
  public float bulletSpeed = 2f;
  // Acceleration of the bullet."
  public float accelerationSpeed = 0f;
  // Do we have a speed limiter?
  public bool useMaxSpeed = false;
  // Sets the max speed (Valid only if m_useMaxSpeed is enabled)
  public float maxSpeed = 0f;
  // Do we have a min speed?
  public bool useMinSpeed = false;
  //  Sets minimum speed (valid only if useMinSpeed is checked)
  public float minSpeed = 0f;
  // For turning bullets, the accleration
  public float accelerationTurn = 0f;
  // TODO: Pause/resume
  public bool usePauseAndResume = false;
  // "Set a time to pause bullet."
  public float pauseTime = 0f;
  // "Set a time to resume bullet."
  public float resumeTime = 0f;
  // Automatically destroy after an amount of time
  public bool useDestroyAfterTime = false;
  // Set a time to destroy
  public float destroyAfterTime = 10f;

  // Whether or not angle will be inherited
  public bool inheritAngle = true;


  // Callback to be fired when shot is fired
  public UnityEvent shotFiredCallback = new UnityEvent();
  // Callback to be fired when shot is finished.
  public UnityEvent shotFinishedCallback = new UnityEvent();

  // Flag for shooting
  protected bool isShooting;

  public bool shooting { get { return isShooting; } }

  // Flag for lockon
  public virtual bool lockOnShot { get { return false; } }

  // Sets shooting to false
  protected virtual void OnDisable()
  {
    this.isShooting = false;
  }

  // "Main" function that shoots a bullet based on bullet pattern
  public abstract void Shot();

  // Invoke callback for fired shot
  protected void FiredShot()
  {
    this.shotFiredCallback.Invoke();
  }

  // Invoke callback for finished shot
  protected void FinishedShot()
  {
    this.isShooting = false;
    this.shotFinishedCallback.Invoke();
  }

  // Get bullet from ObjectPool
  protected Bullet GetBullet(Vector3 position, bool forceInstantiate = false)
  {
    if (this.bulletPrefab == null)
    {
      Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
      return null;
    }

    Bullet bullet = this.bulletPrefab.GetPooledInstance<Bullet>(position);
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
                speed, angle, accelerationSpeed, accelerationTurn,
                homing, homingTarget, homingAngleSpeed,
                wave, waveSpeed, waveRangeSize,
                usePauseAndResume, pauseTime, resumeTime,
                useDestroyAfterTime, destroyAfterTime,
                Utils2D.AXIS.X_AND_Y, inheritAngle,
                useMaxSpeed, maxSpeed, useMinSpeed, minSpeed);
  }
}