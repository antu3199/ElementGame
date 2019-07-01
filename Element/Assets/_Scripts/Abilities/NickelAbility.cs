using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickelAbility : PlayerAbilityBase {
    public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.NICKEL; }  }
	public override string commonName { get { return "Nickel"; }  }
	public override string chemicalName { get { return "Si"; }  }
  public override string description { get { 
    return "Description: Sudbury Nickel Irruptive, is a major geological structure in Ontario, Canada. It is the third-largest known impact crater or astrobleme on Earth, as well as one of the oldest. The crater formed 1.849 billion years ago in the Paleoproterozoic era. Gameplay: Give you x5 score every second\n\n";
  }}

  public override bool useableAbility { get { return false; } }
  
  float curTime = 0;
  // Update is called once per frame
  public new void Update () {
    base.Update();
    curTime += Time.deltaTime;
    if (curTime >= 1) {
      GameStateManager.Instance.game.score+= 5;
      curTime = 0;
    }
  }
}
