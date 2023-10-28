using InteractionSauvage;
using InteractionSauvage.Interruptions;
using InteractionSauvage.MachinAeEtat;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;

namespace SimulationConsole;

public static class EntiteExtension
{
    public static Entite WithRandomPosition(this Entite e, Simulation simu) 
    {
        return e.WithPosition(e.Rand.Next(0, simu.Grille.Longueur), e.Rand.Next(0, simu.Grille.Hauteur));
    }

    public static Entite WithPosition(this Entite e, float x, float y) 
    {
        e.DeBase.X = x;
        e.DeBase.Y = y;
        return e;
    }
}
