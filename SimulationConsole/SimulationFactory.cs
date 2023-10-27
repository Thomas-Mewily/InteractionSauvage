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

    public static MachineEtat Mouton; 
    public static MachineEtat Herbe;

    public static Categories Plantes;
    public static Categories Herbivores;

    static SimulationFactory() 
    {
        Mouton = new MachineEtat();
        Mouton.Etats.Add(new Etat(Etat.ActionEnum.MarcherAleatoire,
                                  new Transition(
                                        new Interruption(Interruption.InterruptionEnum.Fatigue), new List<EtatSuivant>() { new EtatSuivant(100, 1) }
                                        )
                                  ));

        Mouton.Etats.Add(new Etat(Etat.ActionEnum.Dormir,
                                  new Transition(
                                        new Interruption(Interruption.InterruptionEnum.Repose), new List<EtatSuivant>() { new EtatSuivant(100, 0) }
                                        )
                                  ));

        Herbe = new MachineEtat();
        Herbe.Etats.Add(new Etat(Etat.ActionEnum.Attendre, new Transition(
                                        new Interruption(Interruption.InterruptionEnum.None), new List<EtatSuivant>() { new EtatSuivant(0, 0) }
                                        )
                                  )); ;

        Plantes = new Categories(Categories.CategorieEnum.Plante);

        Herbivores = new Categories(Categories.CategorieEnum.Herbivore);
        Herbivores.CategoriesNouritures.Add(Categories.CategorieEnum.Plante);
    }


    public Entite GenerateMouton() 
    {
        Entite e = new Entite(Mouton, "Mouton");
        e.DeBase.X = Rng.Next(0, 100);
        e.DeBase.Y = Rng.Next(0, 100);
        e.DeBase.VX = 0;
        e.DeBase.VY = 0;
        e.DeBase.VitesseMax = 2;
        e.DeBase.Age = 10;
        e.DeBase.Energie = 1;
        e.DeBase.Categorie = Herbivores;
        e.DeBase.Direction = (float)(Rng.NextDouble() % (2f * Math.PI));
        return e;
    }

    public Entite GenerateHerbe()
    {
        Entite e = new Entite(Herbe, "Herbe");
        e.DeBase.X = Rng.Next(0, 100);
        e.DeBase.Y = Rng.Next(0, 100);
        e.DeBase.VX = 0;
        e.DeBase.VY = 0;
        e.DeBase.VitesseMax = 0;
        e.DeBase.Age = 1;
        e.DeBase.Energie = 1;
        e.DeBase.Direction = 0;
        e.DeBase.Categorie = Plantes;
        return e;
    }

    public void AddEntite() 
    {
        Simulation.EntitesDeBase.Add(GenerateMouton());
        Simulation.EntitesDeBase.Add(GenerateHerbe());
    }
}