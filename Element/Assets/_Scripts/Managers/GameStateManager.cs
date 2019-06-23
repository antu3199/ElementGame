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

  // Where we will store the current "particle points"
  public float particlePoints;
  public float particlePointsToNextLevel;
  // Reference to the player. If not in game, this is null
  // [HideInInspector] // Should hide in inspector, but may be useful for debugging
  public Player player;
  public UIController ui;

  // Specifies the current game state:
  public GAME_STATE gameState { get; private set; }

  public float maxHealth = 10;
  public float health = 10;
   
  public float score = 0;
  public float scoreIncreaseSpeed = 10f;

  public float timeBeforeGoBackToMainMenu = 5f;
  
  void Update() {
    if (this.gameState == GAME_STATE.GAME) {
      this.score += this.scoreIncreaseSpeed * Time.deltaTime;
      this.ui.setScoreText(this.score);
    }
  }
  

  private void resetGame() {
    this.score = 0;
    this.health = 10;
    this.particlePoints = 0;
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
        this.resetGame();
      break;
      // TODO (maybe not even needed)
    }
  }

  public void UpdatePoints(int delta) {
    if (this.ui.particleTransformer.isTransforming == true && particlePoints + 1 > this.particlePointsToNextLevel) return;
    
    this.particlePoints += delta;
    this.ui.pointsSlider.value = Mathf.Clamp(this.particlePoints / this.particlePointsToNextLevel, 0, 1);
    if (this.particlePoints >= this.particlePointsToNextLevel) {
      this.particlePoints = 0;
      this.score += 100;
      this.ui.setScoreText(this.score);
      this.ui.particleTransformer.TransformParticle();
    }
    this.ui.setPointsSliderText(this.particlePoints, this.particlePointsToNextLevel);
  }

	public void updateHealth(float deltaHealth) {
		this.health = Mathf.Clamp(this.health + deltaHealth, 0, this.maxHealth);
    float t = health / maxHealth;
    this.ui.healthSlider.value = t;
    this.ui.setHealthSliderText(health, maxHealth);

    if (this.health == 0) {
      this.player.setCanMove(false);
      StartCoroutine(this.GoToMainMenuAfterTime(this.timeBeforeGoBackToMainMenu));
    }
	}
  

  IEnumerator GoToMainMenuAfterTime(float time) {
    yield return new WaitForSeconds(time);
    this.GoBackToMainMenu();
  }
  
  public void GoBackToMainMenu() {
    SceneManager.LoadScene("MainMenu");
  }

}
