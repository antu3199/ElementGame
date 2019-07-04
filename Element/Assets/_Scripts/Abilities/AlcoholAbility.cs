using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// OH ability that "cleanses" the surrounding area
public class AlcoholAbility : PlayerAbilityBase {
  public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.ALCOHOL; }  }
	public override string commonName { get { return "Alcohol"; }  }
	public override string chemicalName { get { return "OH"; }  }
  public override string description { get { 
    return "Description: Alcohol is a family of compounds that have OH attached to the carbon molecule.\n\n Gameplay: Tap the screen with two fingers to destory the surrounding bullets (on a cooldown)";
  }}

  public override bool useableAbility { get { return true; } }

    public Transform circleObject;
    public float increaseAmount = 0.3f;
    public float timeDuration = 0.5f;

    public override void useAbility() {
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
}

