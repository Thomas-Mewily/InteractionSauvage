
namespace Useful;

public struct Angle
{
    public float Degree
    {
        get => Radian * TurnDegree / TurnRadian;
        set => Radian = value * TurnRadian / TurnDegree;
    }
    public float Radian { get; set; }
    public float One { get { return Degree / TurnDegree; } set { Degree = value * TurnDegree; } }

    public float Cos => MathF.Cos(Radian);
    public float Sin => MathF.Sin(Radian);

    #region Const
    public const float TurnRadian = MathF.PI * 2;
    public const float TurnDegree = 360;
    public const float TurnOne = 1;

    public const float FlatRadian = TurnRadian / 2;
    public const float FlatDegree = TurnDegree / 2;
    public const float FlatOne = TurnOne / 2;

    public const float RightRadian = TurnRadian / 4;
    public const float RightDegree = TurnDegree / 4;
    public const float RightOne = TurnOne / 4;
    #endregion

    /// <summary>
    /// Based on the rad because it is used by the system for trigo (ex: Math.Sin)
    /// </summary>
    /// <param name="fromRadianUsedBySystem"></param>
    public Angle(float fromRadianUsedBySystem)
    {
        Radian = fromRadianUsedBySystem;
    }

    #region Operator
    public static bool operator ==(Angle a, Angle b) => a.Radian == b.Radian;
    public static bool operator !=(Angle a, Angle b) => a.Radian != b.Radian;

    public static Angle operator +(Angle a, Angle b) => a.Radian + b.Radian;
    public static Angle operator -(Angle a, Angle b) => a.Radian - b.Radian;

    public static Angle operator *(float a, Angle b) => a * b.Radian;
    public static Angle operator *(Angle a, float b) => b * a.Radian;

    public static Angle operator /(Angle a, Angle b) => a.Radian / b.Radian;
    public static Angle operator /(Angle a, float b) => a.Radian / b;

    public static implicit operator float(Angle a) => a.Radian;
    public static implicit operator Angle(float a) => FromRadian(a);
    #endregion

    #region Basic Method
    public override string ToString() => Degree + "°";
    public override bool Equals(object? obj) => obj is Angle a && a == this;
    public override int GetHashCode() => Radian.GetHashCode();
    #endregion

    #region Static Methods
    public static Angle FromDegree(float degree) => new Angle { Degree = degree };
    public static Angle FromRadian(float radian) => new Angle { Radian = radian };
    public static Angle AngleFromOne(float one)  => new Angle { One = one };

    public static Angle Zero => FromRadian(0);
    #endregion
}