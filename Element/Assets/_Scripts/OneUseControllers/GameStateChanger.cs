using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
  public GAME_STATE stateTo;
  // Use this for initialization
  void Start()
  {
    GameStateManager.Instance.changeState(stateTo);
  }
}
