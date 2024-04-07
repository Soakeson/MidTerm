using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace Entities
{
    public class Hand 
    {
        public static Entity Create(Texture2D open, Texture2D closed, SoundEffect sound, Controls.ControlManager cm, Vector2 pos, string chain = null)
        {
            Entity hand = new Entity();
            int radius = closed.Width >= closed.Height ? closed.Width / 2 : closed.Height / 2;

            if (chain != null)
            {
                hand.Add(new Components.Linkable(chain, Components.LinkPosition.Head));
            }

            hand.Add(new Components.Collidable(new Vector3(pos.X, pos.Y, radius)));
            hand.Add(new Components.Renderable<Texture2D>(open, Color.White, Color.Black));
            hand.Add(new Components.Positionable(pos));
            hand.Add(new Components.Audible(sound));
            Components.Renderable<Texture2D> renderable = hand.GetComponent<Components.Renderable<Texture2D>>();
            hand.Add(new Components.KeyboardControllable(
                true,
                cm,
                new (Controls.ControlContext, Controls.ControlDelegate)[1]
                {
                (Controls.ControlContext.ToggleHand,
                     new Controls.ControlDelegate((GameTime gameTime, float value) =>
                     {
                        renderable.texture = closed;
                     }))
                }));
            return hand;
        }
    }
}
