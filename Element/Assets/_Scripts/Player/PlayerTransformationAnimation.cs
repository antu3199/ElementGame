using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationAnimation : MonoBehaviour {
    
	public ParticleSystem particleS;
	
    public float animationDuration = 1.0f;
	public float timeBeforeChange = 1f;

	public void PlayParticleTransformationAnimation() {
       StartCoroutine(particleAnimation(this.animationDuration));
	}

	IEnumerator particleAnimation(float time) {
       this.particleS.Play();
	   yield return new WaitForSeconds(timeBeforeChange);

	   this.GetComponent<SpriteRenderer>().color = Color.blue;

	   yield return new WaitForSeconds(animationDuration);
	   this.particleS.Stop();
	}
}
