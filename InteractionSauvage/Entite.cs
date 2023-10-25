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

    public float Score = 0;

    public Caracteristiques Actuel;
    public Caracteristiques DeBase;

    public Entite(MachineEtat e)
    {
        MachineEtat = e;
        DeBase = new Caracteristiques();
    }

    public void Reset() 
    {
        Actuel = DeBase;
    }
}