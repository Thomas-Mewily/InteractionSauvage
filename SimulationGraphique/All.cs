using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using Microsoft.Xna.Framework.Content;
using SimulationGraphique.Managers;
using Render;
using System;
using Geometry;

namespace SimulationGraphique;

public class All
{
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static GraphicsDevice GraphicsDevice { get; private set; }
    public static Screen Screen { get; private set; }

    public static SpriteBatch SpriteBatch { get; private set; }
    public static ContentManager Content { get; private set; }
    public static Assets Assets { get; private set; }
    public static Input Input { get; private set; }

    public static Performance Performance { get; private set; }

    public static Scene SceneActuel; // Pas Ouf mais ça suffit pour le moment

    public static LeJeu Jeu { get; private set; }

    public static Color BackgroundColor = Color.Tomato;

    public static void Load(LeJeu jeu, Scene scene)
    {
        Jeu = jeu;
        Content = jeu.Content;
        SpriteBatch = jeu.SpriteBatch;
        Graphics = jeu.Graphics;
        GraphicsDevice = jeu.GraphicsDevice;


        Assets = new Assets();
        Performance = new Performance();
        Screen = new Screen();

        Input = new Input();
        Input.Load();

        SceneActuel = scene;

        Screen.Load();
        Assets.Load();
        SceneActuel.Load();
    }

    public static void Unload() 
    {
        Input.Unload();
        Screen.Unload();
        SceneActuel.Unload();
        Assets.Unload();
    }

    public static void Update(GameTime gameTime) 
    {
        Input.Update();
        Screen.Update();
        Performance.Update(gameTime);
        SceneActuel.Update();
    }

    public static int NbDebugText = 0;
    public static void Draw(GameTime gameTime) 
    {
        Camera.Push(CameraExtension.Default);

        NbDebugText = 0;
        GraphicsDevice.Clear(BackgroundColor);

        SceneActuel.Draw();

        Screen.Draw();
        Performance.Draw(gameTime);
        Input.Draw();

        if (Camera.Count != 1) { throw new Exception("La camera a pas été Pop()"); }
        Camera.Pop();
    }
}