using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles player collision using Unity's "Trigger" system.
public class PlayerCollision : MonoBehaviour {

  public bool canCollide = true;
  public CircleCollider2D playerCollider;

  public bool hasArmor = false;
  
  void OnTriggerEnter2D(Collider2D other) {
    if (!canCollide) return;
     
    // Enemy collision
    if (other.tag == "Enemy") {

    } else if (other.tag == "Particle") {
      // Add points
      PooledObject particle = other.gameObject.GetComponent<PooledObject>();
      particle.ReturnToPool();
      GameStateManager.Instance.game.UpdatePoints(1);
   
    } else if (other.tag == "EnemyBullet") {
      // Lower health
      Bullet bulletScript = other.transform.parent.GetComponent<Bullet>();
      bulletScript.DestroyBullet();
      if(!hasArmor) GameStateManager.Instance.game.updateHealth(-1);
    }
  }
}
