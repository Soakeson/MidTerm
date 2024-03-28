using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Entities
{
    public class Player
    {
        public static Entity Create(Texture2D texture, Controls.ControlManager cm, Scenes.SceneContext sc, Vector2 pos)
        {
            Entity player = new Entity();

            int radius = texture.Width >= texture.Height ? texture.Width/2 : texture.Height/2;

            player.Add(new Components.Collidable(new Vector3(pos.X, pos.Y, radius)));
            player.Add(new Components.Renderable(texture, Color.Red, Color.Black));
            player.Add(new Components.Positionable(pos));
            player.Add(new Components.Movable(new Vector2(0, 0), new Vector2(0, 0)));
            Components.Movable movable = player.GetComponent<Components.Movable>();
            player.Add(new Components.KeyboardControllable(
                        cm,
                        new (Controls.Control, Controls.ControlDelegate)[4]
                        {
                        (new Controls.Control(sc, Controls.ControlContext.MoveUp, Keys.W, false),
                         new Controls.ControlDelegate((GameTime gameTime, float value) =>
                         {
                            movable.Facing = new Vector2(0, -1);
                            movable.Velocity = new Vector2(1, 1);
                         })),
                        (new Controls.Control(sc, Controls.ControlContext.MoveDown, Keys.S, false),
                         new Controls.ControlDelegate((GameTime gameTime, float value) =>
                         {
                            movable.Facing = new Vector2(0, 1);
                            movable.Velocity = new Vector2(1, 1);
                         })),
                        (new Controls.Control(sc, Controls.ControlContext.MoveRight, Keys.D, false),
                         new Controls.ControlDelegate((GameTime gameTime, float value) =>
                         {
                            movable.Facing = new Vector2(1, 0);
                            movable.Velocity = new Vector2(1, 1);
                         })),
                        (new Controls.Control(sc, Controls.ControlContext.MoveLeft, Keys.A, false),
                         new Controls.ControlDelegate((GameTime gameTime, float value) =>
                         {
                            movable.Facing = new Vector2(-1, 0);
                            movable.Velocity = new Vector2(1, 1);
                         })),
                        }));
            return player;
        }
    }
}
