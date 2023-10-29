using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using SimulationConsole;
using System;
using System.Collections.Generic;
using static SimulationGraphique.SpriteBatchExtension;

namespace SimulationGraphique.Scenes;

public class SceneSimu : Scene
{
    public SimulationFactory SimuFact;

    public override void Load()
    {
        SimuFact = new SimulationFactory();
        for (int i = 0; i < 500; i++)
        {
            SimuFact.AddEntite();
        }

        SimuFact.Simu.Reset();
    }

    public override void Update()
    {
        SimuFact.Simu.Update();
    }

    public override void Draw()
    {
        SpriteBatch.Begin();
        string label;

        foreach (var entite in SimuFact.Simu.ToutesLesEntites)
        {
            Vector2 position = new Vector2(entite.X * GraphicsDevice.Viewport.Width / SimuFact.Simu.Grille.Longueur,
                                           entite.Y * GraphicsDevice.Viewport.Height / SimuFact.Simu.Grille.Hauteur);

            //Random random = new Random((int)entite.Categorie.Categorie);
            //Color couleurAleatoire = new Color(random.Next(256), random.Next(256), random.Next(256));

            var rayonDuCercle = entite.Taille * GraphicsDevice.Viewport.Width / SimuFact.Simu.Grille.Longueur;

            label = entite.Nom + " : " + entite.Etat.Nom;
            SpriteBatch.DrawEllipse(position, rayonDuCercle, Color.White);

            // petetite bidouille pour le texte
            //SpriteBatch.DrawText("Beep Beep, I'm a Sheep", position + new Vec2(0, -32), Color.Black);
            SpriteBatch.DrawText(label, position + new Vec2(0, -32), Color.Black);
        }

        SpriteBatch.End();
    }
}