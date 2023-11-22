using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;

using System;
using System.Collections.Generic;
using static SimulationGraphique.SpriteBatchExtension;
using Geometry;
using Render;
using SimulationGraphique.Managers;
using SimulationGraphique.Animations;

namespace SimulationGraphique.Scenes;

public struct RedParticles 
{
    public Vec2 Pos;
    public float Radius;

    public RedParticles(Vec2 pos, float radius)
    {
        Pos = pos;
        Radius = radius;
    }
}

public class SceneSimu : Scene
{
    private Camera Cam;
    public bool Paused = false;
    public Entite Target = null;

    public SimulationFactoryGraphique SimuFact;
    public Simulation Simu => SimuFact.Simu;
    public Grille Grille => Simu.Grille;

    public List<RedParticles> Particles = new();

    public override void Load()
    {
        SimuFact = new SimulationFactoryGraphique();
        for (int i = 0; i < 20; i++)
        {
            SimuFact.AddEntite();
        }
        SimuFact.AddSniperSheep();

        for (int i = 0; i < 4; i++)
        {
            Simu.Add(SimuFact.GenerateWolf());
        }

        for(int i = 0; i < 300; i++) 
        {
            //Simu.Add(SimuFact.GenerateMouton());
        }


        Simu.Reset();

        Cam = Camera.Center(new Rect2F(0, 0, Simu.Grille.Longueur, Simu.Grille.Hauteur));
        //Cam.Size *= 2;
        Console.WriteLine("P to pause");
        Console.WriteLine("Space to go fast");
        Console.WriteLine("Arrows Keys/WASD to move");
        Console.WriteLine("Mouse Wheel to zoom");
        Console.WriteLine("A : Add CP");
        Console.WriteLine("Z : Delete CP");
        Console.WriteLine("R : RollBack CP");
    }

    public override void Update()
    {
        Vec2 add = Vec2.Zero;

        if(Keys.Right.IsDown())// || Keys.D.IsDown())
        {
            add.X++;
        }
        if (Keys.Left.IsDown())// || Keys.A.IsDown())
        {
            add.X--;
        }
        if (Keys.Up.IsDown())// || Keys.W.IsDown())
        {
            add.Y--;
        }
        if (Keys.Down.IsDown())// || Keys.S.IsDown())
        {
            add.Y++;
        }
        if (Keys.P.IsPressed()) { Paused = !Paused; }

        if (Keys.A.IsPressed()) 
        {
            Simu.CheckPointAdd();
        }
        if (Keys.Z.IsPressed() && Simu.NbCheckPoint > 0)
        {
            Simu.CheckPointRemove();
        }
        if (Keys.E.IsPressed() && Simu.NbCheckPoint > 0)
        {
            Simu.CheckPointRollBack();
        }


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

        if (Target != null) 
        {
            if((Cam.Y - Target.Y) > Cam.Rect.SizeY / 2) 
            {
                Cam.Y = (Cam.Y * 199 + Target.Y) / 200;
            }
            if ((Cam.X - Target.X) > Cam.Rect.SizeX / 2)
            {
                Cam.X = (Cam.X * 199 + Target.X) / 200;
            }
        }

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

        foreach (var e in Simu.Grille.EntitesDansRectangle(Camera.Peek().Rect))
        {
            e.Animation?.Draw(e);

            if (All.Input.MouseLeftPressed  && e.IsSelected())
            {
                Target = e;
            }
            
            if(e.Animation is WolfAnim && (e.Position-e.OldPosition).HaveLength) 
            {
                int iMax = 3;
                for(int i = 0; i < 3; i++) 
                {
                    float coef = i / (float)iMax; // Note que le (float) est à droite et pas à gauche Mathis ;)
                    var pos = e.Position * coef + e.OldPosition * (1 - coef);
                    Particles.Add(new RedParticles(pos + Vec2.FromAngle(e.HeadDirection + Angle.FromDegree(12), e.Rayon * 0.55f), e.Rayon * 0.1f));
                    Particles.Add(new RedParticles(pos + Vec2.FromAngle(e.HeadDirection - Angle.FromDegree(12), e.Rayon * 0.55f), e.Rayon * 0.1f));
                }
            }
        }

        if (All.Input.MouseRightPressed) 
        {
            Target = null;
        }


        if (Target != null) 
        {
            Target.Animation.DrawChampsVision(Target);
            Target.Animation.Draw(Target);


            foreach (var v in Target.EntitesVisibles())
            {
                SpriteBatch.DrawEllipse(v.Position, v.Rayon, new Color(0, 255, 0, 128));
            }

            if(Target.Target != null) 
            {
                SpriteBatch.DrawEllipse(Target.Target.Position, Target.Target.Rayon, new Color(255, 0, 0, 255));
            }

            Target.Animation.DrawExtraInfo(Target);

        }

        SpriteBatch.Fin();

        SpriteBatch.Debut(BlendState.Additive);
        for (int i = Particles.Count - 1; i >= 0; i--)
        {
            RedParticles red = Particles[i];
            if (!Paused)
            {
                red.Radius -= 0.002f;
            }
            if (red.Radius < 0) { Particles.RemoveAt(i); continue; }
            SpriteBatch.DrawEllipse(red.Pos, red.Radius, new Color(255,0,0, 128));
            Particles[i] = red;
        }
        SpriteBatch.Fin();

        Camera.Pop();
        SpriteBatch.DebugText(Simu.ToutesLesEntites.Count + " entitées");
        if (Paused) 
        {
            SpriteBatch.DebugText("[P]aused");
        }
        SpriteBatch.DebugText("CP : " + Simu.NbCheckPoint);
        SpriteBatch.DebugText("A : Add CP");
        SpriteBatch.DebugText("Z : Delete CP");
        SpriteBatch.DebugText("E : RollBack CP");

    }
}