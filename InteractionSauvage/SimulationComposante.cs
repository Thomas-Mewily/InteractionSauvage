using System.Collections;

namespace InteractionSauvage;

public abstract class SimulationComposante
{
    public Simulation? MaybeSimu;
    public Simulation Simu => MaybeSimu!;

    public Temps Time { get => Simu.Time; set => Simu.Time = value; }
    public Random Rand => Simu.Rand;

    public void Load(Simulation s)
    {
        MaybeSimu = s;
        Load();
    }
    public virtual void Load() { }
}