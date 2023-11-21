using Geometry;
using InteractionSauvage.MachineEtats;
using Useful;
using static InteractionSauvage.MachineEtats.Etat;

namespace InteractionSauvage.Interruptions;

public class Jamais : Interruption 
{
    public override bool Interrupt => false;

    public override Interruption Clone()
    {
        return new Jamais();
    }
}

public class Instantanee : Interruption
{
    public override bool Interrupt => true;

    public override Interruption Clone()
    {
        return new Instantanee();
    }
}

public class Apres : Interruption
{
    public Temps Temps;
    public Apres(Temps  temps) 
    {
        Temps = temps;
    }
    public override bool Interrupt => Simu.Time - AssocieA.TempsDebut > Temps;

    public override Interruption Clone()
    {
        return new Apres(Temps);
    }
}

public class ApresAleatoire : Interruption
{
    public Temps TempsMax;
    public Temps TempsMin;

    public Temps TempsRng;
    public List<Temps> TempsRngCp = new List<Temps>();

    public ApresAleatoire(Temps min, Temps max)
    {
        TempsMin = min;
        TempsMax = max;
        TempsRng = TempsMax;
    }

    public override void Debut()
    {
        TempsRng = Rand.IntUniform(TempsMin.T, TempsMax.T);
    }

    public override void CheckPointAdd()
    {
        TempsRngCp.Add(TempsRng);
        base.CheckPointAdd();
    }

    public override void CheckPointRollBack()
    {
        TempsRng = TempsRngCp.Peek();
        base.CheckPointRollBack();
    }

    public override void CheckPointRemove()
    {
        TempsRngCp.Pop();
        base.CheckPointRemove();
    }

    public override bool Interrupt => Simu.Time - AssocieA.TempsDebut > TempsRng;

    public override Interruption Clone()
    {
        return new ApresAleatoire(TempsMin, TempsMax);
    }
}

public class NourritureAtteignable : Interruption
{
    public override bool Interrupt => E.Target != null && E.Target.Vivant ? E.Collision(E.Target) : false;

    public override Interruption Clone()
    {
        return new NourritureAtteignable();
    }
}

public class PredateurVisible : Interruption
{
    public override bool Interrupt => E.TrouverPredateur() != null;

    public override Interruption Clone()
    {
        return new PredateurVisible();
    }
}

public class VoitNourritureJusteDevant : Interruption 
{
    public float CoefRayonVision;
    public override bool Interrupt 
    { 
        get {
            var tmp = E.ChampsVision;
            E.ChampsVision = Angle.FromDegree(1);
            bool b = E.EntitesVisibles().Any(e => E.PeutManger(e) && E.DistanceVisionTo(e) > E.RayonVision * CoefRayonVision);
            E.ChampsVision = tmp;
            E.Energie -= 0.0001f;
            return b;
        }
    
    }
    public override Interruption Clone() => new VoitNourritureJusteDevant(CoefRayonVision);
    public VoitNourritureJusteDevant(float coefRayonVisionApres = 0) { CoefRayonVision = coefRayonVisionApres; }
}

public class Fatigue : Interruption
{
    public float NiveauEnergie;
    public Fatigue(float niveauEnergie = 0.05f)
    {
        NiveauEnergie = niveauEnergie;
    }
    public override bool Interrupt => E.Energie < NiveauEnergie;

    public override Interruption Clone()
    {
        return new Fatigue(NiveauEnergie);
    }
}

public class Repose : Interruption
{
    public float NiveauEnergie;
    public Repose(float niveauEnergie)
    {
        NiveauEnergie = niveauEnergie;
    }
    public override bool Interrupt => E.Energie >= NiveauEnergie;

    public override Interruption Clone()
    {
        return new Repose(NiveauEnergie);
    }
}

/*
public class Faim : Interruption
{
    public int NiveauNouriture;
    public Faim(int niveauNouriture = 0)
    {
        NiveauNouriture = niveauNouriture;
    }
    public override bool Interrupt => E.Nourriture < NiveauNouriture;

    public override Interruption Clone()
    {
        return new Faim(NiveauNouriture);
    }
}*/

/*
public class Repu : Interruption
{
    public int NiveauNouriture;
    public Repu(int niveauNouriture)
    {
        NiveauNouriture = niveauNouriture;
    }
    public override bool Interrupt => E.Nourriture >= NiveauNouriture;

    public override Interruption Clone()
    {
        return new Repu(NiveauNouriture);
    }
}*/



public abstract class InterrupComposition : Interruption 
{
    public List<Interruption> Composites;
    public InterrupComposition(params Interruption[] ou) : this(ou.ToList()) { }
    public InterrupComposition(List<Interruption> ou)
    {
        Composites = ou;
    }
    public override void CheckPointAdd()
    {
        Composites.ForEach(t => t.CheckPointAdd());
        base.CheckPointAdd();
    }
    public override void CheckPointRemove()
    {
        Composites.ForEach(t => t.CheckPointRemove());
        base.CheckPointRemove();
    }
    public override void CheckPointRollBack()
    {
        Composites.ForEach(t => t.CheckPointRollBack());
        base.CheckPointRollBack();
    }
    public override void Load()
    {
        Composites.ForEach(t => t.Load());
    }
    public override void Debut()
    {
        Composites.ForEach(t => t.Debut());
        base.Debut();
    }
    public override void Fin()
    {
        Composites.ForEach(t => t.Fin());
        base.Fin();
    }
}

public class InterrupOU : InterrupComposition 
{
    public override bool Interrupt => Composites.Any(t => t.Interrupt);

    public override Interruption Clone()
    {
        return new InterrupOU();
    }
}

public class InterrupET : InterrupComposition
{
    public override bool Interrupt => Composites.All(t => t.Interrupt);

    public override Interruption Clone()
    {
        return new InterrupET();
    }
}
