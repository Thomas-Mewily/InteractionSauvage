using Geometry;
using InteractionSauvage;
using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using SimulationConsole;
using Useful;

namespace InteractionSauvage;

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
        MoutonME.Add("marcher",  new MarcherAleatoire(0.5f), new Transition("cours ma gazelle", new Apres(Rand.IntUniform(10, 20)), "courrir"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("j'ai faim", new Faim(), "marcher vers nouriture"));
        MoutonME.Add("courrir",  new MarcherAleatoire(1f),   new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("j'ai faim", new Faim(), "courrir vers nouriture"));
        MoutonME.Add("dormir", new Dormir(), new Transition("c'est reparti pour un tour", new Repose(Rand.IntUniform(30, 50)), "marcher vers nouriture"));

        MoutonME.Add("marcher vers nouriture", new MarcherVersNouriture(0.5f), new Transition("cours ma gazelle", new Apres(Rand.IntUniform(10, 20)), "courrir vers nouriture"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"), new Transition("plus faim", new Repu(5), "marcher"));
        MoutonME.Add("courrir vers nouriture", new MarcherVersNouriture(1f), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"));

        MoutonME.Add("manger", new Mange(), new Transition("marchons", new Apres(1), "marcher vers nouriture"));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher")));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new List<EtatSuivant> { new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher") }));

        return MoutonME;
    }

    public MachineEtat HerbeME()
    {
        var HerbeME = new MachineEtat();

        HerbeME.Add("rien", new Attendre(), new Transition("transitionne pas", new Jamais(), "rien"));

        return HerbeME;
    }

    public Categories PlantesC() => PlantesCat;
    public Categories HerbivoresC() => HerbivoresCat;

    static SimulationFactory() 
    {
        PlantesCat = new Categories(Categories.CategorieEnum.Plante);

        HerbivoresCat = new Categories(Categories.CategorieEnum.Herbivore);
        HerbivoresCat.CategoriesNouritures.Add(Categories.CategorieEnum.Plante);
        //HerbivoresCat.CategoriesNouritures.Add(Categories.CategorieEnum.Herbivore);
    }

    public Entite NewEntite(string nom, MachineEtat? machineEtat) => new(Simu, nom + "#" + Simu.EntitesDeBase.Count, machineEtat);

    public virtual Entite GenerateMouton() 
    {
        Entite e = NewEntite("Mouton", MoutonME()).WithRandomPosition(Simu);

        e.VitesseMax = 0.1f;
        e.Taille = 1;
        e.Age = 10;
        e.Rayon = 1;
        e.Energie = 1000;
        e.Nourriture = 0;
        e.Categorie = HerbivoresC();
        e.Direction = Angle.FromRadian(Rand.NextFloat(2f * MathF.PI));
        //e.Direction = Angle.FromDegree(225f);
        e.ChampsVision = Angle.FromDegree(Rand.FloatUniform(45, 180));
        e.RayonVision = 10f;

        //e.DeBase.EtatDeBase = "marcher";
        return e;
    }

    public virtual Entite GenerateHerbe()
    {
        Entite e = NewEntite("Herbe", HerbeME()).WithRandomPosition(Simu);
        e.VitesseMax = 0;
        e.Age = 1;
        e.Taille = 0.5f;
        e.Energie = 1;
        e.Categorie = PlantesC();
        e.Direction = 0;
        e.ChampsVision = Angle.FromDegree(0);
        e.RayonVision = 0f;

        return e;
    }

    public void AddEntite()
    {
        Simu.Add(GenerateMouton());
        for(int i = 0; i < 50; i++) 
        {
            Simu.Add(GenerateHerbe());
        }
        //Simu.EntitesDeBase.Add(GenerateHerbe());
    }
}