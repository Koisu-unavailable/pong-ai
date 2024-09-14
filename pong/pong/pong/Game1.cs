using System;
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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        // Sprite to be added later
        paddles[0] = new Paddle(new Vector2(70,141), PlayerNums.left, null);
        paddles[1] = new Paddle(new Vector2(727,141), PlayerNums.right, null); // WYSI!!!!!!!
        ball = new Ball(new Vector2(paddles[0].position.X + 10, paddles[0].position.Y), null);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D paddleTexture = this.Content.Load<Texture2D>("paddle");
        foreach (Paddle paddle in paddles){
            paddles[Array.IndexOf(paddles, paddle)].sprite = paddleTexture;
        }
        ball.sprite = paddleTexture;
    }

    protected override void Update(GameTime gameTime)
    {
        hasStarted = true;
        if (!hasStarted){
            ball.position.X = paddles[0].position.X;
        }
        Keys[] keys = Keyboard.GetState().GetPressedKeys();
       
        float newYPos;
        foreach (Keys key in keys){
        switch (key){
            case Keys.W:
                newYPos = paddles[0].position.Y + -200 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!((newYPos > (int)ScreenEdges.bottom) || (newYPos < (int)ScreenEdges.top))){
                    paddles[0].position.Y = newYPos;
                }
                break;
            case Keys.S:
                newYPos = paddles[0].position.Y + 200 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!((newYPos > (int)ScreenEdges.bottom) || (newYPos < (int)ScreenEdges.top))){
                    paddles[0].position.Y = newYPos;
                }
                break;
            case Keys.Down:
                newYPos = paddles[1].position.Y + -200 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!((newYPos > (int)ScreenEdges.bottom) || (newYPos < (int)ScreenEdges.top))){
                    paddles[1].position.Y = newYPos;
                }
                break;
            case Keys.Up:
                newYPos = paddles[1].position.Y + 200 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!((newYPos > (int)ScreenEdges.bottom) || (newYPos < (int)ScreenEdges.top))){
                    paddles[1].position.Y = newYPos;
                }
                break;
        }
        }
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        Console.WriteLine(Mouse.GetState().Position);
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        foreach (Paddle paddle in paddles){
            _spriteBatch.Draw(paddle.sprite, new Rectangle(paddle.position.ToPoint(), new Vector2(20, 100).ToPoint()), Color.White);
        }
        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
