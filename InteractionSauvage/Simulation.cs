using System.Collections;

namespace InteractionSauvage;

public class Simulation //: IEnumerable<Simulation>, IEnumerator<Simulation>
{
    public List<Entite> ToutesLesEntites;
    public List<Entite> EntitesDeBase;
    public int MaxTime = 200;
    public int Time;

    public Simulation()
    {
        EntitesDeBase = new List<Entite>();
        Reset();
        Console.WriteLine("Coucou2");
    }

    public bool IsOver => Time > MaxTime;
    public void Reset() 
    {
        Time = 0;
        ToutesLesEntites = EntitesDeBase.ToList();
        foreach(var e in ToutesLesEntites) { e.Reset(); }
    }

    public void OneStep() 
    {
        if (IsOver) { return; }
        Time++;
    }
}