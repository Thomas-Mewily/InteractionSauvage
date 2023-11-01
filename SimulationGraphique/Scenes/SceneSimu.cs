using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using SimulationConsole;
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

    public SimulationFactory SimuFact;
    public Simulation Simu => SimuFact.Simu;

    public override void Load()
    {
        SimuFact = new SimulationFactory();
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

        Cam.Position += add * Cam.Zoom.X / 100;

        int nbStep = 1;
        if (Keys.Space.IsDown()) 
        {
            nbStep *= 4;
        }
        Simu.Update(nbStep);

        float delta = All.Input.MouseOld.ScrollWheelValue- All.Input.Mouse.ScrollWheelValue;

        Cam.Zoom *= 1 + delta / 1000;
    }

    public override void Draw()
    {



        Camera.Push(Cam);
        SpriteBatch.Debut();

        SpriteBatch.DrawRectangle(Vec2.Zero, Simu.Grille.Size, Color.Chocolate);

        //SpriteBatch.DrawEllipse(Simu.Grille.Size / 2, Simu.Grille.Size, Color.Purple);

        foreach (var e in Simu.ToutesLesEntites)
        {
            //string label;
            /*
            Vec2 position = new Vec2(entite.X * GraphicsDevice.Viewport.Width / SimuFact.Simu.Grille.Longueur,
                                           entite.Y * GraphicsDevice.Viewport.Height / SimuFact.Simu.Grille.Hauteur);

            //Random random = new Random((int)entite.Categorie.Categorie);
            //Color couleurAleatoire = new Color(random.Next(256), random.Next(256), random.Next(256));

            var rayonDuCercle = entite.Taille * GraphicsDevice.Viewport.Width / SimuFact.Simu.Grille.Longueur;

            label = entite.Nom + " : " + entite.Etat.Nom;
            SpriteBatch.DrawEllipse(position, rayonDuCercle, Color.White);

            // petetite bidouille pour le texte
            //SpriteBatch.DrawText("Beep Beep, I'm a Sheep", position + new Vec2(0, -32), Color.Black);
            SpriteBatch.DrawText(label, position + new Vec2(0, -32), Color.Black);
            */

            //SpriteBatch.DrawEllipse(e.Position, e.Rayon, Color.White);
            SpriteBatch.Draw(Assets.Sheep, e.Position, null, Color.White, e.Direction, ((Point2)Assets.Sheep.Bounds.Size) / 2, new Vec2(1.0f/ Assets.Sheep.Width, 1.0f / Assets.Sheep.Height)* e.Rayon*2, SpriteEffects.None, 0);
        }



        SpriteBatch.Fin();
        Camera.Pop();


    }
}