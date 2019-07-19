using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// Class that handles the "Animation" that appears when you get enough particles
public class ParticleTransformer : MonoBehaviour
{
  // Variables for tweeking
  public float fullAlpha = 0.45f;
  public float transitionSpeed = 1f;
  public float displayTime = 5f;
  public bool isTransforming{get; private set;}

  // Variables for screen info:
  public CanvasGroup fadeInGroup;
  public RectTransform particleImageContainer;
  private GameObject curTransformObject;
  
  public Text commonNameText;
  public Text formulaText;
  public Text descriptionText;

  public void TransformParticle()
  {

    if (this.isTransforming) return;

    PlayerAbilityController abilityController = GameStateManager.Instance.game.player.playerAbility;
    // Transform the particle, and extract the image information to the particleImageContainer
    PlayerAbilityBase info = GameStateManager.Instance.game.player.playerAbility.TransformParticle(true);
    GameObject particleImage =  Instantiate(info.canvasVisualsPrefab, this.particleImageContainer.transform);
    particleImage.transform.SetParent(this.particleImageContainer.transform);
    this.curTransformObject = particleImage;

    this.commonNameText.text = info.commonName;
    this.formulaText.text = info.chemicalName;
    this.descriptionText.text = info.description;
    GameStateManager.Instance.game.backgroundHandler.ChangeColor(true);
    
    // Animation effect
    StartCoroutine(this.transformCor());
  }


  // Animation that fades in the black background, does the transformation, and then fades out
  private IEnumerator transformCor()
  {
    float t = 0;
    this.fadeInGroup.gameObject.SetActive(true);
    this.isTransforming = true;
    while (t <= 1)
    {
      t += transitionSpeed * Time.deltaTime;
      this.fadeInGroup.alpha = Mathf.Lerp(0, this.fullAlpha, t);
      yield return null;
    }

    GameStateManager.Instance.game.UpdatePoints(0);

    yield return new WaitForSeconds(this.displayTime);

    while (t > 0)
    {
      t -= transitionSpeed * Time.deltaTime;
      this.fadeInGroup.alpha = Mathf.Lerp(0, this.fullAlpha, t);
      yield return null;
    }
    yield return null;
    Destroy(this.curTransformObject);
    this.fadeInGroup.gameObject.SetActive(false);
    this.isTransforming = false;
  }
}
