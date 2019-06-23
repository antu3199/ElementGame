﻿using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

// Reference: https://assetstore.unity.com/packages/tools/integration/uni-bullet-hell-19088
public class RotatingShot : BulletShooterBase
{
  [Header("===== RotatingShot Settings =====")]
  
  public int numBullets = 60;
  public float deltaAngle = 5;
  public float initialAngle = 12;

  float prevAngle;


  public override void Shot()
  {
    StartCoroutine(ShotCoroutine());
  }

  private IEnumerator ShotCoroutine()
  {
    if (base.bulletSpeed <= 0f)
    {
      Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
      yield break;
    }
    if (base.isShooting)
    {
      yield break;
    }
    base.isShooting = true;


    for (int i = 0; i < this.numBullets; i++) {
        var bullet = GetBullet(transform.position);
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
        if (bullet == null) {
            break;
        }
        ShotBullet(bullet, this.bulletSpeed/2, prevAngle);
        this.prevAngle += this.deltaAngle;
    }
    FiredShot();
    FinishedShot();
    this.prevAngle += this.initialAngle;
    this.prevAngle = this.prevAngle % 360;
  }
}