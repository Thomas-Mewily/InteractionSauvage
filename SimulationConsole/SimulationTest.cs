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

    }

    public Program()
    {
        var s = DefaultSimulation();
        Simulate(s);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Mathis");
        var p = new Program();
    }
}