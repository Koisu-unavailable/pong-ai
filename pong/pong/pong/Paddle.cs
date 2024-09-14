using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    public struct Paddle
    {
        public Vector2 position = new Vector2(0,0);
        public PlayerNums? player_nums = null;
        public Texture2D sprite = null;
        public int points = 0;
        public Paddle(Vector2 position, PlayerNums playerNums, Texture2D sprite){
            this.position = position;
            this.player_nums = playerNums;
            this.sprite = sprite;
        }
    }
}