using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PARTICLE_TYPES
{
  BASE = 0,
  WATER = 1,
  CORNSTARCH = 2
}
public class ParticleTransformer : MonoBehaviour {

    // TODO: Add prefab for each particle instead...
	public Image particleTransformInto;
    public CanvasGroup fadeInGroup;
	public float fullAlpha;
	public float transitionSpeed;
	public float displayTime;
	private bool isTransforming = false;

	void Start () {
		GameStateManager.Instance.particleTransformer = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			this.TransformParticle();
		}
	}

	public void TransformParticle() {

		if (this.isTransforming) return;

		PARTICLE_TYPES transformInto = PARTICLE_TYPES.WATER;
	    
		// Set particle info here...
		GameStateManager.Instance.player.playerTransformationAnim.PlayParticleTransformationAnimation();

		StartCoroutine(this.transformCor());
	}

	private IEnumerator transformCor() {
		float t = 0;
		this.fadeInGroup.gameObject.SetActive(true);
		this.isTransforming = true;
		while (t <= 1) {
			t += transitionSpeed * Time.deltaTime; 
	    	this.fadeInGroup.alpha = Mathf.Lerp(0, this.fullAlpha, t);
			yield return null;
		}

		yield return new WaitForSeconds(this.displayTime);

		while (t > 0) {
			t -= transitionSpeed * Time.deltaTime; 
	    	this.fadeInGroup.alpha = Mathf.Lerp(0, this.fullAlpha, t);
			yield return null;
		}
		yield return null;
		this.fadeInGroup.gameObject.SetActive(false);
		this.isTransforming = false;
	} 
}
