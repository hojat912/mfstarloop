using System;
using UnityEngine;

[System.Serializable]
public struct CellVector : IEquatable<CellVector>, IComparable<CellVector>
{
    [SerializeField]
    private int _y;
    [SerializeField]
    private int _x;

    public int Y => _y;

    public int X => _x;

    public static CellVector Left => new CellVector(-1, 0);
    public static CellVector Right => new CellVector(1, 0);
    public static CellVector Up => new CellVector(0, 1);
    public static CellVector Down => new CellVector(0, -1);

    public static CellVector Defult => new CellVector(0, 0);

    public CellVector(int x, int y)
    {
        _y = y;
        _x = x;
    }

    public bool Equals(CellVector other)
    {
        return _y == other.Y && _x == other.X;
    }

    public int CompareTo(CellVector other)
    {
        bool isEuql = _y == other.Y && _x == other.X;
        return isEuql ? 0 : 1;
    }

    public static CellVector operator +(CellVector T1, CellVector T2)
    {
        return new CellVector(T1.X + T2.X, T1.Y + T2.Y);
    }
    public static CellVector operator -(CellVector T1, CellVector T2)
    {
        return new CellVector(T1.X - T2.X, T1.Y - T2.Y);
    }

    public static bool operator ==(CellVector T1, CellVector T2)
    {
        return T1.X == T2.X && T1.Y == T2.Y;
    }
    public static bool operator !=(CellVector T1, CellVector T2)
    {
        return T1.X != T2.X && T1.Y != T2.Y;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}