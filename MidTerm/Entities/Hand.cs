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
        public static Entity Create(Texture2D open, Texture2D closed, bool controllable, bool isOpen, SoundEffect sound, Controls.ControlManager cm, Vector2 pos, string chain = null)
        {
            Entity hand = new Entity();
            int radius = closed.Width >= closed.Height ? closed.Width / 2 : closed.Height / 2;

            if (chain != null)
            {
                hand.Add(new Components.Linkable(chain, Components.LinkPosition.Head));
            }

            hand.Add(new Components.Collidable(new Vector3(pos.X, pos.Y, radius), controllable ? !isOpen : false));
            hand.Add(new Components.Renderable<Texture2D>(isOpen ? open : closed, Color.White, Color.Black));
            hand.Add(new Components.Positionable(pos));
            hand.Add(new Components.Audible(sound));
            Components.Renderable<Texture2D> renderable = hand.GetComponent<Components.Renderable<Texture2D>>();
            Components.Collidable collidable = hand.GetComponent<Components.Collidable>();
            if (controllable)
            {
                hand.Add(new Components.KeyboardControllable(
                            true,
                            cm,
                            new (Controls.ControlContext, Controls.ControlDelegate)[1]
                            {
                            (Controls.ControlContext.HandChange,
                             new Controls.ControlDelegate((GameTime gameTime, float value) =>
                                 {
                                     isOpen = !isOpen;
                                     renderable.texture = isOpen ? open : closed;
                                     collidable.enabled = !collidable.enabled;
                                 }))
                            }));
            }
            return hand;
        }
    }
}
