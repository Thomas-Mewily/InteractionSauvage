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
    private static Categories CarnivoreCat { get; set; }

    public Temps Second(float secondMax) => Second(0, secondMax);
    public Temps Second(float secondMin, float secondMax) => Temps.Second(Rand.FloatUniform(secondMin, secondMax));

    public MachineEtat MoutonMEMathis() 
    {
        var MoutonME = new MachineEtat();
        // le 1er état ajouté est l'état suggeré par défaut/l'état d'entrée

        //MoutonME.Add("marcher",  new MarcherAleatoire(0.5f), new Transition("cours ma gazelle", new Apres(Second(0, 0.1f)), "courrir"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("j'ai faim", new Faim(), "marcher vers nouriture"));
        //MoutonME.Add("courrir",  new MarcherAleatoire(1f),   new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("j'ai faim", new Faim(), "courrir vers nouriture"));
        MoutonME.Add("dormir", new Dormir(), new Transition("c'est reparti pour un tour", new Repose(Rand.FloatUniform(0.5f,0.9f)), "marcher vers nouriture"));
        
        //MoutonME.Add("marcher vers nouriture", new MarcherVersNouriture(0.5f), new Transition("cours ma gazelle", new Apres(Second(0.1f, 0.2f)), "courrir vers nouriture"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"), new Transition("plus faim", new Repu(5), "marcher"));
        MoutonME.Add("marcher vers nouriture", new MarcherVersNouriture(0.5f), new Transition("cours ma gazelle", new Apres(Second(0.1f, 0.2f)), "courrir vers nouriture"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"));
        MoutonME.Add("courrir vers nouriture", new MarcherVersNouriture(1f), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"));

        //new Apres(Temps.Second(0.5f))
        MoutonME.Add("manger", new Mange(), new Transition("marchons", new Instantanee(), "marcher vers nouriture"));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher")));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new List<EtatSuivant> { new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher") }));
        
        return MoutonME;
    }

    public MachineEtat WolfME()
    {
        var WolfME = new MachineEtat();
        // le 1er état ajouté est l'état suggeré par défaut/l'état d'entrée

        //MoutonME.Add("marcher",  new MarcherAleatoire(0.5f), new Transition("cours ma gazelle", new Apres(Second(0, 0.1f)), "courrir"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("j'ai faim", new Faim(), "marcher vers nouriture"));
        //MoutonME.Add("courrir",  new MarcherAleatoire(1f),   new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("j'ai faim", new Faim(), "courrir vers nouriture"));
        WolfME.Add("dormir", new Dormir(), new Transition("c'est reparti pour un tour", new Repose(Rand.FloatUniform(0.5f, 0.9f)), "marcher vers nouriture"));

        //MoutonME.Add("marcher vers nouriture", new MarcherVersNouriture(0.5f), new Transition("cours ma gazelle", new Apres(Second(0.1f, 0.2f)), "courrir vers nouriture"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"), new Transition("plus faim", new Repu(5), "marcher"));
        WolfME.Add("marcher vers nouriture", new MarcherVersNouriture(0.5f), new Transition("cours ma gazelle", new Apres(Second(0.1f, 0.2f)), "courrir vers nouriture"), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"));
        WolfME.Add("courrir vers nouriture", new MarcherVersNouriture(1f), new Transition("je suis epuise", new Fatigue(), "dormir"), new Transition("miam", new NourritureAtteignable(), "manger"));

        //new Apres(Temps.Second(0.5f))
        WolfME.Add("manger", new Mange(), new Transition("marchons", new Instantanee(), "marcher vers nouriture"));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher")));
        //MoutonME.Add("courrir", new Marcher(1), new Transition("je suis epuise", new Apres(2), new List<EtatSuivant> { new EtatSuivant(4, "attendre"), new EtatSuivant(8, "marcher") }));

        return WolfME;
    }

    public MachineEtat HerbeME()
    {
        var HerbeME = new MachineEtat();

        //HerbeME.Add("rien", new Attendre(), new Transition("", new Jamais(), "rien"));
        //HerbeME.Add("Attend", new Attendre(), new Transition("Replique", new Apres(Second(1,7)), "Replique"));
        HerbeME.Add("Attend", new Attendre(), new Transition("Replique", new Apres(Second(5,10)), "Replique"));
        HerbeME.Add("Replique", new Replique(), new Transition("Fini", new Instantanee(), "Attend"));

        return HerbeME;
    }

    public Categories PlantesC() => PlantesCat;
    public Categories HerbivoresC() => HerbivoresCat;

    static SimulationFactory() 
    {
        PlantesCat = new Categories(Categories.CategorieEnum.Plante);

        HerbivoresCat = new Categories(Categories.CategorieEnum.Herbivore);
        HerbivoresCat.CategoriesNouritures.Add(Categories.CategorieEnum.Plante);

        CarnivoreCat = new Categories(Categories.CategorieEnum.Carnivore);
        CarnivoreCat.CategoriesNouritures.Add(Categories.CategorieEnum.Herbivore);

        //HerbivoresCat.CategoriesNouritures.Add(Categories.CategorieEnum.Herbivore);
    }

    public Entite NewEntite(string nom, MachineEtat? machineEtat) => new(Simu, nom + "#" + Simu.EntitesDeBase.Count, machineEtat);

    public virtual Entite GenerateMouton() 
    {
        Entite e = NewEntite("Mouton", MoutonMEMathis()).WithRandomPosition(Simu);

        e.VitesseMax = 0.1f;
        e.Taille = 1;
        e.Age = 10;
        e.Rayon = 1;
        e.Energie = Rand.FloatUniform(0, 1);
        //e.Nourriture = 0;
        e.Categorie = HerbivoresC();
        e.Direction = Angle.FromRadian(Rand.NextFloat(2f * MathF.PI));
        e.RotationParSeconde = Angle.FromDegree(Rand.FloatUniform(45, 360 * 2));
        //e.Direction = Angle.FromDegree(225f);

        float repartition = Rand.FloatUniform(0, 1);
        e.ChampsVision = Angle.FromDegree(5 + repartition * 200);
        e.RayonVision = 2 + (1 - repartition)* (1 - repartition) * 12;

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

    public virtual Entite GenerateWolf() 
    {
        Entite e = NewEntite("Loup", WolfME()).WithRandomPosition(Simu);

        e.VitesseMax = 0.1f;
        e.Taille = 1;
        e.Age = 10;
        e.Rayon = 1;
        e.Energie = Rand.FloatUniform(0, 1);
        //e.Nourriture = 0;
        e.Categorie = CarnivoreCat;
        e.Direction = Angle.FromRadian(Rand.NextFloat(2f * MathF.PI));
        e.RotationParSeconde = Angle.FromDegree(Rand.FloatUniform(45, 360 * 2));
        //e.Direction = Angle.FromDegree(225f);

        float repartition = Rand.FloatUniform(0, 1);
        e.ChampsVision = Angle.FromDegree(5 + repartition * 200);
        e.RayonVision = 2 + (1 - repartition) * (1 - repartition) * 12;

        //e.DeBase.EtatDeBase = "marcher";
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

    public void AddSniperSheep() 
    {
        var m = GenerateMouton();
        m.VitesseMax *= 17;
        m.RayonVision = 64;
        m.Rayon = 0.01f;
        m.ChampsVision = Angle.FromDegree(90);
        m.CoefAbandonEnergiePerduPendantLesDeplacements = 0.000001f;
        m.Energie = 0;
        m.RotationParSeconde = Angle.FromDegree(360 * 4);
        Simu.Add(m);
    }
}