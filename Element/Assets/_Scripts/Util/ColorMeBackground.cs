using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMeBackground : MonoBehaviour {
  public SpriteRenderer spriteRend;
  
  public bool multiplyInstead = false;
  private Color origColor;
  void Start() {
    this.origColor = spriteRend.color;
  }
	void Update () {
    if (this.multiplyInstead == false) {
		  spriteRend.color = GameStateManager.Instance.game.backgroundHandler.curColor;
    } else {
      spriteRend.color = this.origColor * GameStateManager.Instance.game.backgroundHandler.curColor;
    }
	}
}
