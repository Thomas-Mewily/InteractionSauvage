
using Geometry;
using InteractionSauvage;
using InteractionSauvage.Passifs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimulationGraphique.Managers;
using Useful;

namespace SimulationGraphique;

public struct AnimalSprite 
{
    public Tex2 Leg;
    public Tex2 Head;
    public Tex2 Body;

    public AnimalSprite(string nom, string etat)
    {
        Leg  = All.Assets.LoadTexture(nom + "_leg_" + etat);
        Head = All.Assets.LoadTexture(nom + "_head_" + etat);
        Body = All.Assets.LoadTexture(nom + "_body_" + etat);
    }
}

public class DefaultAnimalAnimation : Animation
{
    public string Nom;

    AnimalSprite Dead;
    AnimalSprite Normal;
    AnimalSprite Sleep;

    public DefaultAnimalAnimation(string nom) 
    {
        Nom = nom;
        Dead   = new AnimalSprite(nom, "dead");
        Normal = new AnimalSprite(nom, "normal");
        Sleep  = new AnimalSprite(nom, "sleep");
    }

    public override void Draw(Entite e)
    {
        AnimalSprite sprite = Normal;

        Angle a = e.Direction;
        Angle legsAngleAdd = (e.OldPosition - e.Position).HaveLength ? Angle.FromDegree(e.Simu.Time.T*6) : Angle.Zero;
        Angle legsAngle = Angle.FromDegree(25);

        Draw(e, sprite.Leg,  null, Color.White, a+ legsAngleAdd.Sin * legsAngle);
        Draw(e, sprite.Leg,  null, Color.White, a+ legsAngleAdd.Cos * legsAngle, SpriteEffects.FlipVertically);
        Draw(e, sprite.Body, null, Color.White, a);
        Draw(e, sprite.Head, null, Color.White, e.Target != null ? new Vec2(e.Position, e.Target.Position).Angle : a);


        //All.SpriteBatch.Draw(All.Assets.Sheep, e.Position, null, Color.White, e.Direction, ((Point2)All.Assets.Sheep.Bounds.Size) / 2, new Vec2(1.0f / All.Assets.Sheep.Width, 1.0f / All.Assets.Sheep.Height) * e.Rayon * 2, SpriteEffects.None, 0);
    }

    public override void Meurt(Entite e)
    {
        All.Assets.SheepSound.Play(1f, e.Rand.FloatUniform(-1, 1), 0);
    }

}