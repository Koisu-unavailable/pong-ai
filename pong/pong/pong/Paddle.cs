using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    public struct Paddle : IUpdateRect
    {
        public Vector2 position = new Vector2(0, 0);
        public PlayerNums? player_nums = null;
        public Texture2D sprite = null;
        public int points = 0;
        public Rectangle rect;
        public Vector2 origin => new Vector2(sprite.Width / 2, sprite.Height / 2);
        public int score = 0;
        public Paddle(Vector2 position, PlayerNums playerNums, Texture2D sprite)
        {
            this.position = position;
            this.player_nums = playerNums;
            this.sprite = sprite;
            this.rect = new Rectangle(this.position.ToPoint(), new Vector2(20, 100).ToPoint());

        }
        public void UpdateRectPosition()
        {
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
        }
    }
}