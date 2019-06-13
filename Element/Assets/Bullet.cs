using UnityEngine;

// Bullet:
// Reference: https://assetstore.unity.com/packages/tools/integration/uni-bullet-hell-19088
[DisallowMultipleComponent]
public class Bullet : PooledObject
{
  [SerializeField] Transform m_transformCache;
  private BulletShooterBase m_parentBaseShot;

  // Speed at velocity
  private float m_speed;
  // Angle at which going at
  private float m_angle;
  // Acceleration
  private float m_accelSpeed;
  // Acceleration turning
  private float m_accelTurn;
  // Whether or not the shot is homing
  private bool m_homing;
  // Target, if there is one
  private Transform m_homingTarget;
  // Angle of homing speed
  private float m_homingAngleSpeed;
  // Whether or not the shot is a "Wavy" shot
  private bool m_wave;
  // Speed of waves
  private float m_waveSpeed;
  // Wave range
  private float m_waveRangeSize;
  // TODO: pause/resume
  private bool m_pauseAndResume;
  private float m_pauseTime;
  private float m_resumeTime;
  // Auto release (destroy after a certain amount of time)
  private bool m_useAutoRelease;
  private float m_autoReleaseTime;
  private bool m_useMaxSpeed;
  private float m_maxSpeed;
  private bool m_useMinSpeed;
  private float m_minSpeed;

  private float m_baseAngle;
  private float m_selfFrameCnt;
  private float m_selfTimeCount;

  private bool m_shooting;

  // Is object active?
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

    this.ReturnBullet();
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
    m_useMaxSpeed = useMaxSpeed;
    m_maxSpeed = maxSpeed;
    m_useMinSpeed = useMinSpeed;
    m_minSpeed = minSpeed;

    m_baseAngle = 0f;
    if (inheritAngle && m_parentBaseShot.lockOnShot == false)
    {

      // X and Y axis
      m_baseAngle = m_parentBaseShot.transform.eulerAngles.z;
    }

    m_transformCache.rotation = Quaternion.Euler(m_transformCache.rotation.x, m_transformCache.rotation.y, m_baseAngle + m_angle);

    m_selfFrameCnt = 0f;
    m_selfTimeCount = 0f;
  }

  void Update()
  {
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
        this.ReturnBullet();
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
        float rotAngle = Utils2D.GetAngleFromTwoPosition(m_transformCache, m_homingTarget, Utils2D.AXIS.X_AND_Y);
        float myAngle = 0f;

        myAngle = myAngles.z;

        float toAngle = Mathf.MoveTowardsAngle(myAngle, rotAngle, deltaTime * m_homingAngleSpeed);

        newRotation = Quaternion.Euler(myAngles.x, myAngles.y, toAngle);
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
        newRotation = Quaternion.Euler(myAngles.x, myAngles.y, m_baseAngle + waveAngle);
      }
      m_selfFrameCnt += Time.deltaTime;
    }
    else
    {
      // acceleration turning.
      float addAngle = m_accelTurn * deltaTime;
      // X and Y axis
      newRotation = Quaternion.Euler(myAngles.x, myAngles.y, myAngles.z + addAngle);
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
    newPosition = m_transformCache.position + (m_transformCache.up * (m_speed * deltaTime));

    // set new position and rotation
    m_transformCache.SetPositionAndRotation(newPosition, newRotation);
  }


  private void ReturnBullet() {
    m_baseAngle = 0f;
    m_transformCache.rotation = Quaternion.Euler(m_transformCache.rotation.x, m_transformCache.rotation.y, m_baseAngle + m_angle);

    m_selfFrameCnt = 0f;
    m_selfTimeCount = 0f;
    base.ReturnToPool();
  }
}