using InteractionSauvage;

namespace SimulationConsole;


public static class EntiteExtension
{
    public static Entite WithRandomPosition(this Entite e) 
    {
        return e.WithPosition(SimulationFactory.Rng.Next(0, 100), SimulationFactory.Rng.Next(0, 100));
    }


    public static Entite WithPosition(this Entite e, float x, float y) 
    {
        e.DeBase.X = x;
        e.DeBase.Y = y;
        return e;
    }
}

public class SimulationFactory
{
    public Simulation Simulation { get; }
    public static Random Rng = new Random();

    public SimulationFactory(Simulation? s = null) 
    {
        Simulation = (s == null ? new Simulation() : s!);
    }

    public static MachineEtat MoutonME; 
    public static MachineEtat HerbeME;

    public static Categories PlantesC;
    public static Categories HerbivoresC;

    static SimulationFactory() 
    {
        MoutonME = new MachineEtat();
        MoutonME.Etats.Add(new Etat(Etat.ActionEnum.MarcherAleatoire,
                                  new Transition(
                                        new Interruption(Interruption.InterruptionEnum.Fatigue), new List<EtatSuivant>() { new EtatSuivant(100, 1) }
                                        )
                                  ));

        MoutonME.Etats.Add(new Etat(Etat.ActionEnum.Dormir,
                                  new Transition(
                                        new Interruption(Interruption.InterruptionEnum.Repose), new List<EtatSuivant>() { new EtatSuivant(100, 0) }
                                        )
                                  ));

        HerbeME = new MachineEtat();
        HerbeME.Etats.Add(new Etat(Etat.ActionEnum.Attendre, new Transition(
                                        new Interruption(Interruption.InterruptionEnum.None), new List<EtatSuivant>() { new EtatSuivant(0, 0) }
                                        )
                                  )); ;

        PlantesC = new Categories(Categories.CategorieEnum.Plante);

        HerbivoresC = new Categories(Categories.CategorieEnum.Herbivore);
        HerbivoresC.CategoriesNouritures.Add(Categories.CategorieEnum.Plante);
    }



    public Entite newEntite(MachineEtat? machineEtat, string nom) => new Entite(Simulation, machineEtat, nom);

    public Entite GenerateMouton() 
    {
        Entite e = newEntite(MoutonME, "Mouton").WithRandomPosition();
        e.DeBase.VitesseMax = 2;
        e.DeBase.Age = 10;
        e.DeBase.Energie = 1;
        e.DeBase.Categorie = HerbivoresC;
        e.DeBase.Direction = (float)(Rng.NextDouble() % (2f * Math.PI));
        return e;
    }

    public Entite GenerateHerbe()
    {
        Entite e = newEntite(HerbeME, "Herbe").WithRandomPosition();
        e.DeBase.VitesseMax = 0;
        e.DeBase.Age = 1;
        e.DeBase.Energie = 1;
        e.DeBase.Direction = 0;
        e.DeBase.Categorie = PlantesC;
        return e;
    }

    public void AddEntite() 
    {
        Simulation.EntitesDeBase.Add(GenerateMouton());
        Simulation.EntitesDeBase.Add(GenerateHerbe());
    }
}