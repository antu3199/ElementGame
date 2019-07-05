using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
  // Specifies the current game state:
  public GAME_STATE gameState { get; private set; }
  public GameController game {get; set;}
  public AndroidUtils recorder;

  void Update() {
    switch (this.gameState) {
      case GAME_STATE.GAME:
      if (this.game) {
        this.game.DoUpdate();
      }
      break;
    }
  }
  
  public void changeState(GAME_STATE stateTo)
  {
    if (this.gameState == stateTo)
    {
      return;
    }
    this.gameState = stateTo;
    switch (this.gameState)
    {
      case GAME_STATE.GAME:
      break;
      // TODO (maybe not even needed)
    }
  }
}
