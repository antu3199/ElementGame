using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : Shape {
	public Rectangle(SpriteRenderer rend) : base(rend) {
		this.rend.sprite = SpriteDictionary.Instance.findSprite("rectangle");
	}

	public override void printType() {
		Debug.Log("Rectangle");
	}
}
