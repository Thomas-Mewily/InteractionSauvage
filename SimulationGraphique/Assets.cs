﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using SimulationConsole;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace SimulationGraphique;

public class Assets : ClasseBase
{
    public Texture2D  Pixel { get; private set; }

    public const int CircleRadius = 256;
    public Texture2D  Circle { get; private set; }

    public SpriteFont Arial { get; private set; }

    public override void Load()
    {
        Pixel = new Texture2D(All.GraphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });

        Circle = Content.Load<Texture2D>("circle");
        Arial = Content.Load<SpriteFont>("Arial");
    }

    public override void Unload() 
    {
        // Unload the platipus texture here
    }
}