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
    public InterruptionEnum InterruptionType;
    public int Arg;

    public Interruption(InterruptionEnum interruptionType, int arg = 0)
    {
        InterruptionType = interruptionType;
        Arg = arg;
    }
}