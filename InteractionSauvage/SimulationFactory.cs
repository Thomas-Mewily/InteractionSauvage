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
        MoutonME.Add("marcher",  new MarcherVersNouriture(0.5f), new Transition("cours ma gazelle", new Apres(Rand.IntUniform(10, 40)), "courrir"));
        MoutonME.Add("courrir",  new MarcherVersNouriture(1f),   new Transition("je suis epuise",   new Apres(Rand.IntUniform(30, 80)), "attendre"));
        MoutonME.Add("attendre", new Attendre(),   new Transition("c'est reparti pour un tour", new Apres(Rand.IntUniform(40, 90)), "marcher"));

        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher")));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new List<EtatSuivant> { new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher") }));

        return MoutonME;
    }

    public MachineEtat HerbeME()
    {
        var HerbeME = new MachineEtat();

        HerbeME.Add("rien", new Attendre(), new Transition("trasitionne pas", new Jamais(), "rien"));

        return HerbeME;
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
        e.Rayon = 10;
        e.Energie = 1;
        e.Categorie = HerbivoresC();
        e.Direction = Angle.FromRadian(Rand.NextFloat(2f * MathF.PI));
        //e.Direction = Angle.FromDegree(225f);
        e.ChampsVision = Angle.FromDegree(180f);
        e.DistanceVision = 100f;
        //e.DeBase.EtatDeBase = "marcher";
        return e;
    }

    public Entite GenerateHerbe()
    {

        Entite e = NewEntite("Herbe", HerbeME()).WithRandomPosition(Simu);
        e.VitesseMax = 0;
        e.Age = 1;
        e.Taille = 5;
        e.Energie = 1;
        e.Categorie = PlantesC();
        e.Direction = 0;
        e.ChampsVision = Angle.FromDegree(0);
        e.DistanceVision = 0f;

        return e;
    }

    public void AddEntite() 
    {
        Simu.Add(GenerateMouton());
        Simu.Add(GenerateHerbe());
        //Simu.EntitesDeBase.Add(GenerateHerbe());
    }
}