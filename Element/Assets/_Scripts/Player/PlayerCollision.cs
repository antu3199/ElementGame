using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
  public CircleCollider2D playerCollider;

  void onTriggerEnter2D(Collider2D other) {
    if (other.tag == "Enemy") {
      // Do enemy collision
    } else if (other.tag == "Point") {
      // Do particle point thing
    }
  }
}
