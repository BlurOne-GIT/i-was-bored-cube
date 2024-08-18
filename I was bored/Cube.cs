using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MmgEngine;

namespace I_was_bored;

public class Cube : EngineGame
{
    private float _prevSpeed;
    
    public Cube()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "I was bored.";
        Window.AllowUserResizing = false;
        //Window.IsBorderless = true;
        Graphics.PreferredBackBufferWidth = Graphics.PreferredBackBufferHeight = 200;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Window.KeyDown += (s, e) =>
        {
            switch (e.Key)
            {
                case Keys.Up:
                    CubePoint.Height -= .025f;
                    break;
                case Keys.Down:
                    CubePoint.Height += .025f;
                    break;
                case Keys.Left:
                    CubePoint.Speed -= .25f;
                    break;
                case Keys.Right:
                    CubePoint.Speed += .25f;
                    break;
                case Keys.C:
                    CubePoint.DoColor ^= true;
                    break;
                case Keys.S:
                    CubePoint.DoScale ^= true;
                    break;
                case Keys.P:
                    Console.WriteLine($"Speed: {CubePoint.Speed}, Height: {CubePoint.Height}, Color: {CubePoint.DoColor}, Scale: {CubePoint.DoScale}");
                    break;
                case Keys.Space:
                    if (CubePoint.Speed is 0f)
                        CubePoint.Speed = _prevSpeed;
                    else
                    {
                        _prevSpeed = CubePoint.Speed;
                        CubePoint.Speed = 0f;
                    }
                    break;
                case Keys.Escape:
                    Exit();
                    break;
            }
        };
        
        var ballTexture = Content.Load<Texture2D>("SpinnerBalls");
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), 0f, -1f)
                { DefaultSource = new Rectangle(0, 0, 8, 8), Scale = new Vector2(2f)}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.PiOver2, -1f)
                { DefaultSource = new Rectangle(8, 0, 8, 8), Scale = new Vector2(2f)}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.Pi, -1f)
                { DefaultSource = new Rectangle(16, 0, 8, 8), Scale = new Vector2(2f)}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.PiOver2*3f, -1f)
                { DefaultSource = new Rectangle(24, 0, 8, 8), Scale = new Vector2(2f)}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), 0f)
                { DefaultSource = new Rectangle(0, 0, 8, 8), Scale = new Vector2(2f), Color = Color.LightGray}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.PiOver2)
                { DefaultSource = new Rectangle(8, 0, 8, 8), Scale = new Vector2(2f), Color = Color.LightGray}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.Pi)
                { DefaultSource = new Rectangle(16, 0, 8, 8), Scale = new Vector2(2f), Color = Color.LightGray}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.PiOver2*3f)
                { DefaultSource = new Rectangle(24, 0, 8, 8), Scale = new Vector2(2f), Color = Color.LightGray}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), 0f, 1f)
                { DefaultSource = new Rectangle(0, 0, 8, 8), Scale = new Vector2(2f), Color = Color.Gray}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.PiOver2, 1f)
                { DefaultSource = new Rectangle(8, 0, 8, 8), Scale = new Vector2(2f), Color = Color.Gray}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.Pi, 1f)
                { DefaultSource = new Rectangle(16, 0, 8, 8), Scale = new Vector2(2f), Color = Color.Gray}
        );
        Components.Add(
            new CubePoint(this, ballTexture, new Vector2(100, 100), MathHelper.PiOver2*3f, 1f)
                { DefaultSource = new Rectangle(24, 0, 8, 8), Scale = new Vector2(2f), Color = Color.Gray}
        );
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Transparent);

        // TODO: Add your drawing code here
        SpriteBatch.Begin(transformMatrix: ViewportMatrix, samplerState: SamplerState.PointWrap);
        base.Draw(gameTime);
        SpriteBatch.End();
    }
}