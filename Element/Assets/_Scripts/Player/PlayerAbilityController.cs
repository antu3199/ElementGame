using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAbilityController : MonoBehaviour
{

  public PlayerAbilityBase ability;
  public ParticleSystem particleS;

  public float animationDuration = 1.0f;
  public float timeBeforeChange = 1f;

  void Start()
  {
    this.TransformParticle();
  }

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

  private void PlayParticleTransformationAnimation(PlayerAbilityBase ability)
  {
    StartCoroutine(particleAnimation(this.animationDuration, ability));
  }

  IEnumerator particleAnimation(float time, PlayerAbilityBase ability)
  {
    this.particleS.Play();
    yield return new WaitForSeconds(timeBeforeChange);

    this.GetComponent<SpriteRenderer>().color = Color.blue;
    this.ChangeParticle(ability);

    yield return new WaitForSeconds(animationDuration);
    this.particleS.Stop();
  }

  private void ChangeParticle(PlayerAbilityBase ability)
  {
    if (this.ability != null)
    {
      Destroy(this.ability.gameObject);
    }

    PlayerAbilityBase newAbility = Instantiate<PlayerAbilityBase>(ability, this.transform);
    newAbility.transform.SetParent(this.transform);
    this.ability = newAbility;
    GameStateManager.Instance.ui.setAbilityAvailable(newAbility.useableAbility);
  }

  public void SetAbilityActive(bool val)
  {
    if (this.ability)
    {
      this.ability.setAbilityActive(val);
    }
  }
}
