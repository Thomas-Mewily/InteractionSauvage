using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using SimulationConsole;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Useful;

namespace SimulationGraphique;

public static class Extension
{
    public static Vec2 ToVec(this Angle a) => new(a.Cos, a.Sin);
}