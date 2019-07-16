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
  ACETONE = 6,
}


// Base class for abilities:
public abstract class PlayerAbilityBase : MonoBehaviour
{
  // Type of particle (may not be needed)
  public abstract PARTICLE_TYPES type { get; }
  // Common name, in English
  public abstract string commonName { get; }
  // Chemical name (molecular structure)
  public abstract string chemicalName { get; }
  // Description + mechanics used for the transformation description
  public abstract string description { get; }
  // bool stating whether or not it is a useable ability
  public abstract bool useableAbility { get; }
  // time it takes to be able to use it again
  public float abilityCooldownDuration;
  // Reference to the ability visuals
  public GameObject visuals;
  // Reference to the "Transformation" visuals
  public GameObject canvasVisualsPrefab;
  // Reference to the "cooldown" visuals
  public GameObject cooldownVisualsPrefab;

  // flag for ability available
  public bool abilityAvailable = true;
  // current counter for ability cooldown
  public float curCooldown { get; set; }

  // Every frame, check cooldowns, and check if you activate it
  public void Update()
  {
    if (this.abilityAvailable)
    {

      this.curCooldown = Mathf.Clamp(this.curCooldown - Time.deltaTime, 0, this.abilityCooldownDuration);
      GameStateManager.Instance.game.ui.setAbilityCooldown(this.curCooldown, this.abilityCooldownDuration);

      if ((Input.touchCount == 2 || Input.GetKeyDown(KeyCode.Space)) && this.useableAbility && this.curCooldown <= 0)
      {
        this.useAbility();
        this.curCooldown = this.abilityCooldownDuration;
      }
    }
  }
  public virtual void useAbility() { }

  // Can you use abilities?
  public virtual void setAbilityActive(bool val)
  {
    this.abilityAvailable = val;
    if (val == false)
    {
      this.visuals.SetActive(false);
    }
  }
}
