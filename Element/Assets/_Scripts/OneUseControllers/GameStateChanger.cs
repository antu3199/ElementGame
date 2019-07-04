using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple controller that helps change the GameStateManager's state.
public class GameStateChanger : MonoBehaviour
{
  public GAME_STATE stateTo;
  void Start()
  {
    GameStateManager.Instance.changeState(stateTo);
  }
}
