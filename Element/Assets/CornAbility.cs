using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornAbility : PlayerAbilityBase {
 public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.CORNSTARCH; }  }
	public override string commonName { get { return "Corn starch"; }  }
	public override string chemicalName { get { return "idk"; }  }
	public override void useAbility() {
		// DO something
            
	}
}
