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
                        // If collided put it back in it's previous location
                        if (res) 
                        {
                            Components.Positionable ep = e1.GetComponent<Components.Positionable>();
                            ep.Pos = ep.PrevPos;
                        }
                    }
                }
            }
        }

        private bool DidCollide(Entities.Entity e1, Entities.Entity e2)
        {
            Components.Collidable e1Col = e1.GetComponent<Components.Collidable>();
            Components.Collidable e2Col = e2.GetComponent<Components.Collidable>();
            double hitDist = Math.Pow(e1Col.HitBox.Z + e2Col.HitBox.Z, 2);
            double dist = Math.Pow(Math.Abs(e1Col.HitBox.X - e2Col.HitBox.X), 2) + Math.Pow(Math.Abs(e1Col.HitBox.Y - e2Col.HitBox.Y), 2);

            if (dist < hitDist)
            {
                return true;
            }

            return false;
        }

        
    }
}
