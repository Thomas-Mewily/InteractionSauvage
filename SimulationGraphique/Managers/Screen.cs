using Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SimulationGraphique.Managers;

public class Screen : ClasseBase
{
    private Point2 _WindowSize;
    public Point2 WindowSize { get => _WindowSize; set { _WindowSize = GetFinalSize(value); } }
    public Vec2 WindowSizeVec2 => WindowSize;

    public bool IsFullScreen
    {
        get => All.Graphics.IsFullScreen;
        set
        {
            if (IsFullScreen != value)
            {
                All.Graphics.ToggleFullScreen();
            }
        }
    }

    public Point2 ScreenSize { get; private set; }
    public Vec2 ScreenSizeVec2 => ScreenSize; 

    public Screen()
    {
        Load();
    }

    public override void Load()
    {
        ReadScreenSize();
        WindowSize = ScreenSize / 2;
        ApplyGraphicChange();
    }

    private void ReadScreenSize() 
    {
        ScreenSize = new Point2(All.GraphicsDevice.Adapter.CurrentDisplayMode.Width, All.GraphicsDevice.Adapter.CurrentDisplayMode.Height);
        WindowSize = new Point2(All.Graphics.PreferredBackBufferWidth, All.Graphics.PreferredBackBufferHeight);
    }

    public override void Update()
    {
        ReadScreenSize();
    }

    public override void Draw()
    {
#if DEBUG
        All.SpriteBatch.DebugText("windows: " + WindowSize + ", screen: " + ScreenSize);
#endif
    }

    public void ApplyGraphicChange()
    {
        All.Graphics.PreferredBackBufferWidth  = WindowSize.X;
        All.Graphics.PreferredBackBufferHeight = WindowSize.Y;
        All.Graphics.ApplyChanges();
    }

    private Point2 GetFinalSize(Point2 size)
    {
        //No zero pixel window size, please
        size.X = Math.Max(1, size.X);
        size.Y = Math.Max(1, size.Y);
        return size;
    }
}