using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Entities
{
    public class Wall 
    {
        public static Entity Create(Texture2D texture, Vector2 pos)
        {
            Entity wall = new Entity();
            int radius = texture.Width >= texture.Height ? texture.Width/2 : texture.Height/2;

            wall.Add(new Components.Collidable(new Vector3(pos.X, pos.Y, radius)));
            wall.Add(new Components.Renderable(texture, Color.White, Color.Black));
            wall.Add(new Components.Positionable(pos));

            return wall;
        }
    }
}
