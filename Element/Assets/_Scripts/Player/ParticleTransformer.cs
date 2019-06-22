using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ParticleTransformer : MonoBehaviour
{

  // TODO: Add prefab for each particle instead...
  public CanvasGroup fadeInGroup;
  public float fullAlpha;
  public float transitionSpeed;
  public float displayTime;
  private bool isTransforming = false;

  // Variables for screen info:
  public RectTransform particleImageContainer;
  private GameObject curTransformObject;
  
  public Text commonNameText;
  public Text formulaText;
  public Text descriptionText;

  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.A))
    {
      this.TransformParticle();
    }
  }

  public void TransformParticle()
  {

    if (this.isTransforming) return;

    PlayerAbilityController abilityController = GameStateManager.Instance.player.playerAbility;
    // Set particle info here...
    PlayerAbilityBase info = GameStateManager.Instance.player.playerAbility.TransformParticle();
    GameObject particleImage =  Instantiate(info.canvasVisualsPrefab, this.particleImageContainer.transform);
    particleImage.transform.SetParent(this.particleImageContainer.transform);
    this.curTransformObject = particleImage;

    this.commonNameText.text = info.commonName;
    this.formulaText.text = info.chemicalName;
    this.descriptionText.text = info.description;
    
    Debug.Log(info.commonName);

    StartCoroutine(this.transformCor());
  }

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
