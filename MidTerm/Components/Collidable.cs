using Microsoft.Xna.Framework;

namespace Components
{
    public class Collidable : Component
    {
        private Vector3 hitBox { get; set; }
        public Vector3 HitBox
        {
            get { return hitBox; }
            set { hitBox = value; }
        }

        public Collidable(Vector3 hitBox)
        {
            this.hitBox = hitBox;
        }
    }
}
