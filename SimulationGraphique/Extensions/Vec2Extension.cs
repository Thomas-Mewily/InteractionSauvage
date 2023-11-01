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

public static class Vec2Extension
{
    public static Vec2 ToWorldPosition(this Vec2 p) => Camera.Peek().ToWorldPosition(p);
}
