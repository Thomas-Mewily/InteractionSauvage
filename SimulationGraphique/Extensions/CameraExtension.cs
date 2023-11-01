using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Useful;
using Geometry;
using SimulationGraphique.Managers;
using Render;

namespace SimulationGraphique;

public static class CameraExtension
{
    public static Camera Default => Camera.Center(new Rect2F(0, 0, All.Screen.WindowSize.X, All.Screen.WindowSize.Y));


    public static Matrix TransformMatrix(this Camera cam)
    {

        //return Matrix.CreateScale(1, -1, 1) * Matrix.CreateTranslation(0, All.Screen.WindowSize.Y, 0);

        // Calculate the translation to set the bottom-left corner as Min
        Vector2 translation = new Vector2(-cam.Min.X, -cam.Max.Y);

        // Calculate the scaling factors to map the camera's size to the screen size
        float scaleX = All.Screen.WindowSize.X / cam.Zoom.X;
        float scaleY = All.Screen.WindowSize.Y / cam.Zoom.Y;

        // Adjust the translation to move the camera by half of the screen height

        // Create the transformation matrix with translation and scaling
        Matrix transform = Matrix.CreateTranslation(new Vector3(translation, 0)) *
                           Matrix.CreateScale(scaleX, scaleY, 1) *
                           Matrix.CreateTranslation(0, All.Screen.WindowSize.Y, 0);

        return transform;
    }
}
