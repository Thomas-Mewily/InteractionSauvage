
using Geometry;
using InteractionSauvage;
using InteractionSauvage.Passifs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimulationGraphique.Managers;
using System.IO;
using Useful;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SimulationGraphique;

public class AnimalSprite 
{
    public Tex2 Queue;
    public Tex2 Leg;
    public Tex2 Head;
    public Tex2 Body;

    public AnimalSprite(string nom, string etat)
    {
        Queue= All.Assets.LoadTexture(Path.Combine(nom, "queue_" + etat));
        Leg  = All.Assets.LoadTexture(Path.Combine(nom, "leg_" + etat));
        Head = All.Assets.LoadTexture(Path.Combine(nom, "head_" + etat));
        Body = All.Assets.LoadTexture(Path.Combine(nom, "body_" + etat));
    }
}

public class DefaultAnimalAnimation : Animation
{
    public string Nom;

    AnimalSprite Dead;
    AnimalSprite Normal;
    AnimalSprite Sleep;
    Color Colored;

    public DefaultAnimalAnimation(string nom) 
    {
        Nom = nom;
        Dead   = new AnimalSprite(nom, "dead");
        Normal = new AnimalSprite(nom, "normal");
        Sleep  = new AnimalSprite(nom, "sleep");
    }

    public override AnimationBase Clone()
    {
        DefaultAnimalAnimation anim = new DefaultAnimalAnimation(Nom);
        anim.Colored = Colored;
        return anim;
    }
    public override void Load(Entite e)
    {
        Colored = new Color(255-e.Rand.Next(0, 255)/2, 255 - e.Rand.Next(0, 255) / 2, 255 - e.Rand.Next(0, 255) / 2);
    }

    public override void Draw(Entite e)
    {
        AnimalSprite sprite = e.Dors ? Sleep : Normal;

        Angle a = e.Direction;
        Angle legsAngleAdd = (e.OldPosition - e.Position).HaveLength ? Angle.FromDegree(e.Simu.Time.T*7) : Angle.Zero;
        Angle legsAngle = Angle.FromDegree(25);

        var legLeft  =  legsAngleAdd.Sin;
        var legRight = (legsAngleAdd + Angle.FromDegree(90)).Sin;

        Draw(e, sprite.Leg, null, Vec2.FromAngle(a, (e.Rayon * 0.4f) * legLeft ), Colored, a + legLeft  * legsAngle);
        Draw(e, sprite.Leg, null, Vec2.FromAngle(a, (e.Rayon * 0.4f) * legRight), Colored, a + legRight * legsAngle, SpriteEffects.FlipVertically);
        Draw(e, sprite.Body, null, Vec2.Zero, Colored, a);
        Draw(e, sprite.Head, null, Vec2.Zero, Colored, e.HeadDirection);

        Draw(e, sprite.Queue, null, Vec2.Zero, Colored, a + Angle.FromDegree(e.Simu.Time.T * 9).Cos * Angle.FromDegree(45));

        //All.SpriteBatch.Draw(All.Assets.Sheep, e.Position, null, Color.White, e.Direction, ((Point2)All.Assets.Sheep.Bounds.Size) / 2, new Vec2(1.0f / All.Assets.Sheep.Width, 1.0f / All.Assets.Sheep.Height) * e.Rayon * 2, SpriteEffects.None, 0);
    }

    public override void Meurt(Entite e)
    {
        All.Assets.SheepSound?.Play(1f, e.Rand.FloatUniform(-1, 1), 0);
    }

}