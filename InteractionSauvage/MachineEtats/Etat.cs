using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;
using Useful;

namespace InteractionSauvage.MachineEtats;

public class Etat : EntiteComposante
{
    public string Nom;

    public Passif Passif;
    public List<Transition> Transitions;

    public Temps TempsDebut = 0;
    public List<Temps> CheckPointsTempsDebut = new();

    public Etat(string? nom, Passif passif, params Transition[] transitions): this(nom, passif, transitions.ToList()) { }
    public Etat(string? nom, Passif passif, List<Transition> transitions)
    {
        Nom = nom ?? "?";
        Passif = passif;
        Transitions = transitions;
    }

    public void Debut() 
    {
        Passif.Debut();
        Transitions.ForEach(t => t.Debut());
        TempsDebut = Simu.Time;
    }

    public void Fin()
    {
        Passif.Fin();
        Transitions.ForEach(t => t.Fin());
    }

    public override void Load()
    {
        Passif.Load(E);
        foreach(var t in Transitions) 
        {
            t.Interruptions.MaybeAssocieA = this;
            t.Load(E);
        }
    }

    public void Update() 
    {
        Passif.Execute();
        foreach(var t in Transitions) 
        {
            if (t.Interrupt()) 
            {
                E.Etat = t.DoTransition();
            }
        }
    }

    public Etat Clone()
    {
        Passif newPassif = Passif.Clone();
        List<Transition> newTransitions = new List<Transition>();
        foreach (Transition transition in Transitions)
        {
            newTransitions.Add(transition.Clone());
        }

        return new Etat(Nom, newPassif, newTransitions);
    }

    #region CheckPoint
    public override void CheckPointAdd()
    {
        CheckPointsTempsDebut.Push(TempsDebut);
        base.CheckPointAdd();

        Passif.CheckPointAdd();
        Transitions.ForEach(t => t.CheckPointAdd());
    }

    public override void CheckPointRemove()
    {
        Passif.CheckPointRemove();
        Transitions.ForEach(t => t.CheckPointRemove());

        CheckPointsTempsDebut.Pop();
        base.CheckPointRemove();
    }

    public override void CheckPointRollBack()
    {
        Passif.CheckPointRollBack();
        Transitions.ForEach(t => t.CheckPointRollBack());

        TempsDebut = CheckPointsTempsDebut.Peek();
        base.CheckPointRollBack();
    }

    public override void CheckPointReset()
    {
        Passif.CheckPointReset();
        Transitions.ForEach(t => t.CheckPointReset());
        base.CheckPointReset();
    }
    #endregion

    override public string ToString() => Nom + " : " + Passif + "; transition : {" + string.Join(", ", Transitions) + "}";
}