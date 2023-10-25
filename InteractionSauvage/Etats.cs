using static InteractionSauvage.Etat;

namespace InteractionSauvage;

public struct EtatSuivant
{
    public int Proba;
    public int Etat;

    public EtatSuivant(int proba, int etat)
    {
        Proba = proba;
        Etat = etat;
    }
}

public class Transition 
{
    public Interruption Interruptions;
    public int ProbaTotal;
    public List<EtatSuivant> EtatSuivants;

    public Transition(Interruption interruptions, List<EtatSuivant> etatSuivants)
    {
        Interruptions = interruptions;
        EtatSuivants = etatSuivants;
        ProbaTotal = EtatSuivants.Sum(t => t.Etat);
    }
}

public class Etat
{
    public enum ActionEnum
    {
        Dormir,

        Crier,

        Attendre,

        AssurerLaDescendence,

        MarcherNourriture,
        MarcherFuir,
        MarcherAleatoire
    }

    public ActionEnum Action;
    public List<Transition> Transitions;

    public Etat(ActionEnum action, List<Transition> transitions)
    {
        Action = action;
        Transitions = transitions;
    }
}