using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

// Reference: https://assetstore.unity.com/packages/tools/integration/uni-bullet-hell-19088
public class RotatingShot : BulletShooterBase
{
  [Header("===== RotatingShot Settings =====")]

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

    for (int i = 0; i < 60; i++) {
        var bullet = GetBullet(transform.position);
        if (bullet == null) {
            break;
        }
        ShotBullet(bullet, bulletSpeed/2, prevAngle);
        this.prevAngle += 5;
    }

    FiredShot();
    FinishedShot();
    this.prevAngle += 12;
  }
}