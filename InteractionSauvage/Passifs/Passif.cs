using System.Numerics;
using static InteractionSauvage.Categories;

namespace InteractionSauvage.Passifs;

public abstract class Passif : EntiteComposante
{
    public string Nom;

    static Passif() { }

    public Passif()
    {
        Nom = GetType().Name;
    }

    public virtual void Debut() { }
    public virtual void Fin() { }
    public abstract void Execute();

    public override string ToString() => this.GetType().Name;
}