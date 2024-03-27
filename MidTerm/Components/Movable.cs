using System;
using Microsoft.Xna.Framework;

namespace Components
{
    public class Movable : Component
    {
        public Vector2 facing {get; set;}
        public Vector2 velocity {get; set;}

        public Movable(Vector2 facing, Vector2 velocity)
        {
            this.facing = facing;
            this.velocity = velocity;
        }

    }
}
