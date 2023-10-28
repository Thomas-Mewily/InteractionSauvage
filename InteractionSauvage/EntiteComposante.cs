using System.Collections;

namespace InteractionSauvage;


public abstract class EntiteComposante : SimulationComposante
{
    public Entite? MaybeE;
    public Entite E => MaybeE!;

    public void Load(Entite e)
    {
        MaybeE = e;
        base.Load(e.Simu);
    }
}