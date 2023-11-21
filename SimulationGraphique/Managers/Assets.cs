using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SimulationGraphique.Managers;

public class Assets : ClasseBase
{
    public Texture2D  Pixel { get; private set; }

    public const int CircleRadius = 256;
    public Texture2D  Circle { get; private set; }

    public SpriteFont Arial { get; private set; }

    public Texture2D  Grass { get; private set; }

    public SoundEffect GrassSound { get; private set; }
    public SoundEffect SheepSound { get; private set; }

    public Dictionary<string, SoundEffect> SoundEffects = new();
    public Dictionary<string, Texture2D> Textures = new();

    public Texture2D LoadTexture(string path) 
    {
        if (Textures.TryGetValue(path, out Tex2 value)) { return value;  }
        Texture2D t = Content.Load<Texture2D>(path);
        Textures.Add(path, t);
        return t;
    }

    public SoundEffect LoadSoundEffect(string path)
    {
        if (SoundEffects.TryGetValue(path, out SoundEffect value)) { return value; }
        SoundEffect t = Content.Load<SoundEffect>(path);
        SoundEffects.Add(path, t);
        return t;
    }

    public override void Load()
    {
        Pixel = new Texture2D(All.GraphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });

        Circle = LoadTexture("circle");
        Grass  = LoadTexture("grass");

        Arial  = Content.Load<SpriteFont>("Arial");

        GrassSound = LoadSoundEffect("grass_mc");
        //SheepSound = LoadSoundEffect("sheep_mc");
    }

    public override void Unload() 
    {
        // Unload the platipus texture here
    }
}