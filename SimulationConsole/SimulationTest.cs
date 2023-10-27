using InteractionSauvage;

namespace SimulationConsole;

internal class Program
{
    public Simulation DefaultSimulation() 
    {
        SimulationFactory f = new SimulationFactory();
        f.AddEntite();
        return f.Simulation;
    }

    public void Simulate(Simulation s)
    {
        s.lancerSimulation();
        
    }

    public Program()
    {
        var s = DefaultSimulation();
        Simulate(s);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Thomas");
        var p = new Program();
    }
}