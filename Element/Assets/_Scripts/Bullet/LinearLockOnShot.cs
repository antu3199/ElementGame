using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class LinearLockOnShot : LinearShot
{
  [Header("=== LinearLockOnShot ===")]
  public Transform targetTransform;
  // "Always aim to target."
  public bool isAiming;

  /// <summary>
  /// is lock on shot flag.
  /// </summary>
  public override bool lockOnShot { get { return true; } }

  public override void Shot()
  {
    if (base.isShooting)
    {
      return;
    }

    AimTarget();

    if (this.targetTransform == null)
    {
      Debug.LogWarning("Cannot shot because TargetTransform is not set.");
      return;
    }

    base.Shot();

    if (this.isAiming)
    {
      StartCoroutine(AimingCoroutine());
    }
  }

  private void AimTarget()
  {

    if (this.targetTransform != null)
    {
      base.angle = Utils2D.GetAngleFromTwoPosition(transform, this.targetTransform, Utils2D.AXIS.X_AND_Y);
    }
  }

  private IEnumerator AimingCoroutine()
  {
    while (this.isAiming)
    {
      if (base.isShooting == false)
      {
        yield break;
      }

      AimTarget();

      yield return null;
    }
  }
}