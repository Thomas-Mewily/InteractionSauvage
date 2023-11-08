
using Geometry;
using InteractionSauvage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Render;
using static SimulationGraphique.SpriteBatchExtension;

namespace SimulationGraphique;

public abstract class Animation : AnimationBase
{
    private static int IdxInfo = 0;
    private void DrawTextInfo(Entite e, string Text) 
    {
        All.SpriteBatch.DrawText(Text, e.Position - new Vec2(0, e.Rayon+ 5 - (IdxInfo++)* Camera.Peek().Rect.SizeY/20), Color.Black);
    }

    public override void DrawExtraInfo(Entite e)
    {
        IdxInfo = 0;
        //All.SpriteBatch.DrawText(e.Nom + " : " +  + (e.Target != null ? (", " + e.Target) : ", null"), e.Position - new Vec2(0, e.Rayon+4), Color.Black);

        DrawTextInfo(e, e.Nom);
        DrawTextInfo(e, e.Etat.Nom);
        DrawTextInfo(e, "vise " + (e.Target != null ? e.Target.ToString() : ""));
        DrawTextInfo(e, ((int)(e.Energie*10000)/ 100).ToString().PadLeft(5) + " % energie");
    }

    public void Draw(Entite e, Tex2 texture, Rectangle? r, Color c, SpriteEffects effects = SpriteEffects.None, float zoom = 1.05f) => Draw(e, texture, r, c, e.Direction, effects, zoom);
    public void Draw(Entite e, Tex2 texture, Rectangle? r, Color c, Angle angle, SpriteEffects effects = SpriteEffects.None, float zoom = 1.05f) 
    {
        Rectangle rec = (r == null ? texture.Bounds : r.Value);
        All.SpriteBatch.Draw(texture, e.Position, r, c, angle, new Vector2(rec.Width/2, rec.Height/2), new Vec2(1.0f / rec.Width, 1.0f / rec.Height) * e.Rayon * 2 * zoom, effects, 0);
    }

    public override void DrawChampsVision(Entite e) 
    {
        All.SpriteBatch.DrawEllipse(e.Position, e.RayonVision+ Entite.BonusRadiusBigEntityMax, new Color(128, 128, 128, 64));
        All.SpriteBatch.DrawArc(e.Position, e.RayonVision, e.Direction, e.ChampsVision, new Color(12, 82, 165, 128));
        All.SpriteBatch.DrawArc(e.Position, e.RayonVision, e.Direction+Angle.AngleFromOne(0.5f), Angle.AngleFromOne(1)-e.ChampsVision, new Color(255, 64, 0, 128));

    }

}