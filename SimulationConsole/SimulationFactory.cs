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

    public static MachineEtat Mouton = null;

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