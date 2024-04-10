using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace Entities
{
    public class Pole 
    {
        public static Entity Create(Texture2D texture, Color color, int height, int width, SoundEffect sound, Controls.ControlManager cm, Vector2 pos, string chain = null)
        {
            Entity pole = new Entity();
            int radius = width/2;

            if (chain != null)
            {
                pole.Add(new Components.Linkable(chain, Components.LinkPosition.Head));
            }

            pole.Add(new Components.Collidable(new Vector3(pos.X, pos.Y, radius), true));
            pole.Add(new Components.Renderable<Texture2D>(texture, color, Color.Black, height, width));
            pole.Add(new Components.Movable(new Vector2(0, 0), new Vector2(0, 0)));
            pole.Add(new Components.Positionable(pos));
            pole.Add(new Components.Audible(sound));

            return pole;
        }
    }
}
