using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAbilityController : MonoBehaviour {
	public List<PlayerAbilityBase> abilityPrefabs;
	public PlayerAbilityBase ability;
	public ParticleSystem particleS;
	
    public float animationDuration = 1.0f;
	public float timeBeforeChange = 1f;

	void Start() {
       this.TransformParticle();
	}

	public PlayerAbilityBase TransformParticle(bool anim = false) {
	   int randIndex = this.GetRandAbilityIndex();
	   PlayerAbilityBase abilityInfo = abilityPrefabs[randIndex];
	   if (anim) {
         this.PlayParticleTransformationAnimation(randIndex);
	   } else {
		   this.ChangeParticle(randIndex);
	   }
	   return abilityInfo;
	}

	public int GetRandAbilityIndex() {
		return Random.Range(0, this.abilityPrefabs.Count);
	}

	private void PlayParticleTransformationAnimation(int index) {
       StartCoroutine(particleAnimation(this.animationDuration, index));
	}

	IEnumerator particleAnimation(float time, int index) {
       this.particleS.Play();
	   yield return new WaitForSeconds(timeBeforeChange);

	   this.GetComponent<SpriteRenderer>().color = Color.blue;
	   this.ChangeParticle(index);

	   yield return new WaitForSeconds(animationDuration);
	   this.particleS.Stop();
	}

	private void ChangeParticle(int index) {
		if (this.ability != null) {
		  Destroy(this.ability.gameObject);
		}
		
		PlayerAbilityBase newAbility = Instantiate<PlayerAbilityBase>(this.abilityPrefabs[index]);
		newAbility.transform.SetParent(this.transform);
		this.ability = newAbility;
	}
}
