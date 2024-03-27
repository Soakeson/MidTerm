using System;
using Microsoft.Xna.Framework;

namespace Systems
{
    /// <summary>
    /// This system is responsible for handling the collision of any
    /// entity with Collidable, & Positionable components.
    /// </summary>
    public class Collision : System
    {
        public Collision()
            : base(
                    typeof(Components.Collidable),
                    typeof(Components.Positionable)
                    )
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var e1 in entities.Values)
            {
                foreach (var e2 in entities.Values)
                {
                    if (e1 != e2)
                    {
                        bool res = DidCollide(e1, e2);
                        Console.WriteLine(res);
                    }
                }
            }
        }

        private bool DidCollide(Entities.Entity e1, Entities.Entity e2)
        {
            Components.Collidable e1hb = e1.GetComponent<Components.Collidable>();
            Components.Collidable e2hb = e2.GetComponent<Components.Collidable>();
            double hitDist = Math.Pow(e1hb.hitBox.Z + e2hb.hitBox.Z, 2);
            double dist = Math.Pow(Math.Abs(e1hb.hitBox.X - e2hb.hitBox.X), 2) + Math.Pow(Math.Abs(e1hb.hitBox.Y - e2hb.hitBox.Y), 2);

            if (dist < hitDist)
            {
                return true;
            }

            return false;
        }
    }
}
