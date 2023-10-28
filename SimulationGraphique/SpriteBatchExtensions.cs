using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulationGraphique;

public static class SpriteBatchExtensions
{
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
        if (points.Length < 3)
            return;

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
    }
}
