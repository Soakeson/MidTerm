using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components
{
    class Renderable : Component
    {
        public Texture2D texture {get; set;}
        public Color color {get; set;}
        public Color stroke {get; set;}

        public Renderable(Texture2D texture, Color color, Color stroke)
        {
            this.texture = texture;
            this.color = color;
            this.stroke = stroke;
        }
    }
}
