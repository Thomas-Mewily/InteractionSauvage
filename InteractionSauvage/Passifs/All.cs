using Geometry;
using System.Numerics;
using Useful;
using static InteractionSauvage.Categories;

namespace InteractionSauvage.Passifs;

public class Attendre : Passif
{
    public override void Execute() { }
}

public class Dormir : Passif
{
    public override void Execute()
    {
        E.Energie++;
    }
}

public class Marcher : Passif
{
    public Marcher(float coef = 1) : base(coef) { }
    public override void Execute()
    {
        E.Avancer(Coef);
    }
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

        if (E.Target == null || E.DistanceTo(E.Target) > E.RayonVision)
        {
            E.NouritureDirection();
        }
        else
        {
            E.Direction = Angle.FromRadian(float.Atan2((E.Target!.Y - E.Y), (E.Target!.X - E.X))); ;
        }

        E.Avancer(Coef);
    }
}

public class Mange : Passif
{
    public override void Execute()
    {
        if (E.Target != null && E.Target.Vivant)
        {
            float proba = Rand.NextFloat();
            if (proba < E.ProbaManger(E.Target))
            {
                E.Nourriture += E.Target.Taille;
                E.Target.Meurt();
                E.Target = null;
            }
        }
    }
}