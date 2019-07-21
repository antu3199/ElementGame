using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ability the "heals" the player
public class AluminumAbility : PlayerAbilityBase {
  public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.ALUMINUM; }  }
	public override string commonName { get { return "Aluminum"; }  }
	public override string chemicalName { get { return "AI"; }  }
  public override string description { get { 
    return "Description: Remarkable for its low density and its ability to resist corrosion through phenomenon of passivation.\n\n Gameplay: Tap the screen with two fingers Wraps itself in aluminum foil, blocking anything for a period of time";
  }}

  	  public override bool useableAbility { get { return false; } }

}
