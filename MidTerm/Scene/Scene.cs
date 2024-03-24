using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CS5410.Input;

namespace Yew
{
    public abstract class Scene : IScene
    {
        protected SpriteBatch spriteBatch;
        protected GraphicsDevice graphicsDevice;
        protected GraphicsDeviceManager graphics;
        protected int screenWidth;
        protected int screenHeight;
        protected KeyboardInput keyboard = new KeyboardInput();

        public void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            graphics = graphics;
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public abstract void LoadContent(ContentManager contentManager);
        public abstract SceneContext ProcessInput(GameTime gameTime);
        public abstract void Render(GameTime gameTime);
        public abstract void Update(GameTime gameTime);

        protected static void DrawOutlineText(SpriteBatch spriteBatch, SpriteFont font, string text, Color outlineColor, Color frontColor, int pixelOffset, Vector2 position, float scale)
        {
            // outline
            spriteBatch.DrawString(font, text, position - new Vector2(pixelOffset * scale, 0), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, position + new Vector2(pixelOffset * scale, 0), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, position - new Vector2(0, pixelOffset * scale), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, position + new Vector2(0, pixelOffset * scale), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);

            // outline corners
            spriteBatch.DrawString(font, text, position - new Vector2(pixelOffset * scale, pixelOffset * scale), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, position + new Vector2(pixelOffset * scale, pixelOffset * scale), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, position - new Vector2(-(pixelOffset * scale), pixelOffset * scale), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, position + new Vector2(-(pixelOffset * scale), pixelOffset * scale), outlineColor, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);

            // inside
            spriteBatch.DrawString(font, text, position, frontColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

    }

}
