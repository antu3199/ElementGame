using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class that acts as a bridge for the model and other player scripts
public class Player : MonoBehaviour, ExplicitInterface
{
  // Contains References to other player classes
  public PlayerCollision playerCollision;
  public PlayerMovement playerMovement;
  public PlayerAbilityController playerAbility;

  public bool canMove = true;
  
  // Particle system that plays when the player is destroyed.
  public ParticleSystem deathParticles;

  public void DoUpdate()
  {
    if (this.canMove) {
      this.playerMovement.DoUpdate();
    }

    if (Input.GetKeyDown(KeyCode.Q)) {
      this.setCanMove(false);
    }
  }
  
  // Play the particle system when player is destroyed.
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
