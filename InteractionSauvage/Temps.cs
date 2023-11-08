namespace InteractionSauvage;

public struct Temps
{
    public int T { get; set; }

    public static Temps OneSecond => new Temps(60);

    public static Temps Second(float s) => new Temps((int)(s * OneSecond.T));
    public static Temps MilliSecond(float s) => new Temps((int)((1000*s) * OneSecond.T));

    public Temps(int t) { T = t; }
    public static implicit operator Temps(int i) => new(i);
    public static implicit operator int(Temps t) => t.T;

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