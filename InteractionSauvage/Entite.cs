using static InteractionSauvage.Etat;

namespace InteractionSauvage;

public class Entite
{
    public List<Etat> MachineEtat;
    public Etat Etat => MachineEtat[Actuel.EtatIndex];

    public float Score = 0;

    public Caracteristiques Actuel;
    public Caracteristiques DeBase;

    public Entite()
    {
        MachineEtat = new List<Etat>();
        DeBase = new Caracteristiques();
    }

    public void Reset() 
    {
        Actuel = DeBase;
    }
}