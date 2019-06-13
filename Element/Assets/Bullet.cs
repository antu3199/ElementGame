using UnityEngine;

/// <summary>
/// Ubh bullet.
/// </summary>
[DisallowMultipleComponent]
public class Bullet : PooledObject
{
    [SerializeField] Transform m_transformCache;
    private BulletShooterBase m_parentBaseShot;
    private float m_speed;
    private float m_angle;
    private float m_accelSpeed;
    private float m_accelTurn;
    private bool m_homing;
    private Transform m_homingTarget;
    private float m_homingAngleSpeed;
    private bool m_wave;
    private float m_waveSpeed;
    private float m_waveRangeSize;
    private bool m_pauseAndResume;
    private float m_pauseTime;
    private float m_resumeTime;
    private bool m_useAutoRelease;
    private float m_autoReleaseTime;
    private Utils2D.AXIS m_axisMove;
    private bool m_useMaxSpeed;
    private float m_maxSpeed;
    private bool m_useMinSpeed;
    private float m_minSpeed;

    private float m_baseAngle;
    private float m_selfFrameCnt;
    private float m_selfTimeCount;

    private bool m_shooting;

    /// <summary>
    /// Activate/Inactivate flag
    /// Override this property when you want to change the behavior at Active / Inactive.
    /// </summary>
    public virtual bool isActive { get { return gameObject.activeSelf; } }

    private void Awake()
    {
        m_transformCache = transform;
    }

    private void OnDisable()
    {
        if (m_shooting == false)
        {
            return;
        }

        base.ReturnToPool();
    }

    /// <summary>
    /// Activate/Inactivate Bullet
    /// Override this method when you want to change the behavior at Active / Inactive.
    /// </summary>
    public virtual void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    /// <summary>
    /// Finished Shot
    /// </summary>
    public void OnFinishedShot()
    {
        if (m_shooting == false)
        {
            return;
        }
        m_shooting = false;

        m_parentBaseShot = null;
        m_homingTarget = null;
        m_transformCache.transform.position = Vector3.zero;
        m_transformCache.transform.position = Vector3.zero;
    }

    /// <summary>
    /// Bullet Shot
    /// </summary>
    public void Shot(BulletShooterBase parentBaseShot,
                     float speed, float angle, float accelSpeed, float accelTurn,
                     bool homing, Transform homingTarget, float homingAngleSpeed,
                     bool wave, float waveSpeed, float waveRangeSize,
                     bool pauseAndResume, float pauseTime, float resumeTime,
                     bool useAutoRelease, float autoReleaseTime,
                     Utils2D.AXIS axisMove, bool inheritAngle,
                     bool useMaxSpeed, float maxSpeed, bool useMinSpeed, float minSpeed)
    {
        if (m_shooting)
        {
            return;
        }
        m_shooting = true;

        m_parentBaseShot = parentBaseShot;

        m_speed = speed;
        m_angle = angle;
        m_accelSpeed = accelSpeed;
        m_accelTurn = accelTurn;
        m_homing = homing;
        m_homingTarget = homingTarget;
        m_homingAngleSpeed = homingAngleSpeed;
        m_wave = wave;
        m_waveSpeed = waveSpeed;
        m_waveRangeSize = waveRangeSize;
        m_pauseAndResume = pauseAndResume;
        m_pauseTime = pauseTime;
        m_resumeTime = resumeTime;
        m_useAutoRelease = useAutoRelease;
        m_autoReleaseTime = autoReleaseTime;
        m_axisMove = axisMove;
        m_useMaxSpeed = useMaxSpeed;
        m_maxSpeed = maxSpeed;
        m_useMinSpeed = useMinSpeed;
        m_minSpeed = minSpeed;

        m_baseAngle = 0f;
        if (inheritAngle && m_parentBaseShot.lockOnShot == false)
        {
            if (m_axisMove == Utils2D.AXIS.X_AND_Z)
            {
                // X and Z axis
                m_baseAngle = m_parentBaseShot.transform.eulerAngles.y;
            }
            else
            {
                // X and Y axis
                m_baseAngle = m_parentBaseShot.transform.eulerAngles.z;
            }
        }

        m_transformCache.rotation = Quaternion.Euler(m_transformCache.rotation.x, m_transformCache.rotation.y, m_baseAngle + m_angle);

        m_selfFrameCnt = 0f;
        m_selfTimeCount = 0f;
    }

    void Update() {
        this.UpdateMove(Time.deltaTime);
    }

    /// <summary>
    /// Update Move
    /// </summary>
    public void UpdateMove(float deltaTime)
    {
        if (m_shooting == false)
        {
            return;
        }

        m_selfTimeCount += deltaTime;

        // auto release check
        if (m_useAutoRelease && m_autoReleaseTime > 0f)
        {
            if (m_selfTimeCount >= m_autoReleaseTime)
            {
                // Release
                base.ReturnToPool();
                return;
            }
        }

        // pause and resume.
        if (m_pauseAndResume && m_pauseTime >= 0f && m_resumeTime > m_pauseTime)
        {
            if (m_pauseTime <= m_selfTimeCount && m_selfTimeCount < m_resumeTime)
            {
                return;
            }
        }

        Vector3 myAngles = m_transformCache.rotation.eulerAngles;

        Quaternion newRotation = m_transformCache.rotation;
        if (m_homing)
        {
            // homing target.
            if (m_homingTarget != null && 0f < m_homingAngleSpeed)
            {
                float rotAngle = Utils2D.GetAngleFromTwoPosition(m_transformCache, m_homingTarget, m_axisMove);
                float myAngle = 0f;
                if (m_axisMove == Utils2D.AXIS.X_AND_Z)
                {
                    // X and Z axis
                    myAngle = -myAngles.y;
                }
                else
                {
                    // X and Y axis
                    myAngle = myAngles.z;
                }

                float toAngle = Mathf.MoveTowardsAngle(myAngle, rotAngle, deltaTime * m_homingAngleSpeed);

                if (m_axisMove == Utils2D.AXIS.X_AND_Z)
                {
                    // X and Z axis
                    newRotation = Quaternion.Euler(myAngles.x, -toAngle, myAngles.z);
                }
                else
                {
                    // X and Y axis
                    newRotation = Quaternion.Euler(myAngles.x, myAngles.y, toAngle);
                }
            }
        }
        else if (m_wave)
        {
            // acceleration turning.
            m_angle += (m_accelTurn * deltaTime);
            // wave.
            if (0f < m_waveSpeed && 0f < m_waveRangeSize)
            {
                float waveAngle = m_angle + (m_waveRangeSize / 2f * Mathf.Sin(m_selfFrameCnt * m_waveSpeed / 100f));
                if (m_axisMove == Utils2D.AXIS.X_AND_Z)
                {
                    // X and Z axis
                    newRotation = Quaternion.Euler(myAngles.x, m_baseAngle - waveAngle, myAngles.z);
                }
                else
                {
                    // X and Y axis
                    newRotation = Quaternion.Euler(myAngles.x, myAngles.y, m_baseAngle + waveAngle);
                }
            }
            m_selfFrameCnt += Time.deltaTime;
        }
        else
        {
            // acceleration turning.
            float addAngle = m_accelTurn * deltaTime;
            if (m_axisMove == Utils2D.AXIS.X_AND_Z)
            {
                // X and Z axis
                newRotation = Quaternion.Euler(myAngles.x, myAngles.y - addAngle, myAngles.z);
            }
            else
            {
                // X and Y axis
                newRotation = Quaternion.Euler(myAngles.x, myAngles.y, myAngles.z + addAngle);
            }
        }

        // acceleration speed.
        m_speed += (m_accelSpeed * deltaTime);

        if (m_useMaxSpeed && m_speed > m_maxSpeed)
        {
            m_speed = m_maxSpeed;
        }

        if (m_useMinSpeed && m_speed < m_minSpeed)
        {
            m_speed = m_minSpeed;
        }

        // move.
        Vector3 newPosition;
        if (m_axisMove == Utils2D.AXIS.X_AND_Z)
        {
            // X and Z axis
            newPosition = m_transformCache.position + (m_transformCache.forward * (m_speed * deltaTime));
        }
        else
        {
            // X and Y axis
            newPosition = m_transformCache.position + (m_transformCache.up * (m_speed * deltaTime));
        }

        // set new position and rotation
        m_transformCache.SetPositionAndRotation(newPosition, newRotation);
    }
}