using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Yew 
{
    public class MidTerm : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private DataManager dataManager;
        private ControlManager controls;
        private Texture2D playerTexture;
        private int x;
        private int y;

        public MidTerm()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            dataManager = new DataManager();
            controls = new ControlManager(dataManager);
            x = 0;
            y = 0;

            controls.RegisterControl(SceneContext.MainMenu, ControlContext.MoveUp, Keys.Up, true,
                    new IInputDevice.CommandDelegate((GameTime gameTime, float value) => { 
                        this.y -= 5;
                        }));
            controls.RegisterControl(SceneContext.MainMenu, ControlContext.MoveDown, Keys.Down, true,
                    new IInputDevice.CommandDelegate((GameTime gameTime, float value) => { 
                        this.y += 5;
                        }));
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = this.Content.Load<Texture2D>("images/player");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            controls.Update(gameTime, SceneContext.MainMenu);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                playerTexture, new Rectangle(
                x,
                y,
                playerTexture.Width,
                playerTexture.Height),
                Color.Red);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
