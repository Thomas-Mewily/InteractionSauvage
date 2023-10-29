using Geometry;
using InteractionSauvage;
using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using Useful;

namespace SimulationConsole;

public class SimulationFactory : SimulationComposante
{
    public SimulationFactory(Simulation? s = null) 
    {
        Load(s == null ? new Simulation() : s!);
    }

    private static Categories PlantesCat { get; set; }
    private static Categories HerbivoresCat { get; set; }

    public MachineEtat MoutonME() 
    {
        var MoutonME = new MachineEtat();
        // le 1er état ajouté est l'état suggeré par défaut/l'état d'entrée
        MoutonME.Add("marcher",  new MarcherAleatoire(0.5f), new Transition("cours ma gazelle", new Apres(20), "courrir"));
        MoutonME.Add("courrir",  new MarcherAleatoire(1f),   new Transition("je suis epuise",   new Apres(40), "attendre"));
        MoutonME.Add("attendre", new Attendre(),   new Transition("c'est reparti pour un tour", new Apres(50), "marcher"));

        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher")));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new List<EtatSuivant> { new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher") }));

        return MoutonME;
    }

    public Categories PlantesC() => PlantesCat;
    public Categories HerbivoresC() => HerbivoresCat;

    static SimulationFactory() 
    {
        PlantesCat = new Categories(Categories.CategorieEnum.Plante);

        HerbivoresCat = new Categories(Categories.CategorieEnum.Herbivore);
        HerbivoresCat.CategoriesNouritures.Add(Categories.CategorieEnum.Plante);
    }

    public Entite NewEntite(string nom, MachineEtat? machineEtat) => new(Simu, nom + "#" + Simu.EntitesDeBase.Count, machineEtat);

    public Entite GenerateMouton() 
    {
        Entite e = NewEntite("Mouton", MoutonME()).WithRandomPosition(Simu);
        e.VitesseMax = 2;
        e.Age = 10;
        e.Taille = 10;
        e.Energie = 1;
        e.Categorie = HerbivoresC();
        e.Direction = Angle.FromRadian(Rand.NextFloat(2f * MathF.PI));

        //e.DeBase.EtatDeBase = "marcher";
        return e;
    }

    public void GenerateHerbe()
    {
        /*
        Entite e = newEntite(HerbeME(), "Herbe").WithRandomPosition();
        e.DeBase.VitesseMax = 0;
        e.DeBase.Age = 1;
        e.DeBase.Energie = 1;
        e.DeBase.Direction = 0;
        e.DeBase.Categorie = PlantesC();
        return e;*/
    }

    public void AddEntite() 
    {
        Simu.Add(GenerateMouton());
        //Simu.EntitesDeBase.Add(GenerateHerbe());
    }
}