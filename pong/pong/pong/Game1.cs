using System;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Paddle[] paddles = new Paddle[2];
    private Ball ball;
    private bool hasStarted = false;
    private SpriteFont font;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
    }

    protected override void Initialize()
    {
        // Sprite to be added later
        paddles[0] = new Paddle(new Vector2(70, 141), PlayerNums.left, null);
        paddles[1] = new Paddle(new Vector2(727, 141), PlayerNums.right, null); // WYSI!!!!!!!
        ball = new Ball(new Vector2(paddles[0].position.X + 10, paddles[0].position.Y), null);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D paddleTexture = this.Content.Load<Texture2D>("paddle");
        foreach (Paddle paddle in paddles)
        {
            paddles[Array.IndexOf(paddles, paddle)].sprite = paddleTexture;
        }
        ball.sprite = paddleTexture;
        font = Content.Load<SpriteFont>("score");

    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: MAKE ORGIN CENTER AND UPDATE SCREEN EDGE VALUES
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (!hasStarted)
        {
            ball.position.X = paddles[0].position.X;
            hasStarted = true;
            base.Update(gameTime);
            return;
        }


        Keys[] keys = Keyboard.GetState().GetPressedKeys();
        float newPaddleYPos;
        foreach (Keys key in keys)
        {
            switch (key)
            {
                case Keys.W:
                    newPaddleYPos = paddles[0].position.Y + -200 * deltaTime;
                    Console.WriteLine("SK");
                    if (!((newPaddleYPos > (int)ScreenEdges.bottom) || (newPaddleYPos < (int)ScreenEdges.top)))
                    {
                        paddles[0].position.Y = newPaddleYPos;
                    }
                    break;
                case Keys.S:
                    newPaddleYPos = paddles[0].position.Y + 200 * deltaTime;
                    Console.WriteLine("SK");
                    if (!((newPaddleYPos > (int)ScreenEdges.bottom) || (newPaddleYPos < (int)ScreenEdges.top)))
                    {
                        paddles[0].position.Y = newPaddleYPos;
                    }
                    break;
                case Keys.Down:
                    newPaddleYPos = paddles[1].position.Y + 200 * deltaTime;
                    Console.WriteLine("SK");
                    if (!((newPaddleYPos > (int)ScreenEdges.bottom) || (newPaddleYPos < (int)ScreenEdges.top)))
                    {
                        paddles[1].position.Y = newPaddleYPos;
                    }
                    break;
                case Keys.Up:
                    newPaddleYPos = paddles[1].position.Y + -200 * deltaTime;
                    Console.WriteLine("SK");
                    if (!((newPaddleYPos > (int)ScreenEdges.bottom) || (newPaddleYPos < (int)ScreenEdges.top)))
                    {
                        paddles[1].position.Y = newPaddleYPos;
                    }
                    break;

            }
        }

        foreach (Paddle paddle in paddles)
        {
            paddle.UpdateRectPosition();
        }
        HandlePaddleBallCollisions(deltaTime);
        HandleScoring(gameTime);
        ball.Move(deltaTime);
        // Console.WriteLine(Mouse.GetState().Position);
        Console.WriteLine(ball.position);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        foreach (Paddle paddle in paddles)
        {

            _spriteBatch.Draw(paddle.sprite, paddle.rect, Color.White);
        }
        _spriteBatch.Draw(ball.sprite, ball.rect, Color.Purple);
        _spriteBatch.DrawString(font, paddles[0].score.ToString(), new Vector2(248, 92), Color.AliceBlue);
        _spriteBatch.DrawString(font, paddles[1].score.ToString(), new Vector2(582, 92), Color.AliceBlue);
        _spriteBatch.DrawString(font, ball.position.Y.ToString(), new Vector2(600, 200), Color.AliceBlue);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void HandleScoring(GameTime gameTime)
    {
        if (ball.position.X < (int)ScreenEdges.left)
        {
            paddles[0].score += 1;
            ball.position = paddles[1].position;
            ball.flipYDirection();
            base.Update(gameTime);
            return;
        }
        else if (ball.position.X > (int)ScreenEdges.right)
        {
            paddles[1].score += 1;
            ball.position = paddles[0].position;
            ball.flipYDirection();
            base.Update(gameTime);
            return;
        }
    }
    private void HandlePaddleBallCollisions(float deltaTime)
    {
        foreach (Paddle paddle in paddles)
        {
            if (paddle.rect.Intersects(ball.rect))
            {

                // MAKE BALL INSTANTLEY LEAVE PADDLE
                bool isOnRight = Array.IndexOf(paddles, paddle) == 1 ? false : true;
                // If paddle is on the right
                if (isOnRight)
                {
                    ball.flipXDirection();
                    Console.WriteLine(ball.Move(deltaTime));
                }
                else
                {
                    ball.flipXDirection();
                    Console.WriteLine(ball.Move(deltaTime));
                }


            }
        }
    }

}
