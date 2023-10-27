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

    public static bool operator ==(Interruption a, Interruption b) => a.InterruptionType == b.InterruptionType;
    public static bool operator !=(Interruption a, Interruption b) => a.InterruptionType != b.InterruptionType;
    

    public override int GetHashCode() => (int)InterruptionType;
    public override bool Equals(object? obj) => obj != null && obj is Interruption i && i == this;
}