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