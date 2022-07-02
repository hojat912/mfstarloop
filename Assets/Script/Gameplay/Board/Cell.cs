using System;
using UnityEngine;
namespace Gameplay.BoardManagment
{
    public class Cell : MonoBehaviour
    {

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

        }

        private float CalculateAngle(CellVector delta)
        {
            float value = (-Mathf.Atan2(delta.X, delta.Y) * Mathf.Rad2Deg);
            value += 360;
            value %= 360;
            return value;
        }
    }

}