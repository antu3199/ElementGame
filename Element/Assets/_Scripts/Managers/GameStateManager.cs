using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
  MENUS,
  GAME,
  GAME_PAUSE,
  GAME_STOP
}
// Construct a singleton instance of this Object
public class GameStateManager : Singleton<GameStateManager>
{

  // Where we will store the current "particle points"
  public float particlePoints;
  // Reference to the player. If not in game, this is null
  // [HideInInspector] // Should hide in inspector, but may be useful for debugging
  public Player player;

  // Specifies the current game state:
  public GAME_STATE gameState { get; private set; }

  public void changeState(GAME_STATE stateTo)
  {
    if (this.gameState == stateTo)
    {
      return;
    }
    this.gameState = stateTo;
    switch (this.gameState)
    {
      // TODO (maybe not even needed)
    }
  }
}
