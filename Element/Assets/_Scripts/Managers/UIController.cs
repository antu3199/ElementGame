using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    
	public ParticleTransformer particleTransformer;
	public Slider slider;
    public Slider healthSlider;

	void Start () {
		GameStateManager.Instance.ui = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
