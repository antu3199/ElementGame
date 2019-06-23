using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

  public bool canCollide = true;
  public CircleCollider2D playerCollider;

  public bool hasArmor = false;

  void OnTriggerEnter2D(Collider2D other) {
    if (!canCollide) return;

    if (other.tag == "Enemy") {
      // Do enemy collision
      // Nothing for now.
    } else if (other.tag == "Particle") {
      // Do particle point thing
      PooledObject particle = other.gameObject.GetComponent<PooledObject>();
      particle.ReturnToPool();
      GameStateManager.Instance.UpdatePoints(1);
   
    } else if (other.tag == "EnemyBullet") {
      Bullet bulletScript = other.transform.parent.GetComponent<Bullet>();
      bulletScript.DestroyBullet();
       
      if(!hasArmor) GameStateManager.Instance.updateHealth(-1);

    }
  }
}
