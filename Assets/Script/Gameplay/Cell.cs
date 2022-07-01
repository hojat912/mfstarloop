using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private GameObject _waySelector;

    private CellVector[] _avalabelDirections;

    private CellVector _cellIndex;

    public CellVector CellIndex => _cellIndex;

    public CellVector[] AvalabelDirections => _avalabelDirections;

    public void SetHeight(int height)
    {
        transform.localScale = new Vector3(1, height, 1);
    }

    public void SetData(CellVector cellIndex, CellVector[] avalabelDirections)
    {
        _avalabelDirections = avalabelDirections;
        _cellIndex = cellIndex;
        int length = _avalabelDirections.Length;
        if (length > 1)
        {

            for (int i = 0; i < length; i++)
            {
                GameObject _avalabelCellBadge = Instantiate(_waySelector, transform);
                _avalabelCellBadge.transform.localPosition = new Vector3(0, 0, -0.6f);
                _avalabelCellBadge.transform.localEulerAngles = new Vector3(0, 0, CalculateAngle(  _avalabelDirections[i]));
            }
        }
    }

    private float CalculateAngle(CellVector delta)
    {
        float value = (-Mathf.Atan2(delta.X, delta.Y) * Mathf.Rad2Deg);
        value += 360;
        value %= 360;
        return value;
    }
}

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