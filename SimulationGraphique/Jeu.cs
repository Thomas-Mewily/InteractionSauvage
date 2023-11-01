using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;

using System;
using SimulationGraphique.Scenes;
using System.Collections.Generic;

namespace SimulationGraphique;

public class LeJeu : Game
{
    public GraphicsDeviceManager Graphics;
    public SpriteBatch SpriteBatch;

    public LeJeu()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = false;
        Graphics.PreferMultiSampling = true;

        //Window.IsBorderless = true;
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        All.Load(this, new SceneSimu());
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) 
        {
            Exit();
        }

        All.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        All.Draw(gameTime);

        base.Draw(gameTime);
    }
}