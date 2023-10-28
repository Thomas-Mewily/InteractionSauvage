using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using SimulationConsole;
using System;
using System.Collections.Generic;

namespace SimulationGraphique;

public class LeJeu : Game
{
    private GraphicsDeviceManager Graphics;
    private SpriteBatch SpriteBatch;
    private SimulationFactory SimuFact;
    public static Texture2D Pixel;
    SpriteFont Arial;

    public LeJeu()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        SimuFact = new SimulationFactory();
        for (int i = 0; i < 10; i++)
        {
            SimuFact.AddEntite();
        }
        
        SimuFact.Simu.Reset();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        Pixel = new Texture2D(GraphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });
        Arial = Content.Load<SpriteFont>("Arial");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //if (gameTime.TotalGameTime.TotalNanoseconds % 1000 == 0)
            SimuFact.Simu.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.GreenYellow);

        SpriteBatch.Begin();
        string label;

        foreach (var entite in SimuFact.Simu.ToutesLesEntites)
        {
            Vector2 position = new Vector2((float)entite.X * GraphicsDevice.Viewport.Width / SimuFact.Simu.Grille.Longueur,
                                           (float)entite.Y * GraphicsDevice.Viewport.Height / SimuFact.Simu.Grille.Hauteur);

            Random random = new Random((int)entite.Categorie.Categorie);
            Color couleurAleatoire = new Color(random.Next(256), random.Next(256), random.Next(256));

            int rayonDuCercle = entite.Taille * GraphicsDevice.Viewport.Width / SimuFact.Simu.Grille.Longueur;

            label = entite.Nom + " : " + entite.Etat.Nom;
            _spriteBatch.DrawDisk(position, rayonDuCercle, couleurAleatoire, label, Arial);
        }

        SpriteBatch.End();

        base.Draw(gameTime);
    }

}