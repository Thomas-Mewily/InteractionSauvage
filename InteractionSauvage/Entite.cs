using static InteractionSauvage.Etat;

namespace InteractionSauvage;

public class MachineEtat
{
    public List<Etat> Etats;
    public Etat this[int i] => Etats[i];

    public MachineEtat(List<Etat> machineEtat = null)
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

    public Caracteristiques Actuel;
    public Caracteristiques DeBase;

    private Random rand; 

    public Entite(MachineEtat e)
    {
        MachineEtat = e;
        DeBase = new Caracteristiques();
        rand = new Random();

        Actions[(int)Etat.ActionEnum.Dormir]           = Dormir;

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
            TempsDeRepos = rand.Next(10, 20);
            tmpIndex = Etat.transitionTo(new Interruption(Interruption.InterruptionEnum.Fatigue));
            if (tmpIndex != -1) Actuel.EtatIndex = tmpIndex;
        }

        if (TempsDeRepos == 0)
        {
            tmpIndex = Etat.transitionTo(new Interruption(Interruption.InterruptionEnum.Repose));
            if (tmpIndex != -1) Actuel.EtatIndex = tmpIndex;
        }

    }

    public void Dormir()
    {
        Actuel.Energie++;
    }

    private void GenDirection()
    {
        Actuel.Direction = (float)(rand.NextDouble() * (2f * Math.PI));
    }

    private void Avancer()
    {
        double deltaX = Actuel.VitesseMax * Math.Cos(Actuel.Direction);
        double deltaY = Actuel.VitesseMax * Math.Sin(Actuel.Direction);

        Actuel.X += deltaX;
        Actuel.Y += deltaY;
    }

    public void MarcherAleatoire()
    {
        if (Actuel.Direction == 0 || rand.NextDouble() < 0.1)
        {
            GenDirection();
        }

        Avancer();

        if (Actuel.Nourriture > 0) Actuel.Nourriture--;
        if (Actuel.Energie    > 0) Actuel.Energie   --;
    }

    public void Affiche()
    {
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