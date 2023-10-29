using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InteractionSauvage;
using Microsoft.Xna.Framework.Content;
using SimulationGraphique.Managers;

namespace SimulationGraphique;

public class All
{
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static GraphicsDevice GraphicsDevice { get; private set; }

    public static SpriteBatch SpriteBatch { get; private set; }
    public static ContentManager Content { get; private set; }
    public static Assets Assets { get; private set; }

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

        SceneActuel = scene;

        Assets.Load();
        SceneActuel.Load();
    }

    public static void Unload() 
    {
        SceneActuel.Unload();
        Assets.Unload();
    }

    public static void Update(GameTime gameTime) 
    {
        Performance.Update(gameTime);
        SceneActuel.Update();
    }

    public static void Draw(GameTime gameTime) 
    {
        GraphicsDevice.Clear(All.BackgroundColor);

        SpriteBatch.Begin();
        Performance.Draw(gameTime);
        SceneActuel.Draw();
        SpriteBatch.End();

    }

}