using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class LinearShot : BulletShooterBase
{
  [Header("===== LinearShot Settings =====")]
  // "Set a angle of shot. (0 to 360)"
  [Range(0f, 360f)]
  public float angle = 180f;
  // "Set a delay time between bullet and next bullet. (sec)"
  public float betweenDelay = 0.1f;

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

      var bullet = GetBullet(this.transform.position);
      if (bullet == null)
      {
        break;
      }

      ShotBullet(bullet, base.bulletSpeed, this.angle);
    }

    FiredShot();

    FinishedShot();
  }
}