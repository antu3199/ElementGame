

using UnityEngine;

//Red shape decorator
public class RedShapeDecorator: ShapeDecorator {
   public RedShapeDecorator(Shape decoratedRend) : base(decoratedRend){
       this.decoratedRend.rend.color = Color.red;
   }

    public override void printType() {
        this.decoratedRend.printType();
        Debug.Log("Should be red");
    }
}