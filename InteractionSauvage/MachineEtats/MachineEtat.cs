using InteractionSauvage.MachinAeEtat;
using InteractionSauvage.Passifs;
using static InteractionSauvage.MachineEtats.Etat;

namespace InteractionSauvage.MachineEtats;

public class MachineEtat : EntiteComposante
{
    private Dictionary<string, Etat> Etats;
    public Etat this[string i] => Etats[i];
    public string EtatSuggererParDefaut = "?";

    public Etat Add(string nom, Passif passif, params Transition[] transitions)
    {
        if(Etats.Count == 0) 
        {
            EtatSuggererParDefaut = nom;
        }
        var e = new Etat(nom, passif, transitions);
        Etats.Add(nom, e);
        return e;
    }

    public MachineEtat(Dictionary<string, Etat>? machineEtat = null)
    {
        Etats = (machineEtat == null ? new() : machineEtat);
    }

    public override void Load()
    {
        foreach(var v in Etats) 
        {
            v.Value.Load(E);
        }
    }

    public override string ToString() => "Machine à Etat : {" + string.Join(", ", Etats.ToList().Select(t => t.Key)) + "}";
}
