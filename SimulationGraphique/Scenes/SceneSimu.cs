﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;

using System;
using System.Collections.Generic;
using static SimulationGraphique.SpriteBatchExtension;
using Geometry;
using Render;
using SimulationGraphique.Managers;

namespace SimulationGraphique.Scenes;

public class SceneSimu : Scene
{
    private Camera Cam;
    public bool Paused = false;
    public Entite? Target = null;

    public SimulationFactoryGraphique SimuFact;
    public Simulation Simu => SimuFact.Simu;
    public Grille Grille => Simu.Grille;

    public override void Load()
    {
        SimuFact = new SimulationFactoryGraphique();
        for (int i = 0; i < 50; i++)
        {
            SimuFact.AddEntite();
        }

        Simu.Reset();

        Cam = Camera.Center(new Rect2F(0, 0, Simu.Grille.Longueur, Simu.Grille.Hauteur));
        //Cam.Size *= 2;
        Console.WriteLine("P to pause");
        Console.WriteLine("Space to go fast");
        Console.WriteLine("Arrows Keys/WASD to move");
        Console.WriteLine("Mouse Wheel to zoom");
    }

    public override void Update()
    {
        Vec2 add = Vec2.Zero;

        if(Keys.Right.IsDown() || Keys.D.IsDown())
        {
            add.X++;
        }
        if (Keys.Left.IsDown() || Keys.A.IsDown())
        {
            add.X--;
        }
        if (Keys.Up.IsDown() || Keys.W.IsDown())
        {
            add.Y--;
        }
        if (Keys.Down.IsDown() || Keys.S.IsDown())
        {
            add.Y++;
        }
        if (Keys.P.IsPressed()) { Paused = !Paused; }

        Cam.Position += add * Cam.Zoom.X / 100;

        if(Paused == false) 
        {
            int nbStep = 1;
            if (Keys.Space.IsDown())
            {
                nbStep *= 16;
            }
            Simu.Update(nbStep);
        }


        float delta = All.Input.MouseOld.ScrollWheelValue- All.Input.Mouse.ScrollWheelValue;
        Cam.Zoom *= 1 + delta / 1000;
    }

    public override void Draw()
    {



        Camera.Push(Cam);
        SpriteBatch.Debut();

        SpriteBatch.DrawRectangle(Vec2.Zero, Simu.Grille.Size, new Color(168,90,38));

        for(int i = 0; i < Grille.NbCaseLongueur; i ++) 
        {
            for (int j = 0; j < Grille.NbCaseHauteur; j ++)
            {
                if(((j+i)&1) == 0) 
                {
                    SpriteBatch.DrawRectangle(new Rect2F(i * Grille.TailleCase, j * Grille.TailleCase, Grille.TailleCase, Grille.TailleCase), new Color(128, 70, 28));
                }
            }
        }

        foreach (var e in Simu.ToutesLesEntites)
        {
            e.Animation?.Draw(e);

            if (All.Input.Mouse.LeftButton == ButtonState.Pressed && e.IsSelected())
            {
                Target = e;
            }
        }

        if(Target != null) 
        {
            Target.Animation.DrawChampsVision(Target);
            Target.Animation.Draw(Target);
            Target.Animation.DrawExtraInfo(Target);

            foreach(var v in Target.EntitesVisibles()) 
            {
                SpriteBatch.DrawEllipse(v.Position, v.Rayon, new Color(0,255,0, 128));
            }
        }

        SpriteBatch.Fin();
        Camera.Pop();

        if (Paused) 
        {
            SpriteBatch.DebugText("[P]aused");
        }
    }
}