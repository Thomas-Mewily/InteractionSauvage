using Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimulationGraphique;
using System.Collections.Generic;
using Useful;

namespace Render;

public class Camera
{
    private bool NeedUpdate = true;

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
    public Angle Rotation { get => _Rotation; set { _Rotation = value; NeedUpdate = true; } }

    public Vec2 Position { get => new(X, Y); set { X = value.X; Y = value.Y; } }

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

    private Matrix _TransformMatrix;
    private Matrix _InvertedMatrix;

    public Matrix TransformMatrix { get { UpdateMatrix(); return _TransformMatrix; } }
    public Matrix InvertedMatrix  { get { UpdateMatrix(); return _InvertedMatrix; } }

    public Vec2 WorldPosition(float x, float y) => ToWorldPosition(new Vec2(x, y));
    public Vec2 ToWorldPosition(Vec2 vec) => Vector2.Transform(vec, InvertedMatrix);

    public static Camera Default => Camera.Center(new Rect2F(0, 0, All.Screen.WindowSize.X, All.Screen.WindowSize.Y));

    public void UpdateMatrix()
    {
        if(NeedUpdate == false) { return; }
        //return Matrix.CreateScale(1, -1, 1) * Matrix.CreateTranslation(0, All.Screen.WindowSize.Y, 0);

        // Calculate the translation to set the bottom-left corner as Min
        Vector2 translation = new(-Min.X, -Max.Y);

        // Calculate the scaling factors to map the camera's size to the screen size
        float scaleX = All.Screen.WindowSize.X / Zoom.X;
        float scaleY = All.Screen.WindowSize.Y / Zoom.Y;

        // Adjust the translation to move the camera by half of the screen height

        // Create the transformation matrix with translation and scaling
        Matrix transform = Matrix.CreateTranslation(new Vector3(translation, 0)) *
                           Matrix.CreateScale(scaleX, scaleY, 1) *
                           Matrix.CreateTranslation(0, All.Screen.WindowSize.Y, 0);

        _TransformMatrix = transform;
        _InvertedMatrix = Matrix.Invert(transform);
        NeedUpdate = false;
    }

    private static List<Camera> Cameras = new() {  };
    public static void Push(Camera cam) => Cameras.Add(cam);
    public static Camera Pop()  => Cameras.Pop();
    public static Camera Peek() => Cameras.Peek();
    public static int Count => Cameras.Count;
}

