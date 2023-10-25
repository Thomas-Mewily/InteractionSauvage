using static InteractionSauvage.Etat;

namespace InteractionSauvage;

public class Interruption 
{
    public enum InterruptionEnum
    {
        Repose,
        Fatigue,

        Faim,
        Repu,

        Alerte,

        Timer,
    }
    public int Arg;

}