namespace InteractionSauvage;

public struct Temps
{
    public int T { get; set; }

    public Temps(int t) { T = t; }
    public static implicit operator Temps(int i) => new(i);

    public static bool operator > (Temps a, Temps b) => a.T >  b.T;
    public static bool operator >=(Temps a, Temps b) => a.T >= b.T;
    public static bool operator < (Temps a, Temps b) => a.T <  b.T;
    public static bool operator <=(Temps a, Temps b) => a.T <= b.T;

    public static bool operator ==(Temps a, Temps b) => a.T == b.T;
    public static bool operator !=(Temps a, Temps b) => !(a == b);

    public static Temps operator -(Temps a, Temps b) => a.T - b.T;

    public static Temps operator ++(Temps a) => a.T + 1;

    public override bool Equals(object? obj) => obj != null && obj is Temps t && t == this;
    public override int GetHashCode() => T;
}