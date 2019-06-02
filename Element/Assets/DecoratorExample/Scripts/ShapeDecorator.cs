

//Shape decorator
public abstract class ShapeDecorator: Shape {
   protected Shape decoratedRend;

   public ShapeDecorator(Shape decoratedRend){
      this.decoratedRend = decoratedRend;
   }

    public override void printType() {
        this.decoratedRend.printType();
    }
}