using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// Ubh base shot.
/// Each shot pattern classes inherit this class.
/// </summary>
public abstract class BulletShooterBase : MonoBehaviour
{
    [Header("===== Common Settings =====")]
    // "Set a bullet prefab for the shot. (ex. sprite or model)"
    public Bullet m_bulletPrefab;
    // "Set a bullet number of shot."
    public int m_bulletNum = 10;
    // "Set a bullet base speed of shot."
    public float m_bulletSpeed = 2f;
    // "Set an acceleration of bullet speed."
    public float m_accelerationSpeed = 0f;
    // "Use max speed flag."
    public bool m_useMaxSpeed = false;
    // "Set a bullet max speed of shot."
    public float m_maxSpeed = 0f;
    // "Use min speed flag"
    public bool m_useMinSpeed = false;
    // "Set a bullet min speed of shot."
    public float m_minSpeed = 0f;
    // "Set an acceleration of bullet turning."
    public float m_accelerationTurn = 0f;
    // "This flag is pause and resume bullet at specified time."
    public bool m_usePauseAndResume = false;
    // "Set a time to pause bullet."
    public float m_pauseTime = 0f;
    // "Set a time to resume bullet."
    public float m_resumeTime = 0f;
    // "This flag is automatically release the bullet GameObject at the specified time."
    public bool m_useAutoRelease = false;
    // "Set a time to automatically release after the shot at using UseAutoRelease. (sec)"
    public float m_autoReleaseTime = 10f;

    public bool m_inheritAngle = true;

    [Space(10)]

    // "Set a callback method fired shot."
    public UnityEvent m_shotFiredCallbackEvents = new UnityEvent();
    // "Set a callback method after shot."
    public UnityEvent m_shotFinishedCallbackEvents = new UnityEvent();

    protected bool m_shooting;

    /// <summary>
    /// is shooting flag.
    /// </summary>
    public bool shooting { get { return m_shooting; } }

    /// <summary>
    /// is lock on shot flag.
    /// </summary>
    public virtual bool lockOnShot { get { return false; } }

    /// <summary>
    /// Call from override OnDisable method in inheriting classes.
    /// Example : protected override void OnDisable () { base.OnDisable (); }
    /// </summary>
    protected virtual void OnDisable()
    {
        m_shooting = false;
    }

    /// <summary>
    /// Abstract shot method.
    /// </summary>
    public abstract void Shot();

    /// <summary>
    /// Fired shot.
    /// </summary>
    protected void FiredShot()
    {
        m_shotFiredCallbackEvents.Invoke();
    }

    /// <summary>
    /// Finished shot.
    /// </summary>
    protected void FinishedShot()
    {
        m_shooting = false;
        m_shotFinishedCallbackEvents.Invoke();
    }

    /// <summary>
    /// Get UbhBullet object from object pool.
    /// </summary>
    protected Bullet GetBullet(Vector3 position, bool forceInstantiate = false)
    {
        if (m_bulletPrefab == null)
        {
            Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
            return null;
        }

        // get UbhBullet from ObjectPool
        Bullet bullet = m_bulletPrefab.GetPooledInstance<Bullet> (position);
        if (bullet == null)
        {
            return null;
        }

        return bullet;
    }

    /// <summary>
    /// Shot UbhBullet object.
    /// </summary>
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