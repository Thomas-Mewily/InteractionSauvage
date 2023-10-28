using System.Numerics;
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
    public double coef;

    public Marcher(double coef = 1)
    {
        this.coef = coef;
    }

    public override void Execute()
    {
        E.Avancer(coef);
    }
}

public class MarcherAleatoire : Passif
{
    public double coef;
    public MarcherAleatoire(double coef = 1)
    {
        this.coef = coef;
    }
    public override void Debut()
    {
        E.RngDirection();
    }

    public override void Execute()
    {
        if (Rand.NextDouble() < 0.1) E.RngDirection();

        E.Avancer(coef);
    }
}