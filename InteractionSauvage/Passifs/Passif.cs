using InteractionSauvage.Interruptions;
using System.Numerics;
using static InteractionSauvage.Categories;

namespace InteractionSauvage.Passifs;

public abstract class Passif : EntiteComposante
{
    public string Nom;
    public float Coef;

    public Passif(float coef = 1)
    {
        Coef = coef;
        Nom = GetType().Name;
    }

    public abstract Passif Clone();
    public virtual void Debut() { }
    public virtual void Fin() { }
    public abstract void Execute();

    public override string ToString() => this.GetType().Name;
}