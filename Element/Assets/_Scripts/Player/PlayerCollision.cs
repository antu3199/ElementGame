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
      PooledObject particle = other.gameObject.GetComponent<PooledObject>();
      particle.ReturnToPool();
      GameStateManager.Instance.player.playerPoints.UpdatePoint(1);
      print("Points: " + GameStateManager.Instance.player.playerPoints.points);
      GameStateManager.Instance.ui.slider.value = System.Math.Min(1f,
          GameStateManager.Instance.player.playerPoints.points / GameStateManager.Instance.player.playerPoints.maxPoints);
    } else if (other.tag == "EnemyBullet") {
      Bullet bulletScript = other.transform.parent.GetComponent<Bullet>();
      bulletScript.DestroyBullet();
      GameStateManager.Instance.player.playerHealth.updateHealth(-1);
      print("Player's Health: " + GameStateManager.Instance.player.playerHealth.health);
      GameStateManager.Instance.ui.healthSlider.value = System.Math.Max(0f,
          GameStateManager.Instance.player.playerHealth.health / GameStateManager.Instance.player.playerHealth.maxHealth);
    }
  }
}
