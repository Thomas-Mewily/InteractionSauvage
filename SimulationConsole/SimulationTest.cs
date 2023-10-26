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
        s.Reset();
        s.ToutesLesEntites.ForEach(entite => {entite.Affiche();});
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine("-----------" +  i + "-----------");
            s.OneStep();
            s.ToutesLesEntites.ForEach(entite => { entite.Affiche(); });
        }
        
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