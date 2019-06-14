using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    void Start()
    {
		this.health = maxHealth;
    }

	public void updateHealth(float deltaHealth) {
		this.health = Mathf.Clamp(this.health + deltaHealth, 0, this.maxHealth);
		Debug.Log("Health is now: " + this.health);
	}
}
