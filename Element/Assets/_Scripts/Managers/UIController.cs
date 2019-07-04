using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Class that contains references to UI objects
// Also handles updating the UI objects to be displayed on the screen
public class UIController : MonoBehaviour, ExplicitInterface
{

  // Reference to ParticleTransformer object, which is opened when you "transform"
  public ParticleTransformer particleTransformer;
  // Progressbar references
  public Slider pointsSlider;
  public Slider healthSlider;
  
  // Text references
  public Text healthSliderText;
  public Text pointsSliderText;
  public Text scoreText;

  // Ability stuff 
  public RectTransform abilityCooldown;
  public RectTransform abilityIndicator;
  public RectTransform abilityCooldownSprite;
  private GameObject curAbilitySprite;


  void Start()
  {
    GameStateManager.Instance.game.UpdatePoints(0);
    GameStateManager.Instance.game.updateHealth(0);
  }

  public void DoUpdate() {}

  // Sets point slider text
  public void setPointsSliderText(float cur, float max)
  {
    this.pointsSliderText.text = this.formatFract(cur, max);
  }

  // Sets health slider text
  public void setHealthSliderText(float cur, float max)
  {
    this.healthSliderText.text = this.formatFract(cur, max);
  }

  // Sets score text
  public void setScoreText(float score)
  {
    int scoreInt = Mathf.RoundToInt(score);
    this.scoreText.text = "Score: " + scoreInt;
  }


  // Sets ability cooldown animation
  public void setAbilityCooldown(float cur, float max)
  {
    float t = Mathf.Clamp(cur, 0, max) / max;
    float newSize = abilityIndicator.sizeDelta.y * t;
    this.abilityCooldown.sizeDelta = new Vector2(abilityIndicator.sizeDelta.x, newSize);
    float sizeDelta = this.abilityIndicator.sizeDelta.y - newSize;
    this.abilityCooldown.anchoredPosition = new Vector2(this.abilityCooldown.anchoredPosition.x, - sizeDelta/2);
  }

  // Sets whether or not the ability indicator is visible
  public void setAbilityAvailable(bool available) {
    this.abilityIndicator.gameObject.SetActive(available);
  }

  // Sets the ability cooldown icon
  public void setAbilityCooldownIcon(GameObject sprite) {
    if (this.curAbilitySprite != null) {
      Destroy(this.curAbilitySprite);
    }

    GameObject spriteObject = Instantiate(sprite, abilityCooldownSprite.transform);
    spriteObject.transform.position = abilityCooldownSprite.transform.position;
    spriteObject.transform.SetParent(abilityCooldownSprite.transform);
    this.curAbilitySprite = spriteObject;
  }

  // Helper function for formatting a fraction
  private string formatFract(float cur, float max)
  {
    return cur + "/" + max;
  }

}
