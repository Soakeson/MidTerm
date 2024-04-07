using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Controls;
using Scenes;

namespace MidTerm 
{
    public class MidTerm : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private DataManager dataManager;
        private ControlManager controlManager;
        private Dictionary<SceneContext, Scene> scenes = new Dictionary<SceneContext, Scene>();
        private SceneContext nextScene;
        private SceneContext currSceneContext;
        private Scene currScene;
        private Texture2D background;

        public MidTerm()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920/2;
            graphics.PreferredBackBufferHeight = 1080/2;

            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            dataManager = new DataManager();
            controlManager = new ControlManager(dataManager);
        }

        protected override void Initialize()
        {
            scenes.Add(SceneContext.Game, new GameScene(graphics.GraphicsDevice, graphics, controlManager));
            scenes.Add(SceneContext.MainMenu, new MainMenuScene(graphics.GraphicsDevice, graphics, controlManager));
            scenes.Add(SceneContext.Options, new OptionScene(graphics.GraphicsDevice, graphics, controlManager));

            foreach (Scene scene in scenes.Values)
            {
                scene.Initialize(graphics.GraphicsDevice, graphics, controlManager);
            }

            currSceneContext = SceneContext.MainMenu;
            currScene = scenes[currSceneContext];
            nextScene = currSceneContext;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = this.Content.Load<Texture2D>("Images/background");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (Scene scene in scenes.Values)
            {
                scene.LoadContent(this.Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextScene == SceneContext.Exit)
            {
                Exit();
            }
            else if (currSceneContext != nextScene)
            {
                currScene = scenes[nextScene];
                currSceneContext = nextScene;
            }

            nextScene = currScene.ProcessInput(gameTime);
            currScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(
                  background,
                  new Rectangle(
                      x: 0,
                      y: 0,
                      width: graphics.PreferredBackBufferWidth,
                      height: graphics.PreferredBackBufferHeight 
                      ),
                  Color.White
                  );
            spriteBatch.End();
            currScene.Render(gameTime);
            base.Draw(gameTime);
        }
    }
}
