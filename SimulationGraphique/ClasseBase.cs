using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using SimulationConsole;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using SimulationGraphique.Managers;

namespace SimulationGraphique;

public class ClasseBase
{
    public virtual void Load() { }
    public virtual void Unload() { }
    public virtual void Update() { }
    public virtual void Draw() { }

    public static GraphicsDevice GraphicsDevice => All.GraphicsDevice;
    public static GraphicsDeviceManager Graphics => All.Graphics;

    public static SpriteBatch SpriteBatch => All.SpriteBatch;
    public static ContentManager Content => All.Content;
    public static Assets Assets => All.Assets;
    public static Performance Perf => All.Performance;

}