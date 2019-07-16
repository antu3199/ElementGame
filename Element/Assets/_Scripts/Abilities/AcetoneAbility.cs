using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ability Instantly removes all surrounding enemy bullets

public class AcetoneAbility : PlayerAbilityBase {
 public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.ACETONE; }  }
	public override string commonName { get { return "Acetone"; }  }
	public override string chemicalName { get { return "CH3COCH3"; }  }
  public override string description { get { 
    return "Description: Acetone is miscible with water and serves as an important solvent in its own right, typically for cleaning purposes in laboratories";
  }}

  public override bool useableAbility { get { return true; } }

  /*public override void useAbility(){
	  StartCoroutine(this.OnTriggerEnter2D());
  }*/
   void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "EnemyBullet") {
      Bullet bulletScript = other.transform.parent.GetComponent<Bullet>();
      bulletScript.DestroyBullet();
    }
  }
}