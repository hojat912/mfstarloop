
using System;
using UnityEngine;

public class PlayerWayFinder : MonoBehaviour
{
    [SerializeField]
    private PlayerMover _playerMover;
    private int _moveCount;

    private CellVector _currentCellIndex;
    private Action _onMoveDone;

    public void SetMap(CellVector startCell)
    {
        _currentCellIndex = startCell;
    }

    public void SetTargetCount(int moveCount,Action onMoveDone)
    {
        _onMoveDone = onMoveDone; ;
        _moveCount = moveCount;
        FindNextCell();
    }

    private void FindNextCell()
    {
        CellVector[] directions = FindObjectOfType<Board>().GetCellDirection(_currentCellIndex);
        StartMove(_currentCellIndex + directions[0]);
    }


    private void StartMove(CellVector cellIndex)
    {
        _currentCellIndex = cellIndex;
        Vector3 cellPosition = FindObjectOfType<Board>().GetCellPosition(cellIndex);
        _playerMover.SetTarget(cellPosition, OnArrived);
        _moveCount--;
    }

    public void OnArrived()
    {
        if (_moveCount > 0)
        {
            FindNextCell();
        }
        else
        {
            _onMoveDone.Invoke();
        }
    }
}
