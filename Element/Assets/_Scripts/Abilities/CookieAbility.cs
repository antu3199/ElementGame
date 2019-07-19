using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Corn ability that shoots out corn that blocks bullets
public class CookieAbility : PlayerAbilityBase
{
  public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.BAKINGSODA; } }
  public override string commonName { get { return "Baking Soda"; } }
  public override string chemicalName { get { return "((Na)(H)(C)(O)3)n"; } }
  public RotatingShot bulletShooter;
  public override string description
  {
    get
    {
      return "Description: Used for cooking.\n\n Gameplay: Tap to throw a bunch of cookies  !";
    }
  }

  public override bool useableAbility { get { return true; } }
  public override void useAbility()
  {
    // Shoot out a bunch of corn using the RotatingShot
    this.bulletShooter.Shot();
  }
}
