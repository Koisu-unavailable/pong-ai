using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
namespace pong
{
    public struct Ball : IUpdateRect
    {
        public Vector2 position = Vector2.Zero;
        public Texture2D sprite = null;
        public Rectangle rect;
        private Vector2 velocity;
        public int currentYDirection = 1;
        public int currentXDirection = 1;
        private Random rdm = new Random();
        private const int VELOCITY_CAP = 800;

        public Ball(Vector2 position, Texture2D sprite)
        {
            this.position = position;
            this.sprite = sprite;
            rect = new Rectangle(this.position.ToPoint(), new Vector2(20, 20).ToPoint());
            velocity = new Vector2(400, 400);
        }
        public void flipYDirection()
        {
            currentYDirection *= -1;
            velocity.Y *= -1;
        }
        public void flipXDirection(){
            velocity.X *= -1;
            currentXDirection *= -1;
        }
        public Vector2 Move(float deltaTime)
        {
            Vector2 newPos = position + velocity * deltaTime;
            if (!(
                (newPos.Y >= (int)ScreenEdges.bottom) ||
                (newPos.Y <= (int)ScreenEdges.top)))
            // Not on bottom or top
            {
                position += velocity * deltaTime;

            }
            else
            // If on bottom or top
            {
                // If not moving too fast downwards
                if (!(velocity.Y > VELOCITY_CAP
                | velocity.Y < VELOCITY_CAP * -1))
                {
                    // velocity *= (float)((rdm.NextDouble() + rdm.NextDouble() + 0.2) * -1);
                    flipYDirection();
                }
                // Moving too fast
                else
                {
                    velocity.Y = 400 * currentYDirection;
                }

                position += velocity * deltaTime;
            }
            UpdateRectPosition();
            return position;
        }
        public void UpdateRectPosition()
        {
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
        }

    }
}