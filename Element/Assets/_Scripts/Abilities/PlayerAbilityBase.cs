using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PARTICLE_TYPES
{
  BASE = 0,
  WATER = 1,
  CORNSTARCH = 2
}
public abstract class PlayerAbilityBase : MonoBehaviour
{
  public abstract PARTICLE_TYPES type { get; }
  public abstract string commonName { get; }
  public abstract string chemicalName { get; }
  public abstract string description { get; }
  public GameObject visuals;
  public GameObject canvasVisualsPrefab;
  public bool abilityAvailable = true;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && this.abilityAvailable)
    {
      this.useAbility();
    }
  }
  public virtual void useAbility() { }
  public virtual void setAbilityActive(bool val) {
    this.abilityAvailable = val;
    if (val == false) {
      this.visuals.SetActive(false);
    }
  }
}
