using System;
using System.Diagnostics;

namespace Geometry;

public struct Angle
{
    #region Constructor
    public float Degree
    {
        get => _Radian * TurnDegree / TurnRadian;
        set => _Radian = value * TurnRadian / TurnDegree;
    }
    private float _Radian;
    public float Radian { get => _Radian; set => _Radian = (value % TurnRadian + TurnRadian) % TurnRadian; }
    public float One { get => _Radian / TurnRadian; set => _Radian = value * TurnRadian; }

    public float Cos => MathF.Cos(Radian);
    public float Sin => MathF.Sin(Radian);

    public bool EstEntre(Angle debut, Angle fin)
    {
        float angleNormalise = (Radian % (2 * (float)Math.PI) + 2 * (float)Math.PI) % (2 * (float)Math.PI);
        float debutNormalise = (debut.Radian % (2 * (float)Math.PI) + 2 * (float)Math.PI) % (2 * (float)Math.PI);
        float finNormalise = (fin.Radian % (2 * (float)Math.PI) + 2 * (float)Math.PI) % (2 * (float)Math.PI);

        if (debutNormalise <= finNormalise)
        {
            return angleNormalise >= debutNormalise && angleNormalise <= finNormalise;
        }
        else
        {
            return angleNormalise >= debutNormalise || angleNormalise <= finNormalise;
        }
    }

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
        _Radian = fromRadianUsedBySystem;
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
    public override bool Equals(object obj) => obj is Angle a && a == this;
    public override int GetHashCode() => Radian.GetHashCode();

    
    #endregion

    #region Static Methods
    public static Angle FromDegree(float degree) => new() { Degree = degree };
    public static Angle FromRadian(float radian) => new() { Radian = radian };
    public static Angle AngleFromOne(float one)  => new() { One = one };

    public static Angle Zero => FromRadian(0);
    #endregion
    #endregion
}