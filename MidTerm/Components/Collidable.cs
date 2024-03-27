using Microsoft.Xna.Framework;

namespace Components
{
    public class Collidable : Component
    {
        public Vector3 hitBox {get; set;}

        public Collidable(Vector3 hitBox)
        {
            this.hitBox = hitBox;
        }
    }
}
