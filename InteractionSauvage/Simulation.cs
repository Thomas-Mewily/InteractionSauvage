using System.Collections;

namespace InteractionSauvage;

public class Simulation //: IEnumerable<Simulation>, IEnumerator<Simulation>
{
    public List<Entite> ToutesLesEntites;
    public List<Entite> EntitesDeBase;

    public Grille Grille;
    //public Grille Grille;
    public int MaxTime = 200;
    public int Time;

    public Random Rand;

    public Simulation()
    {
        ToutesLesEntites = new List<Entite>();
        EntitesDeBase = new List<Entite>();
        Grille = new Grille(200, 200, 10);
        Rand = new Random();
        Reset();
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
        ToutesLesEntites.ForEach(e => e.OneStep());
    }

    public void lancerSimulation()
    {
        Reset();
        ToutesLesEntites.ForEach(entite => { entite.Affiche(); });
        for (int i = 0; i < MaxTime; i++)
        {
            Console.WriteLine("-----------" + i + "-----------");
            OneStep();
            ToutesLesEntites.ForEach(entite => { entite.Affiche(); });
        }
    }
}