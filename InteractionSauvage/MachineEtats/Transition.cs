using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using Useful;
using static InteractionSauvage.MachineEtats.Etat;

namespace InteractionSauvage.MachinAeEtat;

public struct EtatSuivant
{
    public int Proba;
    public string Nom;

    public EtatSuivant(int proba, string nom)
    {
        Proba = proba;
        Nom = nom;
    }
}

public class Transition : EntiteComposante
{
    public string Nom;
    public Interruption Interruptions;

    public int ProbaTotal;
    public List<EtatSuivant> EtatSuivants;

    public Transition(string nom, Interruption interruptions, string suivant) : this(nom, interruptions, new EtatSuivant(1, suivant)) { }
    public Transition(string nom, Interruption interruptions, params EtatSuivant[] etatSuivants) : this(nom, interruptions, etatSuivants.ToList()) { }
    public Transition(string nom, Interruption interruptions, List<EtatSuivant> etatSuivants)
    {
        Nom = nom;
        Interruptions = interruptions;
        EtatSuivants = etatSuivants;
        ProbaTotal = EtatSuivants.Sum(t => t.Proba);
    }

    public override void Load()
    {
        Interruptions.Load(E);
    }

    public bool Interrupt() => Interruptions.Interrupt;
    public Etat DoTransition() 
    {
        int proba = Rand.Next(ProbaTotal);
        int sumProba = 0;

        foreach (EtatSuivant suivant in EtatSuivants)
        {
            sumProba += suivant.Proba;
            if (sumProba > proba)
            {
                return E.MachineEtat[suivant.Nom];
            }
        }

        Crash.Now("Je blame Mathis pour ça x)");
        return E.MachineEtat[E.MachineEtat.EtatSuggererParDefaut];
    }

    public override string ToString() => Nom;
}
