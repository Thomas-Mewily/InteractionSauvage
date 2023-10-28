using InteractionSauvage.MachineEtats;
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

    public override bool Interrupt => Simu.Time - AssocieA.TempsDebutActif > Temps;
}