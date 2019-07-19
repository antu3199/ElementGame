using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create "armor" that blocks a few bullets
public class PhenolphthaleinAbility : PlayerAbilityBase
{
    public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.PHENO; } }
    public override string commonName { get { return "Phenolphthalein"; } }
    public override string chemicalName { get { return "((C)20(H)14(O)4)n"; } }
    public override string description
    {
        get
        {
            return "Description: Phenolphthalein is an organic compound that is often used in pH indicators. Turns colourless in acidic solutions.\n\n Gameplay: Tap to turn invisible to dodge bullets for few seconds";
        }
    }

    public override bool useableAbility { get { return true; } }

    public Transform playerMolecule;
    public float effectDuration;

    public override void useAbility()
    {
        /// Expand the circle; 
        StartCoroutine(this.disappearEffectCoroutine());
    }

    private IEnumerator disappearEffectCoroutine()
    {
        this.playerMolecule.gameObject.SetActive(false);
        GameStateManager.Instance.game.player.playerCollision.hasArmor = true;
        yield return new WaitForSeconds(this.effectDuration);
        this.playerMolecule.gameObject.SetActive(true);
        GameStateManager.Instance.game.player.playerCollision.hasArmor = false;
    }
}
