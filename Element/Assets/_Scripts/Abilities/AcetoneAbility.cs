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
  public Transform circleObject;
  public float increaseAmount = 0.4f;
  public float timeDuration = 0.5f;
  public override void useAbility(){
    /// Expand the circle;
    StartCoroutine(expandCircle());
  }
// Expands a circle, which removes bullets
    // Note: the "circle" object contains a bulletcollider which removes it, so all we have to do here is
    // Increase the size of it.
    private IEnumerator expandCircle() {
        float time = 0;
        this.circleObject.localScale = new Vector3(0, 0, this.circleObject.localScale.z);
        // show circle
        this.circleObject.gameObject.SetActive(true);
        // For timeDuration time...
        while (time < timeDuration) {
            time += Time.deltaTime;
            // Increase scale
            this.circleObject.localScale = new Vector3(this.circleObject.localScale.x + increaseAmount, this.circleObject.localScale.y + increaseAmount, this.circleObject.localScale.z);
            yield return null;
        }
        // hide circle
        this.circleObject.gameObject.SetActive(false);
    }
  /*public override void useAbility(){
	  StartCoroutine(this.OnTriggerEnter2D());
  }
   void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "EnemyBullet") {
      Bullet bulletScript = other.transform.parent.GetComponent<Bullet>();
      bulletScript.DestroyBullet();
    }
  }*/
}