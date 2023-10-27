using System.Reflection.Metadata.Ecma335;
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
    public Random rand = new Random();

    public Transition(Interruption interruptions, List<EtatSuivant> etatSuivants)
    {
        Interruptions = interruptions;
        EtatSuivants = etatSuivants;
        ProbaTotal = EtatSuivants.Sum(t => t.Etat);
    }

    public int doTransition(Interruption interruption)
    {
        if (!this.Interruptions.Equals(interruption)) return -1;

        int proba = rand.Next(100);
        int sumProba = 0;

        foreach(EtatSuivant e in EtatSuivants)
        {
            sumProba += e.Proba;
            if (sumProba > proba)
            {
                return e.Etat;
            }
            
        }

        return -1;
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
        Action      = action;
        Transitions = transitions;
    }


    override public string ToString()
    {
        return Action.ToString();
    }

    public int transitionTo(Interruption interruption)
    {
        int index = -1;
        foreach(Transition t in Transitions)
        {
            index = t.doTransition(interruption);
            if (index >= 0) return index;
        }

        return index;
    }
}