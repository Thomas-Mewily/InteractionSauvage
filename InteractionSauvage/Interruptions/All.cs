using InteractionSauvage.MachineEtats;
using Useful;
using static InteractionSauvage.MachineEtats.Etat;

namespace InteractionSauvage.Interruptions;

public class Jamais : Interruption 
{
    public override bool Interrupt => false;
}

public class Instantanee : Interruption
{
    public override bool Interrupt => true;
}

public class Apres : Interruption
{
    public Temps Temps;
    public Apres(Temps  temps) 
    {
        Temps = temps;
    }
    public override bool Interrupt => Simu.Time - AssocieA.TempsDebut > Temps;
}

public class ApresAleatoire : Interruption
{
    public Temps TempsMax;
    public Temps TempsMin;

    public Temps TempsRng;
    public List<Temps> TempsRngCp = new List<Temps>();

    public ApresAleatoire(Temps min, Temps max)
    {
        TempsMin = min;
        TempsMax = max;
        TempsRng = TempsMax;
    }

    public override void Debut()
    {
        TempsRng = Rand.IntUniform(TempsMin.T, TempsMax.T);
    }

    public override void CheckPointAdd()
    {
        TempsRngCp.Add(TempsRng);
        base.CheckPointAdd();
    }

    public override void CheckPointRollBack()
    {
        TempsRng = TempsRngCp.Peek();
        base.CheckPointRollBack();
    }

    public override void CheckPointRemove()
    {
        TempsRngCp.Pop();
        base.CheckPointRemove();
    }

    public override bool Interrupt => Simu.Time - AssocieA.TempsDebut > TempsRng;
}

public class NourritureAtteignable : Interruption
{
    public override bool Interrupt => E.Target != null && E.Target.Vivant ? E.DistanceTo(E.Target) < E.Taille*2 : false;
}

public class Fatigue : Interruption
{
    public int NiveauEnergie;
    public Fatigue(int niveauEnergie = 0)
    {
        NiveauEnergie = niveauEnergie;
    }
    public override bool Interrupt => E.Energie < NiveauEnergie;
}

public class Repose : Interruption
{
    public int NiveauEnergie;
    public Repose(int niveauEnergie)
    {
        NiveauEnergie = niveauEnergie;
    }
    public override bool Interrupt => E.Energie >= NiveauEnergie;
}

public class Faim : Interruption
{
    public int NiveauNouriture;
    public Faim(int niveauNouriture = 0)
    {
        NiveauNouriture = niveauNouriture;
    }
    public override bool Interrupt => E.Nourriture < NiveauNouriture;
}

public class Repu : Interruption
{
    public int NiveauNouriture;
    public Repu(int niveauNouriture)
    {
        NiveauNouriture = niveauNouriture;
    }
    public override bool Interrupt => E.Nourriture >= NiveauNouriture;
}

