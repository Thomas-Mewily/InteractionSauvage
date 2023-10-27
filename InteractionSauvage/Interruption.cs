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

        None,
    }
    public InterruptionEnum InterruptionType;
    public int Arg;

    public Interruption(InterruptionEnum interruptionType, int arg = 0)
    {
        InterruptionType = interruptionType;
        Arg = arg;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != typeof(Interruption)) return false;
        if (((Interruption)obj).InterruptionType == this.InterruptionType) return true;

        return false;
    }
}