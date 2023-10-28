using InteractionSauvage.Interruptions;
using InteractionSauvage.MachinAeEtat;
using InteractionSauvage.Passifs;

namespace InteractionSauvage.MachineEtats;

public class Etat : EntiteComposante
{
    public string Nom;

    public Passif Passif;
    public List<Transition> Transitions;

    public Temps TempsDebutActif;

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
        TempsDebutActif = Simu.Time;
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
                t.DoTransition();
            }
        }
    }

    override public string ToString() => Nom + " : " + Passif + "; transition : {" + string.Join(", ", Transitions) + "}";
}