using Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Useful;

namespace Render;

public class Camera
{
    public Rect2F Rect;
    public Vec2 Zoom 
    { 
        get => Rect.Size; 
        set 
        {
            Vec2 delta = value - Zoom;
            Rect.Min -= delta / 2;
            Rect.Size += delta;
        }
    }
    public Vec2 Min { get => Rect.Min; set => Rect.Min = value; }
    public Vec2 Max { get => Rect.Max; set => Rect.Max = value; }

    public Rect2F? Bound = null;

    public Angle Rotation = Angle.Zero;

    public Vec2 Position { get => new Vec2(X, Y); set { X = value.X; Y = value.Y; } }

    // center
    public float X 
    {
        get => Rect.XMin + Rect.SizeX / 2;
        set 
        {
            float delta = value - X;
            Rect.XMin += delta;
        } 
    }

    public float Y
    {
        get => Rect.YMin + Rect.SizeY / 2;
        set
        {
            float delta = value - Y;
            Rect.YMin += delta;
        }
    }

    public Camera() { }

    private Camera(Rect2F area)
    {
        Rect = area;
    }
    public static Camera Center(Rect2F area) => new Camera(area);

    private static List<Camera> Cameras = new List<Camera>() {  };
    public static void Push(Camera cam) => Cameras.Add(cam);
    public static Camera Pop()  => Cameras.Pop();
    public static Camera Peek() => Cameras.Peek();
    public static int Count => Cameras.Count;
}

