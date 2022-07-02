using UnityEngine;

namespace Gameplay.BoardManagment
{
    public interface IBoard
    {
        Vector3 GetCellPosition(CellVector cellIndex);
        CellVector[] GetCellDirection(CellVector cellIndex);
    }
}