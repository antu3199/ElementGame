using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
  public CircleCollider2D playerCollider;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "Enemy") {
      // Do enemy collision
      // Nothing for now.
    } else if (other.tag == "Particle") {
      // Do particle point thing
            PooledObject asdf = other.gameObject.GetComponent<PooledObject>();
            asdf.ReturnToPool();
      GameStateManager.Instance.player.playerPoints.UpdatePoint(1);
    } else if (other.tag == "EnemyBullet") {
      Bullet bulletScript = other.transform.parent.GetComponent<Bullet>();
      bulletScript.DestroyBullet();
      GameStateManager.Instance.player.playerHealth.updateHealth(-1);
    }
  }
}
