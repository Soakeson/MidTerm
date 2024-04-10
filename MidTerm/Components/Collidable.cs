using Microsoft.Xna.Framework;

namespace Components
{
    /// <summary>
    /// This component is responsible for managing the state of Collidable entities.
    /// </summary>
    public class Collidable : Component
    {
        public Vector3 hitBox { get; set; }
        public bool collided { get; set; }
        public bool enabled { get; set; }

        public Collidable(Vector3 hitBox, bool enabled)
        {
            this.hitBox = hitBox;
            this.enabled = enabled;
        }
    }
}
