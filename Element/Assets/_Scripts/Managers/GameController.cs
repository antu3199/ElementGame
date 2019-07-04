using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, ExplicitInterface
{

  // References to other classes
  public Player player;
  public UIController ui;
  public CameraFollow cameraMover;
  public BackgroundLayerSpawner backgroundHandler;
  public ParticleSpawn particleSpawner;

  // Score & stuff
  public float particlePoints = 0;
  public float particlePointsToNextLevel = 3;
  public float maxHealth = 10;
  public float health = 10;

  public float score = 0;
  public float scoreIncreaseSpeed = 1f;

  // Other stuff
  public float timeBeforeGoBackToMainMenu = 5f;

  private List<ExplicitInterface> observers = new List<ExplicitInterface>();

  void Awake()
  {
    GameStateManager.Instance.game = this;
    observers.Add(this.player);
    observers.Add(this.ui);
    observers.Add(cameraMover);
    observers.Add(backgroundHandler);
    observers.Add(particleSpawner);
  }

  public void DoUpdate()
  {
    this.score += this.scoreIncreaseSpeed * Time.deltaTime;
    this.ui.setScoreText(this.score);

    // Update observers
    foreach (var observer in this.observers)
    {
      observer.DoUpdate();
    }
  }

  private void resetGame()
  {
    this.score = 0;
    this.health = 10;
    this.particlePoints = 0;
  }

  // Update points
  public void UpdatePoints(int delta)
  {
    if (this.ui.particleTransformer.isTransforming == true && particlePoints + 1 > this.particlePointsToNextLevel) return;

    this.particlePoints += delta;
    this.ui.pointsSlider.value = Mathf.Clamp(this.particlePoints / this.particlePointsToNextLevel, 0, 1);
    if (this.particlePoints >= this.particlePointsToNextLevel)
    {
      this.particlePoints = 0;
      this.score += 100;
      this.ui.setScoreText(this.score);
      this.ui.particleTransformer.TransformParticle();
    }
    this.ui.setPointsSliderText(this.particlePoints, this.particlePointsToNextLevel);
  }

  // Update health
  public void updateHealth(float deltaHealth)
  {
    this.health = Mathf.Clamp(this.health + deltaHealth, 0, this.maxHealth);
    float t = health / maxHealth;
    this.ui.healthSlider.value = t;
    this.ui.setHealthSliderText(health, maxHealth);

    if (this.health == 0)
    {
      this.player.setCanMove(false);
      StartCoroutine(this.GoToMainMenuAfterTime(this.timeBeforeGoBackToMainMenu));
    }
  }

  // Coroutine to go back to the main menu after a set amount of time
  private IEnumerator GoToMainMenuAfterTime(float time)
  {
    yield return new WaitForSeconds(time);
    this.GoBackToMainMenu();
  }

  public void GoBackToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }
}
