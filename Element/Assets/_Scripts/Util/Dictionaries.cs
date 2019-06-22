using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionaries : Singleton<Dictionaries> {

	public List<PlayerAbilityBase> abilityPrefabs;

	public PlayerAbilityBase getRandAbilityPrefab() {
		int randIndex = Random.Range(0, this.abilityPrefabs.Count);
		PlayerAbilityBase abilityInfo = abilityPrefabs[randIndex];
		return abilityInfo;
	}
}
