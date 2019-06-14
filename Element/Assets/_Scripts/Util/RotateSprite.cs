using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour {

  [SerializeField] private float rotationSpeed;
	
  private int rotateDir;

	
  void Start() {
     this.rotateDir = Random.Range(0, 2) == 0 ? 1 : -1;
  }

	void Update () {
		this.transform.Rotate(0, 0, this.rotateDir * this.rotationSpeed * Time.deltaTime);
	}
}
