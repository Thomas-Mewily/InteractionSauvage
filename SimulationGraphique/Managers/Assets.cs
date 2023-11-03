using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace SimulationGraphique.Managers;

public class Assets : ClasseBase
{
    public Texture2D  Pixel { get; private set; }

    public const int CircleRadius = 256;
    public Texture2D  Circle { get; private set; }

    public SpriteFont Arial { get; private set; }

    public Texture2D  Sheep { get; private set; }
    public Texture2D  Grass { get; private set; }

    public SoundEffect GrassSound { get; private set; }
    public SoundEffect SheepSound { get; private set; }

    public override void Load()
    {
        Pixel = new Texture2D(All.GraphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });

        Circle = Content.Load<Texture2D>("circle");
        Sheep  = Content.Load<Texture2D>("sheep");
        Grass  = Content.Load<Texture2D>("grass");
        Arial  = Content.Load<SpriteFont>("Arial");

        GrassSound = Content.Load<SoundEffect>("grass_mc");
        SheepSound = Content.Load<SoundEffect>("sheep_mc");
    }

    public override void Unload() 
    {
        // Unload the platipus texture here
    }
}