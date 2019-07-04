using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Corn ability that shoots out corn that blocks bullets
public class CornAbility : PlayerAbilityBase
{
  public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.CORNSTARCH; } }
  public override string commonName { get { return "Corn starch"; } }
  public override string chemicalName { get { return "((C)6(H)9(O)5)n"; } }
  public RotatingShot bulletShooter;
  public override string description
  {
    get
    {
      return "Description: Used for cooking.\n\n Gameplay: Tap to throw a furry of corn!";
    }
  }

  public override bool useableAbility { get { return true; } }
  public override void useAbility()
  {
    // Shoot out a bunch of corn using the RotatingShot
    this.bulletShooter.Shot();
  }
}
