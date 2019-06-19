using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour {

	public GameObject particle;
	float randX, randY;
	Vector2 whereToSpawn;
	public float spawnRate = 2f;
	float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn){
			nextSpawn = Time.time + spawnRate;
			randX = Random.Range(-9.0f,9.0f);
			randY = Random.Range(-4.3f,4.3f);
			whereToSpawn = new Vector2 (randX, randY);
			Instantiate (particle, whereToSpawn, Quaternion.identity);
		}
	}
}
