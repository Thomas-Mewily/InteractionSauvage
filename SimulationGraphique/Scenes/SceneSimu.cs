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

namespace SimulationGraphique.Scenes;

public class SceneSimu : Scene
{
    private Camera Cam;
    public bool Paused = false;

    public SimulationFactoryGraphique SimuFact;
    public Simulation Simu => SimuFact.Simu;

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
                nbStep *= 4;
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

        Entite target = null;

        foreach (var e in Simu.ToutesLesEntites)
        {
            e.Animation?.Draw(e);

            if ((Camera.Peek().WorldPosition(All.Input.Mouse.X, All.Input.Mouse.Y) - e.Position).LengthSquared < e.Rayon * e.Rayon && e.Animation != null)
            {
                target = e;
            }
            //SpriteBatch.Draw(Assets.Sheep, e.Position, null, Color.White, e.Direction, ((Point2)Assets.Sheep.Bounds.Size) / 2, new Vec2(1.0f/ Assets.Sheep.Width, 1.0f / Assets.Sheep.Height)* e.Rayon*2, SpriteEffects.None, 0);
        }

        if(target != null) 
        {
            target.Animation.DrawChampsVision(target);
            target.Animation.Draw(target);
            target.Animation.DrawExtraInfo(target);
        }

        SpriteBatch.Fin();
        Camera.Pop();

        if (Paused) 
        {
            SpriteBatch.DebugText("[P]aused");
        }
    }
}