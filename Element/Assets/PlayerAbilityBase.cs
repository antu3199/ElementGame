using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour {
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			this.useAbility();
		}
	}
	public virtual void useAbility() {}
}
