﻿using UnityEngine;

// Bullet:
[DisallowMultipleComponent]
public class Bullet : PooledObject
{
  [SerializeField] Transform bulletTransform;
  private BulletShooterBase parentBaseShot;

  // Speed at velocity
  public float speed = 0f;
  // Angle at which going at
  public float angle = 0f;
  // Acceleration
  private float accelSpeed;
  // Acceleration turning
  private float accelTurn;
  // Whether or not the shot is homing
  private bool isHoming;
  // Target, if there is one
  private Transform homingTarget;
  // Angle of homing speed
  private float homingAngleSpeed;
  // Whether or not the shot is a "Wavy" shot
  private bool wave;
  // Speed of waves
  private float waveSpeed;
  // Wave range
  private float waveRangeSize;
  // TODO: pause/resume
  private bool pauseAndResume;
  private float pauseTime;
  private float resumeTime;
  // Auto release (destroy after a certain amount of time)
  private bool useDestroyAfterTime;
  private float destroyAfterTime;
  private bool useMaxSpeed;
  private float maxSpeed;
  private bool useMinSpeed;
  private float minSpeed;

  private float baseAngle;
  private float frameCount;
  private float timeCount;

  private bool isShooting;

  // Is object active?
  public virtual bool isActive { get { return gameObject.activeSelf; } }

  private void Awake()
  {
    bulletTransform = transform;
  }

  private void OnDisable()
  {
    if (isShooting == false)
    {
      return;
    }

    this.DestroyBullet();
  }


  // Activate/Inactivate Bullet
  // Override this method when you want to change the behavior at Active / Inactive.
  public virtual void SetActive(bool isActive)
  {
    gameObject.SetActive(isActive);
  }

  // Finished Shot
  public void OnFinishedShot()
  {
    if (isShooting == false)
    {
      return;
    }
    isShooting = false;

    parentBaseShot = null;
    homingTarget = null;
    bulletTransform.transform.position = Vector3.zero;
    bulletTransform.transform.position = Vector3.zero;
  }

  // Bullet Shot
  public void Shot(BulletShooterBase parentBaseShot,
                   float speed, float angle, float accelSpeed, float accelTurn,
                   bool homing, Transform homingTarget, float homingAngleSpeed,
                   bool wave, float waveSpeed, float waveRangeSize,
                   bool pauseAndResume, float pauseTime, float resumeTime,
                   bool useAutoRelease, float autoReleaseTime,
                   Utils2D.AXIS axisMove, bool inheritAngle,
                   bool useMaxSpeed, float maxSpeed, bool useMinSpeed, float minSpeed)
  {
    if (isShooting)
    {
      Debug.Log("Cancled due to shooting");
      return;
    }
    isShooting = true;

    this.parentBaseShot = parentBaseShot;

    this.speed = speed;
    this.angle = angle;
    this.accelSpeed = accelSpeed;
    this.accelTurn = accelTurn;
    this.isHoming = homing;
    this.homingTarget = homingTarget;
    this.homingAngleSpeed = homingAngleSpeed;
    this.wave = wave;
    this.waveSpeed = waveSpeed;
    this.waveRangeSize = waveRangeSize;
    this.pauseAndResume = pauseAndResume;
    this.pauseTime = pauseTime;
    this.resumeTime = resumeTime;
    this.useDestroyAfterTime = useAutoRelease;
    this.destroyAfterTime = autoReleaseTime;
    this.useMaxSpeed = useMaxSpeed;
    this.maxSpeed = maxSpeed;
    this.useMinSpeed = useMinSpeed;
    this.minSpeed = minSpeed;

    this.baseAngle = 0f;
    if (inheritAngle && this.parentBaseShot.lockOnShot == false)
    {
      this.baseAngle = this.parentBaseShot.transform.eulerAngles.z;
    }

    this.bulletTransform.rotation = Quaternion.Euler(bulletTransform.rotation.x, bulletTransform.rotation.y, baseAngle + this.angle);

    frameCount = 0f;
    timeCount = 0f;
  }

  void Update()
  {
    this.UpdateMove(Time.deltaTime);
  }

  // Update Move
  public void UpdateMove(float deltaTime)
  {
    if (this.isShooting == false)
    {
      return;
    }

    this.timeCount += deltaTime;

    // auto release check
    if (this.useDestroyAfterTime && this.destroyAfterTime > 0f)
    {
      if (this.timeCount >= this.destroyAfterTime && Vector3.Distance(this.transform.position, GameStateManager.Instance.game.player.transform.position) >= 6f)
      {
        // Release
        this.DestroyBullet();
        return;
      }
    }

    // pause and resume.
    if (this.pauseAndResume && this.pauseTime >= 0f && this.resumeTime > this.pauseTime)
    {
      if (this.pauseTime <= this.timeCount && this.timeCount < this.resumeTime)
      {
        return;
      }
    }

    Vector3 myAngles = this.bulletTransform.rotation.eulerAngles;

    Quaternion newRotation = this.bulletTransform.rotation;
    if (this.isHoming)
    {
      // homing target.
      if (this.homingTarget != null && 0f < this.homingAngleSpeed)
      {
        float rotAngle = Utils2D.GetAngleFromTwoPosition(bulletTransform, homingTarget, Utils2D.AXIS.X_AND_Y);
        float myAngle = 0f;

        myAngle = myAngles.z;

        float toAngle = Mathf.MoveTowardsAngle(myAngle, rotAngle, deltaTime * homingAngleSpeed);

        newRotation = Quaternion.Euler(myAngles.x, myAngles.y, toAngle);
      }
    }
    else if (wave)
    {
      // acceleration turning.
      this.angle += (this.accelTurn * deltaTime);
      // wave.
      if (0f < this.waveSpeed && 0f < this.waveRangeSize)
      {
        float waveAngle = angle + (waveRangeSize / 2f * Mathf.Sin(frameCount * waveSpeed / 100f));
        newRotation = Quaternion.Euler(myAngles.x, myAngles.y, baseAngle + waveAngle);
      }
      this.frameCount += Time.deltaTime;
    }
    else
    {
      // acceleration turning.
      float addAngle = this.accelTurn * deltaTime;
      // X and Y axis
      newRotation = Quaternion.Euler(myAngles.x, myAngles.y, myAngles.z + addAngle);
    }

    // acceleration speed.
    this.speed += (this.accelSpeed * deltaTime);

    if (this.useMaxSpeed && this.speed > this.maxSpeed)
    {
      this.speed = this.maxSpeed;
    }

    if (this.useMinSpeed && this.speed < this.minSpeed)
    {
      this.speed = this.minSpeed;
    }

    // move.
    Vector3 newPosition;
    newPosition = bulletTransform.position + (bulletTransform.up * (speed * deltaTime));

    // set new position and rotation
    bulletTransform.SetPositionAndRotation(newPosition, newRotation);
  }


  public void DestroyBullet() {
    this.isShooting = false;
    this.frameCount = 0f;
    this.timeCount = 0f;
    base.ReturnToPool();
  }
}