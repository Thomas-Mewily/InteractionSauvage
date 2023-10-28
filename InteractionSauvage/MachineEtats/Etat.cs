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
        TempsDebut = Simu.Time;
    }

    public void Fin()
    {
        Passif.Fin();
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

    public override void CheckPointAdd()
    {
        CheckPointsTempsDebut.Push(TempsDebut);
        base.CheckPointAdd();
    }

    public override void CheckPointRemove()
    {
        CheckPointsTempsDebut.Pop();
        base.CheckPointRemove();
    }

    public override void CheckPointRollBack()
    {
        TempsDebut = CheckPointsTempsDebut.Peek();
        base.CheckPointRollBack();
    }

    override public string ToString() => Nom + " : " + Passif + "; transition : {" + string.Join(", ", Transitions) + "}";
}