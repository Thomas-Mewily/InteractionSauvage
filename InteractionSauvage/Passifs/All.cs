using Geometry;
using System.Numerics;
using Useful;
using static InteractionSauvage.Categories;

namespace InteractionSauvage.Passifs;

public class Attendre : Passif
{
    public override void Execute() { }

    public override Passif Clone()
    {
        return new Attendre();
    }
}

public class Dormir : Passif
{
    public override void Execute()
    {
        E.Energie += Entite.EnergiePerduParTour + 300 * Entite.EnergiePerduParTour;
    }

    public override Passif Clone()
    {
        return new Dormir();
    }
}

public class Marcher : Passif
{
    public Marcher(float coef = 1) : base(coef) { }
    public override void Execute()
    {
        E.Avancer(Coef);
    }

    public override Passif Clone()
    {
        return new Marcher(Coef);
    }
}

public class Sprinter : Passif
{
    public Sprinter(float coef = 1) : base(coef) { }
    public override void Execute()
    {
        E.Vitesse += Vec2.FromAngle(E.Direction, E.Rayon * Coef);
    }
    public override Passif Clone() => new Sprinter(Coef);
}

public class MarcherAleatoire : Passif
{
    public MarcherAleatoire(float coef = 1) : base(coef) { }
    public override void Debut()
    {
        E.RngDirection();
    }

    public override void Execute()
    {
        if (Rand.NextDouble() < 0.01) E.RngDirection();

        E.Avancer(Coef);
    }

    public override Passif Clone()
    {
        return new MarcherAleatoire(Coef);
    }
}

public class MarcherVersNouriture : Passif
{
    public MarcherVersNouriture(float coef = 1) : base(coef) { }
    public override void Debut()
    {
        E.NouritureDirection();
    }

    public override void Execute()
    {
        if(E.Target != null && !E.Target.Vivant)
        {
            E.Target = null;
        }

        if (E.Target == null || E.DistanceVisionTo(E.Target) > E.RayonVision)
        {
            E.NouritureDirection();
        }
        else
        {
            E.DirectionTarget = (E.Target.Position- E.Position).Angle; //Angle.FromRadian(float.Atan2((E.Target!.Y - E.Y), (E.Target!.X - E.X))); ;
        }


        float c = Coef;
        if (E.Target != null) 
        {
            var d = new Vec2(E.Target.Position, E.Position).Length;
            if (E.MarcheCoef * c > d) 
            {
                c = d / E.MarcheCoef;
            }
        }
        E.Avancer(c);
    }

    public override Passif Clone()
    {
        return new MarcherVersNouriture(Coef);
    }
}

public class Fuir : Passif
{
    public Fuir(float coef = 1) : base(coef) { }
    public override void Debut()
    {
        E.PredateurDirection();
    }

    public override void Execute()
    {
        if (E.Predateur != null && !E.Predateur.Vivant)
        {
            E.Predateur = null;
        }

        if (E.Predateur == null || E.DistanceVisionTo(E.Predateur) > E.RayonVision)
        {
            E.PredateurDirection();
        }
        else
        {
            E.DirectionTarget = (E.Predateur.Position - E.Position).Angle + (float) Math.PI;
        }

        E.Avancer(Coef);
    }

    public override Passif Clone()
    {
        return new Fuir(Coef);
    }
}

public class Mange : Passif
{
    public override void Execute()
    {
        // bidouille pour augmenter la taille de la zone ou le mouton peut manger
        if (E.Target != null && E.Target.Vivant && E.Collision(E.Target))
        {
            E.Absorber(E.Target, 0.4f);
            //float proba = Rand.NextFloat();
            // sorry
            //if (proba < E.ProbaManger(E.Target))
            {
                //E.Target = null;
            }
        }
    }

    public override Passif Clone()
    {
        return new Mange();
    }
}

public class Replique : Passif
{
    public override void Execute()
    {
        E.Replication();
    }

    public override Passif Clone()
    {
        return new Replique();
    }
}

public class Tourner : Passif
{
    public Angle RotationPerSecond;

    public Tourner(Angle rotationPerSecond)
    {
        RotationPerSecond = rotationPerSecond;
    }

    public override void Execute()
    {
        var add = RotationPerSecond / Temps.OneSecond;
        E.DirectionTarget += add;
        E.Direction = E.DirectionTarget;
    }

    public override Passif Clone() => new Tourner(RotationPerSecond);
}