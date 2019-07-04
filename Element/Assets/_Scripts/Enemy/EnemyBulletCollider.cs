using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script that blocks Enemy bullets.
// Useful for abilities
public class EnemyBulletCollider : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D other) {
    if (other.tag == "EnemyBullet") {
      Bullet bulletScript = other.transform.parent.GetComponent<Bullet>();
      bulletScript.DestroyBullet();
    }
  }
}
