using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour {

	public GameObject objectToDisappear;
	// Use this for initialization
	public void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	public void changeScene(string scene){
		// Change scene
		SceneManager.LoadScene(scene);
	}

	public void makeObjectDisappear(){
		this.objectToDisappear.SetActive(false);
	}
}
