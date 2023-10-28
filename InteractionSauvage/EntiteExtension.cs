using InteractionSauvage;
using InteractionSauvage.Interruptions;
using InteractionSauvage.MachinAeEtat;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;

namespace SimulationConsole;

public static class EntiteExtension
{
    public static Entite WithRandomPosition(this Entite e) 
    {
        return e.WithPosition(e.Rand.Next(0, 100), e.Rand.Next(0, 100));
    }

    public static Entite WithPosition(this Entite e, float x, float y) 
    {
        e.DeBase.X = x;
        e.DeBase.Y = y;
        return e;
    }
}
