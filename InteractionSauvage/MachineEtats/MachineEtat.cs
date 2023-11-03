using InteractionSauvage.MachineEtats;
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

    #region CheckPoint
    public override void CheckPointAdd()
    {
        foreach(var v in Etats) 
        {
            v.Value.CheckPointAdd();
        }
        base.CheckPointAdd();
    }

    public override void CheckPointRemove()
    {
        foreach (var v in Etats)
        {
            v.Value.CheckPointRemove();
        }
        base.CheckPointRemove();
    }

    public override void CheckPointRollBack()
    {
        foreach (var v in Etats)
        {
            v.Value.CheckPointRollBack();
        }
        base.CheckPointRollBack();
    }

    public override void CheckPointReset()
    {
        foreach (var v in Etats)
        {
            v.Value.CheckPointReset();
        }
        base.CheckPointReset();
    }
    #endregion
}
