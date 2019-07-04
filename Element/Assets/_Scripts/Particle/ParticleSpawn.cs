using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that randomly spawns particles around the player
public class ParticleSpawn : MonoBehaviour, ExplicitInterface {
  
  public Transform player;
	public PooledObject particle;
	float randX, randY;
	private Vector2 whereToSpawn;
	public float spawnRate = 2f;
	float nextSpawn = 0.0f;

	public void DoUpdate () {
		if (Time.time > nextSpawn){
			nextSpawn = Time.time + spawnRate;
      // Compute a random x and Y value
			randX = Random.Range(-9.0f,9.0f);
			randY = Random.Range(-4.3f,4.3f);
			whereToSpawn = player.position + new Vector3 (randX, randY, 1);
      // Spawn the particle.
      PooledObject particle = this.particle.GetPooledInstance<PooledObject>(whereToSpawn);
		}
	}
}
