
using Geometry;
using InteractionSauvage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimulationGraphique;

public abstract class Animation : AnimationBase
{
    public override void DrawExtraInfo(Entite e)
    {
        All.SpriteBatch.DrawText(e.Nom + " : " + e.Etat.Nom, e.Position - new Vec2(0, e.Rayon + 32), Color.Black);
    }

    public void Draw(Entite e, Tex2 texture, Rectangle? r, Color c, SpriteEffects effects = SpriteEffects.None, float zoom = 1.05f) 
    {
        Rectangle rec = (r == null ? texture.Bounds : r.Value);
        All.SpriteBatch.Draw(texture, e.Position, r, Color.White, e.Direction, new Vector2(rec.Width/2, rec.Height/2), new Vec2(1.0f / rec.Width, 1.0f / rec.Height) * e.Rayon * 2 * zoom, effects, 0); ;
    }
}