using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Yew
{
    public class ParticleSystemRenderer
    {
        private string nameParticleContent;
        private Texture2D texParticle;

        public ParticleSystemRenderer(string nameParticleContent)
        {
            this.nameParticleContent = nameParticleContent;
        }

        public void LoadContent(ContentManager content)
        {
            texParticle = content.Load<Texture2D>(nameParticleContent);
        }

        public void draw(SpriteBatch spriteBatch, ParticleSystem system)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Rectangle r = new Rectangle(0, 0, 0, 0);
            Vector2 centerTexture = new Vector2(texParticle.Width / 2, texParticle.Height / 2);
            foreach (Particle particle in system.particles)
            {
                r.X = (int)particle.center.X;
                r.Y = (int)particle.center.Y;
                r.Width = (int)particle.size.X;
                r.Height = (int)particle.size.Y;

                spriteBatch.Draw(
                        texParticle,
                        r,
                        null,
                        Color.White,
                        particle.rotation,
                        centerTexture,
                        SpriteEffects.None,
                        0);
            }

            spriteBatch.End();
        }
    }

}
