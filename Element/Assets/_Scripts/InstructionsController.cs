using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controller for instructions.
public class InstructionsController : MonoBehaviour {
  
  public int curSlide = 0;
  public List<RectTransform> slides;
  public RectTransform nextButton;
  public RectTransform prevButton;

  void Start() {
    this.checkLimits();
  }

  public void PageForward() {
    this.slides[curSlide].gameObject.SetActive(false);
    curSlide = Mathf.Clamp(curSlide + 1, 0, slides.Count-1);
    this.slides[curSlide].gameObject.SetActive(true);
    this.checkLimits();
  }

  public void PageBackwards() {
    this.slides[curSlide].gameObject.SetActive(false);
    curSlide = Mathf.Clamp(curSlide - 1, 0, slides.Count-1);
    this.slides[curSlide].gameObject.SetActive(true);
    this.checkLimits();
  }

  private void checkLimits() {
    this.prevButton.gameObject.SetActive(this.curSlide != 0);
    this.nextButton.gameObject.SetActive(this.curSlide != this.slides.Count-1);
  }
}
