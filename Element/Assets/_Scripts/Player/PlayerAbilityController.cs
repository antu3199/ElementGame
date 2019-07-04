using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class that manages a reference to the ability, and how it "transforms"
public class PlayerAbilityController : MonoBehaviour
{

  // Reference to ability. Can be null.
  public PlayerAbilityBase ability;

  // Particle System for the "transformation" animation
  public ParticleSystem particleS;

  // Time before it trasnforms
  public float animationDuration = 1.0f;
  public float timeBeforeChange = 1f;

  void Start()
  {
    this.TransformParticle();
  }
  

  // Transforms into a random particle
  public PlayerAbilityBase TransformParticle(bool anim = false)
  {
    PlayerAbilityBase abilityInfo = Dictionaries.Instance.getRandAbilityPrefab();
    if (anim)
    {
      this.PlayParticleTransformationAnimation(abilityInfo);
    }
    else
    {
      this.ChangeParticle(abilityInfo);
    }
    return abilityInfo;
  }
 
  // Play particle transformation animation
  private void PlayParticleTransformationAnimation(PlayerAbilityBase ability)
  {
    StartCoroutine(particleAnimation(this.animationDuration, ability));
  }

  // Does the particle animation
  private IEnumerator particleAnimation(float time, PlayerAbilityBase ability)
  {
    this.particleS.Play();
    yield return new WaitForSeconds(timeBeforeChange);

    this.GetComponent<SpriteRenderer>().color = Color.blue;
    this.ChangeParticle(ability);

    yield return new WaitForSeconds(animationDuration);
    this.particleS.Stop();
  }

  // Change the particle - Destroy the previous ability, and then create the new one.
  private void ChangeParticle(PlayerAbilityBase ability)
  {
    if (this.ability != null)
    {
      Destroy(this.ability.gameObject);
    }

    PlayerAbilityBase newAbility = Instantiate<PlayerAbilityBase>(ability, this.transform);
    newAbility.transform.SetParent(this.transform);
    this.ability = newAbility;
    GameStateManager.Instance.game.ui.setAbilityAvailable(newAbility.useableAbility);
    GameStateManager.Instance.game.ui.setAbilityCooldownIcon(newAbility.cooldownVisualsPrefab);
  }


  // Disable if necessary.
  public void SetAbilityActive(bool val)
  {
    if (this.ability)
    {
      this.ability.setAbilityActive(val);
    }
  }
}
