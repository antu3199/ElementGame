using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbility : PlayerAbilityBase {
    public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.WATER; }  }
	public override string commonName { get { return "Water"; }  }
	public override string chemicalName { get { return "H20"; }  }
	public float healAmount;
	public override void useAbility() {
		GameStateManager.Instance.player.playerHealth.updateHealth(healAmount);
	}
}
