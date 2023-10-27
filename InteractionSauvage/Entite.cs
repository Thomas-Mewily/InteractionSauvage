using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using static InteractionSauvage.Etat;

namespace InteractionSauvage;

public class MachineEtat
{
    public List<Etat> Etats;
    public Etat this[int i] => Etats[i];

    public MachineEtat(List<Etat>? machineEtat = null)
    {
        Etats = (machineEtat == null ? new List<Etat>() : machineEtat);
    }
}

public class Entite
{
    MachineEtat MachineEtat;
    public Etat Etat => MachineEtat[Actuel.EtatIndex];
    public Action[] Actions = new Action[10];

    public float Score = 0;
    public int TempsDeRepos = 0;
    public string Nom;

    public double X { get => Actuel.X; set => Actuel.X = value; } 
    public double Y { get => Actuel.Y; set => Actuel.Y = value; }
    public double VX { get => Actuel.VX; set => Actuel.VX = value; }
    public double VY { get => Actuel.VY; set => Actuel.VY = value; }
    public double Direction { get => Actuel.Direction; set => Actuel.Direction = value; }
    public double Energie { get => Actuel.Energie; set => Actuel.Energie = value; }
    public double Nourriture { get => Actuel.Nourriture; set => Actuel.Nourriture = value; }
    public int Age { get => Actuel.Age; set => Actuel.Age = value; }

    private Caracteristiques Actuel;
    public  Caracteristiques DeBase;

    private Simulation Inside;
    private Random Rand => Inside.Rand;

    public Entite(Simulation simu, MachineEtat? e, string? nom = null)
    {
        Inside = simu;
        Nom = nom == null ? "Sans Nom" : nom;
        MachineEtat = e == null ? new MachineEtat() : e;
        DeBase = new Caracteristiques();

        simu.Grille.AjouterEntiteDansGrille(this);

        Actions[(int)Etat.ActionEnum.Dormir] = Dormir;
        Actions[(int)Etat.ActionEnum.Attendre] = Attendre;
        Actions[(int)Etat.ActionEnum.MarcherAleatoire] = MarcherAleatoire;
    }

    public void Reset() 
    {
        Actuel = DeBase;
    }

    public void OneStep()
    {
        Actuel.Age++;
        FaireTransition();

        Actions[(int)Etat.Action]();

        TempsDeRepos = TempsDeRepos > 0 ? TempsDeRepos - 1 : 0;
    }

    public void FaireTransition()
    {
        int tmpIndex = -1;
        if (Actuel.Energie == 0)
        {
            TempsDeRepos = Rand.Next(10, 20);
            tmpIndex = Etat.transitionTo(new Interruption(Interruption.InterruptionEnum.Fatigue));
            if (tmpIndex != -1) Actuel.EtatIndex = tmpIndex;
        }

        if (TempsDeRepos == 0)
        {
            tmpIndex = Etat.transitionTo(new Interruption(Interruption.InterruptionEnum.Repose));
            if (tmpIndex != -1) Actuel.EtatIndex = tmpIndex;
        }

    }

    public void Attendre(){}
    public void Dormir()
    {
        Actuel.Energie++;
    }

    private void GenDirection()
    {
        Actuel.Direction = (float)(Rand.NextDouble() * (2f * Math.PI));
    }

    private void Avancer()
    {
        double deltaX = Actuel.VitesseMax * Math.Cos(Direction);
        double deltaY = Actuel.VitesseMax * Math.Sin(Direction);

        if (X + deltaX > 0 && X + deltaX < Inside.Grille.Hauteur && Y + deltaY > 0 && Y + deltaY < Inside.Grille.Longueur)
        {
            X += deltaX;
            Y += deltaY;

            Inside.Grille.RetirerEntiteDeGrille(this);
            Inside.Grille.AjouterEntiteDansGrille(this);
        }
    }

    public void MarcherAleatoire()
    {
        if (Actuel.Direction == 0 || Rand.NextDouble() < 0.1)
        {
            GenDirection();
        }

        Avancer();

        if (Actuel.Nourriture > 0) Actuel.Nourriture--;
        if (Actuel.Energie    > 0) Actuel.Energie   --;
    }

    public void MarcherNouriture()
    {


        Avancer();

        if (Actuel.Nourriture > 0) Actuel.Nourriture--;
        if (Actuel.Energie > 0) Actuel.Energie--;
    }

    public void Affiche()
    {
        Console.WriteLine("-------------- " + Nom + " --------------");
        Console.WriteLine("Categorie   = "  + Actuel.Categorie);
        Console.WriteLine("Etat        = "  + Etat);
        Console.WriteLine("EtatIndex   = "  + Actuel.EtatIndex);
        Console.WriteLine("TempsDeRepos= "  + TempsDeRepos);
        Console.WriteLine("Nouriture   = "  + Actuel.Nourriture);
        Console.WriteLine("Energie     = "  + Actuel.Energie);
        Console.WriteLine("Age         = "  + Actuel.Age);
        Console.WriteLine("VitesseMax  = "  + Actuel.VitesseMax);
        Console.WriteLine("Direction   = "  + Actuel.Direction);
        Console.WriteLine("X, Y        = (" + Actuel.X + ", " + Actuel.Y + ")");
    }
}