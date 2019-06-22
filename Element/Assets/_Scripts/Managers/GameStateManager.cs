﻿using System.Collections;
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
  public float particlePointsToNextLevel;
  // Reference to the player. If not in game, this is null
  // [HideInInspector] // Should hide in inspector, but may be useful for debugging
  public Player player;
  public UIController ui;

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

  public void IncreasePoints(int delta) {
    this.particlePoints += delta;
    if (this.particlePoints >= this.particlePointsToNextLevel) {
      this.particlePoints = 0;
      this.ui.particleTransformer.TransformParticle();
    }
  }

}
