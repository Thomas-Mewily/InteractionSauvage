using Render;
using System;
using Geometry;
using InteractionSauvage;
using Microsoft.Xna.Framework;
using SimulationGraphique.Managers;
using Useful;

namespace SimulationGraphique.Animations;

public class MoutonAnim : Animation
{
    public override void Draw(Entite e)
    {
        Draw(e, All.Assets.Sheep, null, Color.White);
        //All.SpriteBatch.Draw(All.Assets.Sheep, e.Position, null, Color.White, e.Direction, ((Point2)All.Assets.Sheep.Bounds.Size) / 2, new Vec2(1.0f / All.Assets.Sheep.Width, 1.0f / All.Assets.Sheep.Height) * e.Rayon * 2, SpriteEffects.None, 0);
    }

    public override void Meurt(Entite e)
    {
        All.Assets.SheepSound.Play(0.2f, e.Rand.FloatUniform(-1, 1), 0);
    }
}