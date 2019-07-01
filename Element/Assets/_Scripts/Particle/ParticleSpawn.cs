using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour, ExplicitInterface {
  
  public Transform player;
	public PooledObject particle;
	float randX, randY;
	Vector2 whereToSpawn;
	public float spawnRate = 2f;
	float nextSpawn = 0.0f;

	public void DoUpdate () {
		if (Time.time > nextSpawn){
			nextSpawn = Time.time + spawnRate;
			randX = Random.Range(-9.0f,9.0f);
			randY = Random.Range(-4.3f,4.3f);
			whereToSpawn = player.position + new Vector3 (randX, randY, 1);

      PooledObject particle = this.particle.GetPooledInstance<PooledObject>(whereToSpawn);
		}
	}
}
