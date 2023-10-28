using InteractionSauvage;

namespace SimulationConsole;

internal class Program
{
    public static Simulation DefaultSimulation() 
    {
        SimulationFactory f = new();
        f.AddEntite();
        return f.Simu;
    }

    public static void Simulate(Simulation s)
    {
        s.LancerSimulation();
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