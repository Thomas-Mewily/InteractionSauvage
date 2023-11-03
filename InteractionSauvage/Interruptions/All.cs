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
    public Temps Temps;
    public ApresAleatoire(Temps min, Temps max)
    {
        Temps = Rand.IntUniform(min.T, max.T);
    }
    /*
    public override void Load(Entite e)
    {
        base.Load(e);
    }*/
    public override bool Interrupt => Simu.Time - AssocieA.TempsDebut > Temps;
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

