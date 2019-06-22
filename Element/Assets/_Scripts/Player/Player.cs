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
  public PlayerHealth playerHealth;
  public PlayerMovement playerMovement;
  public PlayerPoints playerPoints;
  void Awake()
  {
    GameStateManager.Instance.player = this;
  }

  void OnDestroy()
  {
    if (GameStateManager.Instance != null)
    {
      GameStateManager.Instance.player = null;
    }
  }
}
