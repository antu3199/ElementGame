using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbility : PlayerAbilityBase {
    
	public float healAmount;
	public override void useAbility() {
		GameStateManager.Instance.player.playerHealth.updateHealth(healAmount);
	}
}
