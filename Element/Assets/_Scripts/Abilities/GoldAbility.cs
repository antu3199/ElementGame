using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAbility : PlayerAbilityBase
{
    public override PARTICLE_TYPES type { get { return PARTICLE_TYPES.GOLD; } }
    public override string commonName { get { return "Gold"; } }
    public override string chemicalName { get { return "Au"; } }
    public override string description
    {
        get
        {
            return "Description: As a precious metal, it has been used for many thousands of years by people all over the world, for jewelry, and as money. Gold is important because it is rare, but also easier to use than other rare metals. It is also used to repair and replace teeth and in electronic equipment such as computers.\n\n Gameplay: Tap the screen with two fingers to automatically block the next few attacks if you get hit";
        }
    }

    public SpriteRenderer sprite;

    public override bool useableAbility { get { return true; } }

    public int armor;

    public override void useAbility()
    {
        GameStateManager.Instance.player.playerCollision.hasArmor = true;
        armor = 3;
        sprite.color = new Color32(218, 165, 32, 255);
    }

    void OnDestroy()
    {
        if (!GameStateManager.Instance || !GameStateManager.Instance.player || !GameStateManager.Instance.player.playerCollision) return;

        if (GameStateManager.Instance.player.playerCollision.hasArmor)
        {
            GameStateManager.Instance.player.playerCollision.hasArmor = false;
        }

        sprite.color = new Color32(255, 215, 0, 255);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameStateManager.Instance.player.playerCollision.canCollide) return;

        print(other.tag);

        if (other.tag == "EnemyBullet")
        {
            armor = System.Math.Max(0, armor - 1);
            if (armor == 0)
            {
                GameStateManager.Instance.player.playerCollision.hasArmor = false;
                sprite.color = new Color32(255, 215, 0, 255);
            }
        }
    }
}
