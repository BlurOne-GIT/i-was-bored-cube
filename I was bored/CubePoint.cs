using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MmgEngine;

namespace I_was_bored;

public class CubePoint : SimpleImage
{
    private static float _height;
    public static float Height { get => _height; set => _height = MathHelper.Clamp(value, -1f, 1f); }
    public static float Speed { get; set; } = 2;
    public static bool DoColor { get; set; } = true;
    public static bool DoScale { get; set; }
    private static float _maxCubeOffsetFactor = -1f;
    private static float _minCubeOffsetFactor = 1f;
    private static float _minmaxCubeOffsetFactorDistance;
    
    public float Size { get; set; } = 75;
    private readonly Vector2 _viewportPivot;
    private readonly float _initialTime;
    private readonly Vector2 _cubeOffsetFactor;
    private Color _ogColor;
    private Vector2 _ogScale;
    private TimeSpan _accumulatedTime;
    
    public CubePoint(Game game, Texture2D texture, Vector2 viewportPivot, float initialTime, float cubeOffsetFactor = 0f)
        : base(game, texture, viewportPivot, 0, Alignment.Center)
    {
        _viewportPivot = viewportPivot;
        _initialTime = initialTime;
        _cubeOffsetFactor = new Vector2(0f, cubeOffsetFactor);
        _minCubeOffsetFactor = MathHelper.Min(_minCubeOffsetFactor, cubeOffsetFactor);
        _maxCubeOffsetFactor = MathHelper.Max(_maxCubeOffsetFactor, cubeOffsetFactor);
        _minmaxCubeOffsetFactorDistance = MathHelper.Distance(_minCubeOffsetFactor, _maxCubeOffsetFactor);
    }
    
    public override void Initialize()
    {
        base.Initialize();
        _ogColor = Color;
        _ogScale = Scale;
    }

    public override void Update(GameTime gameTime)
    {
        _accumulatedTime += gameTime.ElapsedGameTime * Speed;
        var t = _initialTime + (float)_accumulatedTime.TotalSeconds; //(float)gameTime.TotalGameTime.TotalSeconds * Speed;
        Position = (GetPosition(t, Height) + CubeOffset * _cubeOffsetFactor) * Size + _viewportPivot;
        
        // 2 points for sin function, 8 points for cube height
        DrawOrder = (int)(VerticalFunction(t, 1f) + 1f +
                          (-Height * _cubeOffsetFactor.Y - _minCubeOffsetFactor) / _minmaxCubeOffsetFactorDistance * 8f);
        
        Color = (DoColor ? _ogColor * ((VerticalFunction(t, 1f) + 2f) / 2f) : _ogColor) with { A = 255 };
        Scale = DoScale ? _ogScale + new Vector2((VerticalFunction(t, 1f) + 2f) / MathHelper.Tau) : _ogScale;
    }

    private static float HorizontalFunction(float t) => float.Cos(t);
    
    private static float VerticalFunction(float t, float h) => float.Sin(t) * h;
    
    private static Vector2 GetPosition(float t, float h) => new(HorizontalFunction(t), VerticalFunction(t, h));

    private static Vector2 CubeOffset =>
        new(0, (1f - float.Pow(float.Abs(Height), MathHelper.Pi)) * float.Cos(MathHelper.PiOver4));
}