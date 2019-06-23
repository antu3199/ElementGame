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

  void Start()
  {
    GameStateManager.Instance.ui = this;
    GameStateManager.Instance.UpdatePoints(0);
    GameStateManager.Instance.updateHealth(0);
  }

  public void setPointsSliderText(float cur, float max) {
     this.pointsSliderText.text = this.formatFract(cur, max);
  }

  public void setHealthSliderText(float cur, float max) {
    this.healthSliderText.text = this.formatFract(cur, max);
  }

  public void setScoreText(float score) {
     int scoreInt = Mathf.RoundToInt(score);
     this.scoreText.text = "Score: " + scoreInt;
  }

  private string formatFract(float cur, float max) {
    return cur + "/" + max;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
