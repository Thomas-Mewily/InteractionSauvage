using Geometry;
using InteractionSauvage;
using InteractionSauvage.Interruptions;
using InteractionSauvage.MachineEtats;
using InteractionSauvage.Passifs;

using SimulationGraphique.Animations;
using Useful;

namespace SimulationGraphique;

public class SimulationFactoryGraphique : SimulationFactory
{
    public SimulationFactoryGraphique() : base(null) { }

    public override Entite GenerateHerbe()
    {
        var herbe = base.GenerateHerbe();
        herbe.Animation = new HerbeAnim();
        return herbe;
    }

    public override Entite GenerateMouton()
    {
        var mouton = base.GenerateMouton();
        mouton.Animation = new MoutonAnim();
        return mouton;
    }
}