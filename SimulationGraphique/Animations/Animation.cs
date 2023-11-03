
using Geometry;
using InteractionSauvage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimulationGraphique;

public abstract class Animation : AnimationBase
{
    public override void DrawExtraInfo(Entite e)
    {
        All.SpriteBatch.DrawText(e.Nom + " : " + e.Etat.Nom + (e.Target != null ? (", " + e.Target) : ", null"), e.Position - new Vec2(0, e.Rayon+4), Color.Black);
    }

    public void Draw(Entite e, Tex2 texture, Rectangle? r, Color c, SpriteEffects effects = SpriteEffects.None, float zoom = 1.05f) 
    {
        Rectangle rec = (r == null ? texture.Bounds : r.Value);
        All.SpriteBatch.Draw(texture, e.Position, r, c, e.Direction, new Vector2(rec.Width/2, rec.Height/2), new Vec2(1.0f / rec.Width, 1.0f / rec.Height) * e.Rayon * 2 * zoom, effects, 0);
    }

    public override void DrawChampsVision(Entite e) 
    {
        All.SpriteBatch.DrawEllipse(e.Position, e.RayonVision+ Entite.BonusRadiusBigEntityMax, new Color(128, 128, 128, 3));
        All.SpriteBatch.DrawArc(e.Position, e.RayonVision, e.Direction, e.ChampsVision, new Color(12, 82, 165, 128));
        All.SpriteBatch.DrawArc(e.Position, e.RayonVision, e.Direction+Angle.AngleFromOne(0.5f), Angle.AngleFromOne(1)-e.ChampsVision, new Color(255, 64, 0, 128));

    }

}