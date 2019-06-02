using UnityEngine;

// Shape
public abstract class Shape  {
   public SpriteRenderer rend;
   public abstract void printType();

   public Shape() {}

   public Shape(SpriteRenderer rend) {
       this.rend = rend;
   }
}