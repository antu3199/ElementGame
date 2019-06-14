using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

// Reference: https://assetstore.unity.com/packages/tools/integration/uni-bullet-hell-19088
public class RandomShot : BulletShooterBase
{
  [Header("===== RandomShot Settings =====")]
  // "Set a angle of shot. (0 to 360)"
  // "Set a delay time between bullet and next bullet. (sec)"
  public float betweenDelay = 0.1f;
  public float minimumAngleDistance = 10f;
  public float minimumAngleSeparation = 25f;
  float prevAngle = 0;

  public override void Shot()
  {
    StartCoroutine(ShotCoroutine());
  }

  private IEnumerator ShotCoroutine()
  {
    if (base.bulletNum <= 0 || base.bulletSpeed <= 0f)
    {
      Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
      yield break;
    }
    if (base.isShooting)
    {
      yield break;
    }
    base.isShooting = true;

    for (int i = 0; i < base.bulletNum; i++)
    {
      if (0 < i && 0f < this.betweenDelay)
      {
        FiredShot();
        yield return new WaitForSeconds(this.betweenDelay);
      }

      var bullet = GetBullet(transform.position);
      if (bullet == null)
      {
        break;
      }

      float shootingAngle = Random.Range(0, 360);
      if (Mathf.Abs(shootingAngle - prevAngle) <= this.minimumAngleDistance)
      {
        shootingAngle = Random.Range(0.0f, 1.0f) >= 0.5f ? shootingAngle + this.minimumAngleSeparation : shootingAngle - this.minimumAngleSeparation;
        shootingAngle = Utils2D.GetNormalizedAngle(shootingAngle);
      }

      ShotBullet(bullet, bulletSpeed, shootingAngle);
      this.prevAngle = shootingAngle;
    }

    FiredShot();

    FinishedShot();
  }
}