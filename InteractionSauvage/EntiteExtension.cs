using InteractionSauvage;
using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using Useful;

namespace SimulationConsole;

public static class EntiteExtension
{
    public static Entite WithRandomPosition(this Entite e, Simulation simu) 
    {
        return e.WithPosition(e.Rand.FloatUniform(0, simu.Grille.Longueur), e.Rand.FloatUniform(0, simu.Grille.Hauteur));
    }

    public static Entite WithPosition(this Entite e, float x, float y) 
    {
        e.X = x;
        e.Y = y;
        e.PositionChanger();
        return e;
    }
}
