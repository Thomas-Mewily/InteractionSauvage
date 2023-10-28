using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using Useful;
using static InteractionSauvage.MachineEtats.Etat;

namespace InteractionSauvage;

public class Entite : SimulationComposante
{
    public string Nom;

    public MachineEtat MachineEtat;
    public string EtatDeBase => DeBase.EtatDeBase == null ? MachineEtat.EtatSuggererParDefaut : DeBase.EtatDeBase;

    #region Champs Affecté par la simulation
    public Etat? MaybeEtat = null;
    public Etat Etat { get => MaybeEtat!; set => MaybeEtat = value!; }

    public string EtatNom { get => Etat.Nom; set => Etat = MachineEtat[value]; }

    public float Score { get => Actuel.Score; set => Actuel.Score = Score; }
    public int TempsDeRepos { get => Actuel.TempsDeRepos; set => Actuel.TempsDeRepos = TempsDeRepos; }

    public double X { get => Actuel.X; set => Actuel.X = value; } 
    public double Y { get => Actuel.Y; set => Actuel.Y = value; }

    public double VX { get => Actuel.VX; set => Actuel.VX = value; }
    public double VY { get => Actuel.VY; set => Actuel.VY = value; }

    public double VitesseMax { get => Actuel.VitesseMax; set => Actuel.VitesseMax = value; }
    public double Direction { get => Actuel.Direction; set => Actuel.Direction = value; }
    
    public double Energie { get => Actuel.Energie; set => Actuel.Energie = value; }
    public double Nourriture { get => Actuel.Nourriture; set => Actuel.Nourriture = value; }
    
    public int Age { get => Actuel.Age; set => Actuel.Age = value; }
    #endregion

    private Caracteristiques Actuel;
    public  Caracteristiques DeBase;

    public Entite(Simulation simu, string? nom = null, MachineEtat ? e = null)
    {
        Nom = nom ?? "";
        MachineEtat = e ?? new MachineEtat();
        DeBase = new Caracteristiques();

        Load(simu); // because of the need of Simu for rand
    }

    public void Reset() 
    {
        Actuel = DeBase;
        EtatNom = EtatDeBase;
    }

    public override void Load() 
    {
        MachineEtat.Load(this);
    }

    public void Update()
    {
        Actuel.Age++;
        TempsDeRepos = TempsDeRepos > 0 ? TempsDeRepos - 1 : 0;

        Etat.Update();
    }


    public void RngDirection()
    {
        Actuel.Direction = (Rand.NextDouble() * (2f * Math.PI));
    }

    public void Avancer(double coef = 1)
    {
        double deltaX = coef * VitesseMax * Math.Cos(Direction);
        double deltaY = coef * VitesseMax * Math.Sin(Direction);

        X += deltaX;
        Y += deltaY;

        Energie -= coef;
    }


    public void Affiche()
    {
        Console.WriteLine("================= " + Nom + " =========");
        Console.WriteLine("Categorie   = "  + Actuel.Categorie);
        Console.WriteLine("Etat        = "  + Etat);
        Console.WriteLine("TempsDeRepos= "  + TempsDeRepos);
        Console.WriteLine("Nouriture   = "  + Nourriture);
        Console.WriteLine("Energie     = "  + Energie);
        Console.WriteLine("Age         = "  + Age);
        Console.WriteLine("VitesseMax  = "  + VitesseMax);
        Console.WriteLine("Direction   = "  + Direction);
        Console.WriteLine("X, Y        = (" + X  + ", " + Y  + ")");
        Console.WriteLine("VX, VY      = (" + VX + ", " + VY + ")");
    }

    public override string ToString() => Nom.Length == 0 ? GetType().Name : Nom;
}