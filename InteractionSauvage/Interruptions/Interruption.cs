using InteractionSauvage.MachineEtats;
using static InteractionSauvage.MachineEtats.Etat;

namespace InteractionSauvage.Interruptions;

public abstract class Interruption : EntiteComposante
{
    public virtual string Nom => this.GetType().Name;
    public abstract bool Interrupt { get; }
    
    public Etat? MaybeAssocieA;
    public Etat AssocieA => MaybeAssocieA!;

    public virtual void Debut() { }
    public virtual void Fin() { }
}
