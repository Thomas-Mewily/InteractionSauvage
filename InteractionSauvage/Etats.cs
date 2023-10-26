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
        Dormir, //0

        Crier, //1

        Attendre, //2

        AssurerLaDescendence, //3

        MarcherNourriture, //4
        MarcherFuir, //5
        MarcherAleatoire //6
    }

    public ActionEnum Action;
    public List<Transition> Transitions;

    public Etat(ActionEnum action, params Transition[] transitions) : this(action, transitions.ToList()) { }
    public Etat(ActionEnum action, List<Transition> transitions)
    {
        Action = action;
        Transitions = transitions;
    }


    override public string ToString()
    {
        return Action.ToString();
    }
}