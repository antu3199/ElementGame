using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ability the "heals" the player
public class WaterAbility : PlayerAbilityBase {
  public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.WATER; }  }
	public override string commonName { get { return "Water"; }  }
	public override string chemicalName { get { return "H20"; }  }
  public override string description { get { 
    return "Description: About 71 percent of the Earth's surface is Water! You also drink it.\n\n Gameplay: Tap the screen with two fingers to recover health (on a cooldown)";
  }}

  public override bool useableAbility { get { return true; } }

  public ParticleSystem healEffect;
  public float healEffectDuration;
	public float healAmount = 5;
	public override void useAbility() {
    // "Heal" the player
		GameStateManager.Instance.game.updateHealth(healAmount);
    StartCoroutine(this.healEffectCoroutine());
	}

  private IEnumerator healEffectCoroutine() {
    // Play a particle system for a few seconds.
    this.healEffect.Play();
    yield return new WaitForSeconds(this.healEffectDuration);
    this.healEffect.Stop();
  }
}
