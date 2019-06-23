using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PARTICLE_TYPES
{
  BASE = 0,
  WATER = 1,
  CORNSTARCH = 2,
  GOLD = 3,
  NICKEL = 4,
  ALCOHOL = 5,
}
public abstract class PlayerAbilityBase : MonoBehaviour
{
  public abstract PARTICLE_TYPES type { get; }
  public abstract string commonName { get; }
  public abstract string chemicalName { get; }
  public abstract string description { get; }
  public abstract bool useableAbility { get; }
  public float abilityCooldownDuration;
  public GameObject visuals;
  public GameObject canvasVisualsPrefab;
  public GameObject cooldownVisualsPrefab;
  
  public bool abilityAvailable = true;
  public float curCooldown { get; set; }

  public void Update()
  {
    if (this.abilityAvailable)
    {

      this.curCooldown = Mathf.Clamp(this.curCooldown - Time.deltaTime, 0, this.abilityCooldownDuration);
      GameStateManager.Instance.ui.setAbilityCooldown(this.curCooldown, this.abilityCooldownDuration);

      if ((Input.touchCount == 2 || Input.GetKeyDown(KeyCode.Space)) && this.useableAbility && this.curCooldown <= 0)
      {
        this.useAbility();
        this.curCooldown = this.abilityCooldownDuration;
      }
    }
  }
  public virtual void useAbility() { }
  public virtual void setAbilityActive(bool val)
  {
    this.abilityAvailable = val;
    if (val == false)
    {
      this.visuals.SetActive(false);
    }
  }
}
