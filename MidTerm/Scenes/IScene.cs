using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Yew
{
    public interface IScene 
    {
        public void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics);
        public void LoadContent(ContentManager contentManager);
        public SceneContext ProcessInput(GameTime gameTime);
        public void Update(GameTime gameTime);
        public void Render(GameTime gameTime);
    }
}
