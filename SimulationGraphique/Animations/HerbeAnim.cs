using Render;
using System;
using Geometry;
using InteractionSauvage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Useful;

namespace SimulationGraphique.Animations;

public class HerbeAnim : Animation
{
    public override AnimationBase Clone()
    {
        HerbeAnim anim = new HerbeAnim();
        return anim;
    }
    public override void Draw(Entite e)
    {
        Draw(e, All.Assets.Grass, null, Vec2.Zero, Color.White);
    }

    public override void Meurt(Entite e)
    {
        All.Assets.GrassSound.Play(1f, e.Rand.FloatUniform(-1,1), 0);
    }
}