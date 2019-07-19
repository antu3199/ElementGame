using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RotatingShot : BulletShooterBase
{
  [Header("===== RotatingShot Settings =====")]
  
  public int numBullets = 60;
  public float deltaAngle = 5;
  public float initialAngle = 12;
	public float delay = 0;

  private float prevAngle;


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
		if (delay != 0)
		{
			yield return new WaitForSeconds(this.delay);
		}
	}
    FiredShot();
    FinishedShot();
    this.prevAngle += this.initialAngle;
    this.prevAngle = this.prevAngle % 360;
  }
}