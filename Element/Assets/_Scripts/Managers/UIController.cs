using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

  public ParticleTransformer particleTransformer;
  public Slider pointsSlider;
  public Slider healthSlider;

  public Text healthSliderText;
  public Text pointsSliderText;
  public Text scoreText;

  public RectTransform abilityCooldown;
  public RectTransform abilityIndicator;

  void Awake() {
    GameStateManager.Instance.ui = this;
  }

  void Start()
  {
    GameStateManager.Instance.UpdatePoints(0);
    GameStateManager.Instance.updateHealth(0);
  }

  public void setPointsSliderText(float cur, float max)
  {
    this.pointsSliderText.text = this.formatFract(cur, max);
  }

  public void setHealthSliderText(float cur, float max)
  {
    this.healthSliderText.text = this.formatFract(cur, max);
  }

  public void setScoreText(float score)
  {
    int scoreInt = Mathf.RoundToInt(score);
    this.scoreText.text = "Score: " + scoreInt;
  }

  private string formatFract(float cur, float max)
  {
    return cur + "/" + max;
  }


  public void setAbilityCooldown(float cur, float max)
  {
    float t = Mathf.Clamp(cur, 0, max) / max;
    float newSize = abilityIndicator.sizeDelta.y * t;
    this.abilityCooldown.sizeDelta = new Vector2(abilityIndicator.sizeDelta.x, newSize);
    float sizeDelta = this.abilityIndicator.sizeDelta.y - newSize;
    this.abilityCooldown.anchoredPosition = new Vector2(this.abilityCooldown.anchoredPosition.x, - sizeDelta/2);
  }

  public void setAbilityAvailable(bool available) {
    this.abilityIndicator.gameObject.SetActive(available);
  }
}
