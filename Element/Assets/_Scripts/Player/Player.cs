using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class that acts as a bridge for the model and other player scripts

// Requires that this component be added
[RequireComponent(typeof(PlayerCollision))
//,(typeof(AnyOtherClass))
]
public class Player : MonoBehaviour
{
  public PlayerCollision playerCollision;
  public PlayerMovement playerMovement;
  public PlayerAbilityController playerAbility;

  public bool canMove = true;

  public ParticleSystem deathParticles;

  void Update()
  {
    if (this.canMove) {
      this.playerMovement.PlayerUpdate();
    }

    if (Input.GetKeyDown(KeyCode.Q)) {
      this.setCanMove(false);
    }
  }


  public void setCanMove(bool move) {
    this.playerCollision.canCollide = move;
    this.playerMovement.SetCanMove(move);
    this.canMove = move;
    this.playerAbility.SetAbilityActive(move);
    if (move == false) {
      this.deathParticles.Play();
    }
  }
}
