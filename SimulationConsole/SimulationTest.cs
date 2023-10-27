using InteractionSauvage;

namespace SimulationConsole;

internal class Program
{
    public static Simulation DefaultSimulation() 
    {
        SimulationFactory f = new();
        f.AddEntite();
        return f.Simulation;
    }

    public static void Simulate(Simulation s)
    {
        s.lancerSimulation();
    }

    public Program()
    {

    }

    public void Run() 
    {
        var s = DefaultSimulation();
        Simulate(s);
    }

    static void Main()
    {
        var p = new Program();
        p.Run();
    }
}