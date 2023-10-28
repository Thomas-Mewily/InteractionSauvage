using System.Collections;

namespace InteractionSauvage;


public class Simulation
{
    public List<Entite> ToutesLesEntites;
    public List<Entite> EntitesDeBase;
    
    //public Grille Grille;
    public Temps MaxTime = 200;
    public Temps Time;
    public bool IsOver => Time > MaxTime;

    private bool IsInit = false;
    public Random Rand;

    public Simulation()
    {
        ToutesLesEntites = new List<Entite>();
        EntitesDeBase = new List<Entite>();
        Grille = new Grille(200, 200, 10);
        Rand = new Random();
        Reset();
    }

    private void InitComposants() 
    {
        foreach (var e in EntitesDeBase)
        {
            // Initialise
            e.Load(this);
        }
    }

    public void Reset()
    {
        if(IsInit == false && EntitesDeBase.Count != 0) 
        {
            InitComposants();
            IsInit = true;
        }

        Time = 0;
        ToutesLesEntites = EntitesDeBase.ToList();
        foreach (var e in ToutesLesEntites) { e.Reset(); }
    }

    public void Update()
    {
        if (IsOver) { return; }
        Time++;
        foreach (var e in ToutesLesEntites) 
        {
            e.Update();
        }
    }

    public void LancerSimulation()
    {
        Reset();
        ToutesLesEntites.ForEach(entite => { entite.Affiche(); });
        for (int i = 0; i < MaxTime; i++)
        {
            Console.WriteLine("-----------" + i + "-----------");
            Update();
            ToutesLesEntites.ForEach(entite => { entite.Affiche(); });
        }
    }
}