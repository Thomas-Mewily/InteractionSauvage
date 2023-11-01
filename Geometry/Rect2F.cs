﻿#if USE_MONOGAME
using Microsoft.Xna.Framework;
#endif
using System;
using System.Collections.Generic;
using System.Text;

namespace Geometry;

public struct Rect2F
{
    public float XMin { get => _Min.X; set => _Min.X = value; }
    public float XMax { get => _Min.X + _Size.X; set => _Size.X = value - Min.X; }

    public float YMin { get => _Min.Y; set => _Min.Y = value; }
    public float YMax { get => _Min.Y + _Size.Y; set => _Size.Y = value - Min.Y; }

    public float SizeX => _Size.X;
    public float SizeY => _Size.Y;

    private Vec2 _Min;
    private Vec2 _Size;

    public Vec2 Min { get => _Min; set => _Min = value; }
    public Vec2 Size { get => _Size; set => _Size = value; }
    public Vec2 Max { get => _Min + _Size; set => _Size += (Max-value); }

    public float Area => SizeX * SizeY;

    public Rect2F(Vec2 min, Vec2 size) { _Min = min; _Size = size; }
    public Rect2F(float x, float y, float sizeX, float sizeY) { _Min = new Vec2(x, y); _Size = new Vec2(sizeX, sizeY); }

    public static Rect2F Zero => new(0, 0, 0, 0);
    public static Rect2F One  => new(0, 0, 1, 1);

    public bool Collide(Rect2F f) => XMin > f.XMax && XMax < f.XMin && YMin > f.YMax && YMax < f.YMin;

    public static bool operator ==(Rect2F a, Rect2F b) => a._Min == b._Min && a.Size == b.Size;
    public static bool operator !=(Rect2F a, Rect2F b) => !(a == b);

    public override string ToString() => _Min + " ; " + _Size;
    public override bool Equals(object obj) => (obj != null && obj is Rect2F v && v == this);
    public override int GetHashCode() => _Min.GetHashCode()+_Size.GetHashCode();
}
