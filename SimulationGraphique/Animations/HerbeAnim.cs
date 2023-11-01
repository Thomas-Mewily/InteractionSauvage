using Render;
using System;
using Geometry;
using InteractionSauvage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimulationGraphique.Animations;

public class HerbeAnim : Animation
{
    public override void Draw(Entite e)
    {
        Draw(e, All.Assets.Grass, null, Color.White);
    }
}