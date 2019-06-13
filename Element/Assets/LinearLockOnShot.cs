using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

// Reference: https://assetstore.unity.com/packages/tools/integration/uni-bullet-hell-19088
public class LinearLockOnShot : LinearShot
{
  [Header("=== LinearLockOnShot ===")]
  // "Set a target with tag name."
  public bool m_setTargetFromTag = true;
  // "Set a unique tag name of target at using SetTargetFromTag."
  public string m_targetTagName = "Player";
  // "Flag to randomly select from GameObjects of the same tag."
  public bool m_randomSelectTagTarget;
  // "Transform of lock on target."
  // "It is not necessary if you want to specify target in tag."
  // "Overwrite Angle in direction of target to Transform.position."
  public Transform m_targetTransform;
  // "Always aim to target."
  public bool m_aiming;

  /// <summary>
  /// is lock on shot flag.
  /// </summary>
  public override bool lockOnShot { get { return true; } }

  public override void Shot()
  {
    if (m_shooting)
    {
      return;
    }

    AimTarget();

    if (m_targetTransform == null)
    {
      Debug.LogWarning("Cannot shot because TargetTransform is not set.");
      return;
    }

    base.Shot();

    if (m_aiming)
    {
      StartCoroutine(AimingCoroutine());
    }
  }

  private void AimTarget()
  {
    if (m_targetTransform == null && m_setTargetFromTag)
    {
      m_targetTransform = Utils2D.GetTransformFromTagName(m_targetTagName, m_randomSelectTagTarget);
    }
    if (m_targetTransform != null)
    {
      m_angle = Utils2D.GetAngleFromTwoPosition(transform, m_targetTransform, Utils2D.AXIS.X_AND_Y);
    }
  }

  private IEnumerator AimingCoroutine()
  {
    while (m_aiming)
    {
      if (m_shooting == false)
      {
        yield break;
      }

      AimTarget();

      yield return null;
    }
  }
}