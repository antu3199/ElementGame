using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape {
	public Circle(SpriteRenderer rend) : base(rend) {
		this.rend.sprite = SpriteDictionary.Instance.findSprite("circle");
	}

	public override void printType() {
		Debug.Log("Circle");
	}
}
