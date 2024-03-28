using Microsoft.Xna.Framework;

namespace Components
{
    public class Movable : Component
    {
        private Vector2 facing { get; set; }
        public Vector2 Facing
        {
            get { return facing; }
            set { facing = value; }
        }

        private Vector2 velocity { get; set; }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Movable(Vector2 facing, Vector2 velocity)
        {
            this.facing = facing;
            this.velocity = velocity;
        }

    }
}
