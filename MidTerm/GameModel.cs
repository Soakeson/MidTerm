using System;
using Systems;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Entities;

namespace SnakeIO
{
    public class GameModel
    {
        public int HEIGHT { get; private set; }
        public int WIDTH { get; private set; }

        private Renderer<Texture2D> renderer;
        private KeyboardInput keyboardInput;
        private MouseInput mouseInput;
        private Movement movement;
        private Collision collision;
        private Audio audio;
        private Spawner spawner;
        private Linker linker;
        private int timer = 3000;
        private int score;
        private int level = 0;
        private TimeSpan lastTime;
        private Entity player;
        private Entity dropper;
        private Entity pole;
        private Texture2D open;
        private Texture2D closed;
        private Texture2D poleTex;
        private Texture2D panel;
        private SpriteFont font;
        private SoundEffect miss;
        private Controls.ControlManager controlManager;
        private bool? win = null;
        private SpriteBatch spriteBatch;

        public delegate void AddDelegate(Entity entity);
        private AddDelegate addEntity;

        private List<Entity> toRemove = new List<Entity>();
        private List<Entity> toAdd = new List<Entity>();

        public GameModel(int height, int width)
        {
            this.HEIGHT = height;
            this.WIDTH = width;
            addEntity = AddEntity;
        }

        public void Initialize(Controls.ControlManager controlManager, SpriteBatch spriteBatch, ContentManager contentManager)
        {
            this.keyboardInput = new Systems.KeyboardInput(controlManager);
            this.mouseInput = new Systems.MouseInput(controlManager);
            this.movement = new Movement();
            this.renderer = new Renderer<Texture2D>(spriteBatch);
            this.collision = new Collision();
            this.audio = new Audio();
            this.spawner = new Spawner(addEntity);
            this.linker = new Linker();
            this.controlManager = controlManager;
            this.spriteBatch = spriteBatch;

            open = contentManager.Load<Texture2D>("Images/hand_open");
            closed = contentManager.Load<Texture2D>("Images/hand_closed");
            poleTex = contentManager.Load<Texture2D>("Images/pole");
            miss = contentManager.Load<SoundEffect>("Audio/miss");
            font = contentManager.Load<SpriteFont>("Fonts/Micro5-50");
            panel = contentManager.Load<Texture2D>("Images/panel");

            player = Hand.Create(open, closed, true, true, miss, controlManager, new Vector2(WIDTH/2, HEIGHT-50));
            pole = Pole.Create(poleTex, Color.Red, 10, 100, miss, controlManager, new Vector2(WIDTH/2, 50));
            dropper = Hand.Create(open, closed, false, false, miss, controlManager, new Vector2(WIDTH/2, 50));

            AddEntity(dropper);
            AddEntity(pole);
            AddEntity(player);
        }

        public void Update(GameTime gameTime)
        {
            TimeSpan diff = gameTime.TotalGameTime - lastTime;
            lastTime = gameTime.TotalGameTime;
            timer -= diff.Milliseconds;

            if (timer < 0)
            {
                RemoveEntity(this.dropper);
                dropper = Hand.Create(open, closed, false, true, miss, controlManager, new Vector2(WIDTH/2, 50));
                AddEntity(this.dropper);
            }

            if (timer < 0 && win == null)
            {
                keyboardInput.Update(gameTime);
                mouseInput.Update(gameTime);
                movement.Update(gameTime);
                collision.Update(gameTime);
                audio.Update(gameTime);
                linker.Update(gameTime);
                spawner.Update(gameTime);
                CheckWin(gameTime);
            }

            if (win != null && timer < 0)
            {
                Reset(gameTime);
            }
        }

        public void Reset(GameTime gameTime)
        {
            RemoveEntity(player);
            RemoveEntity(pole);
            RemoveEntity(dropper);
            player = Hand.Create(open, closed, true, true, miss, controlManager, new Vector2(WIDTH/2, HEIGHT-50));
            pole = Pole.Create(poleTex, Color.Red, 10, 100, miss, controlManager, new Vector2(WIDTH/2, 50));
            dropper = Hand.Create(open, closed, false, false, miss, controlManager, new Vector2(WIDTH/2, 50));
            AddEntity(player);
            AddEntity(pole);
            AddEntity(dropper);
            timer = 3000;
            win = null;
        }

        public void Render(GameTime gameTime)
        {
            renderer.Update(gameTime);
            if (win != null)
            {
                if ((bool)win)
                {
                    spriteBatch.Begin();
                    DrawOutlineText(spriteBatch, font, "WIN", Color.Orange, Color.Black, 4, new Vector2(WIDTH/2, HEIGHT/2), 1.0f);
                    spriteBatch.End();
                }
                else 
                {
                    spriteBatch.Begin();
                    DrawOutlineText(spriteBatch, font, "GAME OVER", Color.Orange, Color.Black, 4, new Vector2(WIDTH/2, HEIGHT/2), 1.0f);
                    spriteBatch.End();
                }
            }
            spriteBatch.Begin();
            spriteBatch.Draw(
                    panel,
                    new Rectangle(
                        30,
                        HEIGHT-panel.Height,
                        panel.Width/2,
                        panel.Height
                        ),
                    Color.White
                   );
            DrawOutlineText(spriteBatch, font, score.ToString(), Color.Orange, Color.Black, 4, new Vector2(150, HEIGHT-panel.Height), 1.0f);
            spriteBatch.End();
        }

        public void CheckWin(GameTime gameTime)
        {
            var polPos = pole.GetComponent<Components.Positionable>();
            var polMov = pole.GetComponent<Components.Movable>();
            if (HEIGHT > polPos.pos.Y && HEIGHT*.70 < polPos.pos.Y && polMov.velocity == new Vector2(0,0))
            {
                win = true;
                timer = 1000;
                score++;
            }
            else if (HEIGHT+50 < polPos.pos.Y)
            {
                win = false;
                timer = 1000;
            }
            else if (HEIGHT*.50 > polPos.pos.Y && polMov.velocity == new Vector2(0, 0))
            {
                win = false;
            }
        }

        private void AddEntity(Entity entity)
        {
            renderer.Add(entity);
            keyboardInput.Add(entity);
            mouseInput.Add(entity);
            movement.Add(entity);
            collision.Add(entity);
            audio.Add(entity);
            linker.Add(entity);
            spawner.Add(entity);
        }

        private void RemoveEntity(Entity entity)
        {
            renderer.Remove(entity.id);
            keyboardInput.Remove(entity.id);
            mouseInput.Remove(entity.id);
            movement.Remove(entity.id);
            collision.Remove(entity.id);
            audio.Remove(entity.id);
            linker.Remove(entity.id);
            spawner.Remove(entity.id);
        }

        private void DrawOutlineText(SpriteBatch spriteBatch, SpriteFont font, string text, Color outlineColor, Color frontColor, int pixelOffset, Vector2 position, float scale)
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
