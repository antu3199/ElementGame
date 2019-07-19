using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ability the "heals" the player
public class CyanideAbility : PlayerAbilityBase {
    public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.CYANIDE; } }
	public override string commonName { get { return "Cyanide"; }  }
	public override string chemicalName { get { return "CN"; }  }
  public override string description { get { 
    return "Description: Cyanide is a potentially very deadly chemical found in various forms in nature.\n\n Gameplay: Continuously lose health";
  }}

    public override bool useableAbility { get { return false; } }

    float curTime = 0;
    public float drainAmount = -1;
    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        // Increase score over time
        curTime += Time.deltaTime;
        if (curTime >= 5)
        {
            GameStateManager.Instance.game.updateHealth(drainAmount);
            curTime = 0;
        }
    }
}
