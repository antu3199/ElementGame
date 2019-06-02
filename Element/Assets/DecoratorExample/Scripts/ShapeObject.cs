using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class ShapeObject : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private bool isRect;
    
    private Shape shapeModel;

    void Awake() {
        if (this.isRect) {
            this.shapeModel = new RedShapeDecorator(new Rectangle(spriteRenderer));
        } else {
            this.shapeModel = new RedShapeDecorator(new Circle(spriteRenderer));
        }
        
        this.shapeModel.printType();
    }
}
