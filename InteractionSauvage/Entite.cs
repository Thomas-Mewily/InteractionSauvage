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

    public Caracteristiques Actuel;
    public Caracteristiques DeBase;

    private Random rand; 

    public Entite(MachineEtat e)
    {
        MachineEtat = e;
        DeBase = new Caracteristiques();
        rand = new Random();

        Actions[(int) Etat.ActionEnum.Dormir] = Dormir;

        Actions[(int)Etat.ActionEnum.MarcherAleatoire] = MarcherAleatoire;

    }

    public void Reset() 
    {
        Actuel = DeBase;
    }

    public void OneStep()
    {
        Actuel.Age++;
        Actions[(int)Etat.Action]();
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
        Console.WriteLine("Nouriture   = "  + Actuel.Nourriture);
        Console.WriteLine("Energie     = "  + Actuel.Energie);
        Console.WriteLine("Age         = "  + Actuel.Age);
        Console.WriteLine("VitesseMax  = "  + Actuel.VitesseMax);
        Console.WriteLine("Direction   = "  + Actuel.Direction);
        Console.WriteLine("X, Y        = (" + Actuel.X + ", " + Actuel.Y + ")");
    }
}