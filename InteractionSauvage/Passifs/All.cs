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
        E.Avancer();
    }
}

public class MarcherAleatoire : Passif
{
    public override void Debut()
    {
        E.RngDirection();
    }

    public override void Execute()
    {
        E.Avancer();
    }
}