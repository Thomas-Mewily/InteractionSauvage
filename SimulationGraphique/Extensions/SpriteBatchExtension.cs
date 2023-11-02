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

public static class SpriteBatchExtension
{
    private static bool Active = false;
    public static bool IsActive(this SpriteBatch spriteBatch) => Active;

    public static void Debut(this SpriteBatch spriteBatch) 
    {
        Active = true;
        
        spriteBatch.Begin(SpriteSortMode.Immediate, null, null, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.Peek().TransformMatrix);
    }

    public static void Fin(this SpriteBatch spriteBatch) { Active = false; spriteBatch.End(); }

    public static void DrawEllipse(this SpriteBatch spriteBatch, Vec2 pos, float radius, Color color)
        => DrawEllipse(spriteBatch, pos, new Vec2(radius, radius), color);

    public static void DrawEllipse(this SpriteBatch spriteBatch, Vec2 pos, Vec2 radius, Color color) 
    {
        spriteBatch.Draw(All.Assets.Circle, pos, null, color, 0, new Vec2(Assets.CircleRadius), radius / (float)Assets.CircleRadius, SpriteEffects.None, 0);
    }

    public static void DrawRectangle(this SpriteBatch spriteBatch, Vec2 pos, Vec2 size, Color color)
    {
        spriteBatch.Draw(All.Assets.Pixel, pos, null, color, 0, Vec2.Zero, size, SpriteEffects.None, 0);
    }

    public static void DrawRectangle(this SpriteBatch spriteBatch, Rect2F rect, Color color)
        => spriteBatch.DrawRectangle(rect.Min, rect.SizeY, color);

    public enum TextSize 
    {
        Normal = 18,
    }

    public static void DrawText(this SpriteBatch spriteBatch, Font font, string text, Vec2 pos, Vec2 coefCenter, Color color, TextSize size = TextSize.Normal) 
    {
        var m = (Vec2)font.MeasureString(text);
        float scale = (float)size / m.Y;

        pos -= m* scale* coefCenter;
        spriteBatch.DrawString(font, text, pos, color, 0, Vec2.Zero, scale, SpriteEffects.None, 0);
    }
    public static void DrawText(this SpriteBatch spriteBatch, Font font, string text, Vec2 pos, Color color, TextSize size = TextSize.Normal)
        => DrawText(spriteBatch, font, text, pos, new Vec2(0.5f, 0), color, size);
    public static void DrawText(this SpriteBatch spriteBatch, string text, Vec2 pos, Color color, TextSize size = TextSize.Normal)
        => DrawText(spriteBatch, All.Assets.Arial, text, pos, new Vec2(0.5f, 0), color, size);
    public static void DrawText(this SpriteBatch spriteBatch, string text, Vec2 pos, Vec2 coefCenter, Color color, TextSize size = TextSize.Normal)
        => DrawText(spriteBatch, All.Assets.Arial, text, pos, coefCenter, color, size);




    public static void DebugText(this SpriteBatch spriteBatch, string text)
    {
        bool wasActive = spriteBatch.IsActive();
        if (wasActive)
        {
            spriteBatch.End();
        }

        Camera.Push(Camera.Default);
        spriteBatch.Debut();

        spriteBatch.DrawText(text, new Vec2(0, All.NbDebugText++ * (int)TextSize.Normal), Vec2.Zero, Color.Black, TextSize.Normal);

        spriteBatch.Fin();
        Camera.Pop();
        if (wasActive)
        {
            spriteBatch.Debut();
        }
    }


    /*
    public static void DrawDisk(this SpriteBatch spriteBatch, Vector2 position, float radius, Color color, string label, SpriteFont font, int segments = 128)
    {
        Texture2D pixelTexture = CreatePixelTexture(spriteBatch.GraphicsDevice, color);
        float angleIncrement = MathHelper.TwoPi / segments;

        List<Vector2> circlePoints = new List<Vector2>();
        for (int i = 0; i < segments; i++)
        {
            float angle = angleIncrement * i;
            circlePoints.Add(position + radius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
        }

        DrawPolygon(spriteBatch, circlePoints.ToArray(), color);

        // Dessiner le texte au-dessus du cercle

        Vector2 textSize = font.MeasureString(label);
        Vector2 textPosition = new Vector2(position.X - textSize.X / 2, position.Y - radius - textSize.Y - 5); // Ajustez la position du texte selon votre préférence
        spriteBatch.DrawString(font, label, textPosition, Color.Black);
    }
    private static void DrawPolygon(SpriteBatch spriteBatch, Vector2[] points, Color color)
    {
        if (points.Length < 3) { return; }

        Texture2D pixelTexture = CreatePixelTexture(spriteBatch.GraphicsDevice, color);
        for (int i = 1; i < points.Length - 1; i++)
        {
            DrawTriangle(spriteBatch, points[0], points[i], points[i + 1], pixelTexture);
        }
    }

    
    private static void DrawTriangle(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Vector2 point3, Texture2D texture)
    {
        Vector2[] vertices = new Vector2[] { point1, point2, point3 };
        Vector2 center = new Vector2((point1.X + point2.X + point3.X) / 3f, (point1.Y + point2.Y + point3.Y) / 3f);
        float rotation = (float)Math.Atan2(center.Y - point1.Y, center.X - point1.X);
        float length = Vector2.Distance(point1, point2);
        Rectangle destinationRectangle = new Rectangle((int)point1.X, (int)point1.Y, (int)length, 1);

        spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0);
    }

    private static Texture2D CreatePixelTexture(GraphicsDevice graphicsDevice, Color color)
    {
        Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new[] { color });
        return texture;
    }*/


    public static void DrawTriangle(this SpriteBatch spriteBatch, Vec2 a, Vec2 b, Vec2 c, Color color)
    {
        // Define vertices and their positions
        VertexPositionColor[] vertices = new VertexPositionColor[3];
        vertices[0] = new VertexPositionColor(new Vector3(a.X, a.Y, 0), color);
        vertices[1] = new VertexPositionColor(new Vector3(b.X, b.Y, 0), color);
        vertices[2] = new VertexPositionColor(new Vector3(c.X, c.Y, 0), color);


        // Draw the triangle
        /*
        spriteBatch.GraphicsDevice.DrawUserPrimitives(
            PrimitiveType.TriangleList,
            vertices,
            0,
            1);*/
        spriteBatch.Fin();

        var _basicEffect = new BasicEffect(All.GraphicsDevice);

        var cam = Camera.Peek();
        _basicEffect.World = Matrix.CreateOrthographicOffCenter(cam.Min.X, cam.Max.X, cam.Max.Y, cam.Min.Y, 0, 1);

        EffectTechnique effectTechnique = _basicEffect.Techniques[0];
        EffectPassCollection effectPassCollection = effectTechnique.Passes;

        foreach (EffectPass pass in effectPassCollection)
        {
            pass.Apply();

            All.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
        }

        spriteBatch.Debut();
    }

    public static void DrawArc(this SpriteBatch spriteBatch, Vec2 pos, Vec2 radius, Angle direction, Angle fieldOfView, Color color, int precision = 32)
    {
        direction -= fieldOfView / 2;
        Angle toAdd = fieldOfView / precision;

        for (int i = 0; i < precision; i++) 
        {
            var d = direction;
            direction += toAdd;
            spriteBatch.DrawTriangle(pos, pos + new Vec2(radius.X * d.Cos, radius.Y * d.Sin), pos + new Vec2(radius.X * direction.Cos, radius.Y * direction.Sin), color);
        }
    }

}
