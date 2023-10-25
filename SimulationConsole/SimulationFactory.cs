using InteractionSauvage;

namespace SimulationConsole;

public class SimulationFactory
{
    public Simulation Simulation { get; }
    public static Random Rng = new Random();

    public SimulationFactory(Simulation? s = null) 
    {
        Simulation = (s == null ? new Simulation() : s!);
    }

    public static MachineEtat Mouton;

    static SimulationFactory() 
    {
        Mouton = new MachineEtat();
        Mouton.Etats.Add(new Etat(Etat.ActionEnum.MarcherAleatoire,
                                  new Transition(
                                        new Interruption(Interruption.InterruptionEnum.Fatigue), new List<EtatSuivant>() { new EtatSuivant(100, 0) }
                                        )
                                  ));

        Mouton.Etats.Add(new Etat(Etat.ActionEnum.Dormir,
                                  new Transition(
                                        new Interruption(Interruption.InterruptionEnum.Repose), new List<EtatSuivant>() { new EtatSuivant(1, 6) }
                                        )
                                  ));
    }


    public Entite GenerateEntity() 
    {
        Entite e = new Entite(Mouton);
        e.DeBase.X = Rng.Next(0, 100);
        e.DeBase.Y = Rng.Next(0, 100);
        e.DeBase.VX = 0;
        e.DeBase.VY = 0;
        e.DeBase.Age = 10;
        e.DeBase.Energie = 1;
        e.DeBase.Direction = (float)(Rng.NextDouble() % (2f * Math.PI));
        return e;
    }

    public void AddEntite() 
    {
        Simulation.EntitesDeBase.Add(GenerateEntity());
    }
}