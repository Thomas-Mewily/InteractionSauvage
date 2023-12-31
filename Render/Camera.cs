﻿using Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Useful;

namespace Render;

public class Camera
{
    private bool NeedUpdate = false;
    private Rect2F _Rect;
    public Rect2F Rect { get => _Rect; set { _Rect = value; NeedUpdate = true; } }

    public Vec2 Zoom 
    { 
        get => _Rect.Size; 
        set 
        {
            Vec2 delta = value - Zoom;
            _Rect.Min -= delta / 2;
            _Rect.Size += delta;
            NeedUpdate = true;
        }
    }
    public Vec2 Min { get => _Rect.Min; set { _Rect.Min = value; NeedUpdate = true; } }
    public Vec2 Max { get => _Rect.Max; set { _Rect.Max = value; NeedUpdate = true; } }

    //public Rect2F? Bound = null;

    private Angle _Rotation = Angle.Zero;
    public Angle Rotation { get => Rotation; set { Rotation = value; NeedUpdate = true; } }

    public Vec2 Position { get => new(X, Y); set { X = value.X; Y = value.Y; } }

    // center
    public float X 
    {
        get => _Rect.XMin + _Rect.SizeX / 2;
        set 
        {
            float delta = value - X;
            _Rect.XMin += delta;
            NeedUpdate = true;
        } 
    }

    public float Y
    {
        get => _Rect.YMin + _Rect.SizeY / 2;
        set
        {
            float delta = value - Y;
            _Rect.YMin += delta;
            NeedUpdate = true;
        }
    }

    public Camera() { }

    private Camera(Rect2F area)
    {
        _Rect = area;
    }
    public static Camera Center(Rect2F area) => new(area);

    public Matrix _TransformMatrix { get; set; }
    public Matrix _InvertedMatrix { get; set; }

    public Vec2 WorldPosition(float x, float y) => WorldPosition(new Vec2(x, y));
    public Vec2 WorldPosition(Vec2 vec) 
    {
        if (NeedUpdate) { NeedUpdate = false; }
        return Vector2.Transform(vec, _InvertedMatrix);
    }

    private static List<Camera> Cameras = new() {  };
    public static void Push(Camera cam) => Cameras.Add(cam);
    public static Camera Pop()  => Cameras.Pop();
    public static Camera Peek() => Cameras.Peek();
    public static int Count => Cameras.Count;
}

