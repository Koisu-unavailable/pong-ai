using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace pong
{
    public struct Ball
    {
        public Vector2 position = Vector2.Zero;
        public Texture2D sprite = null;
        public Ball(Vector2 position, Texture2D sprite){
            this.position = position;
            this.sprite = sprite;
        }
    }
}