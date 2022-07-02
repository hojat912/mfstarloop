using DependencyInjection;
using EventSystem;
using Gameplay.BoardManagment;
using System;
using UnityEngine;

public enum EPlayerType
{
    Player,
    AI
}
namespace Gameplay.NPC
{
    public class PlayerWayFinder : MonoBehaviour
    {
        [SerializeField]
        private EPlayerType _playerType;
        [SerializeField]
        private PlayerMovement _playerMover;
        private int _moveCount;
        private CellVector _endCell;
        private IEvent _event;
        private CellVector _currentCellIndex;
        private Action _onMoveDone;
        private IBoard _board;
        private IDirectionDecision _directionDecision;

        private void Awake()
        {

            _directionDecision = GetComponent<IDirectionDecision>();
        }

        public void SetData(CellVector startCell, CellVector endCell, IServiceLocator serviceLocator)
        {
            _endCell = endCell;
            _event = serviceLocator.GetService<IEvent>();
            _board = serviceLocator.GetService<IBoard>();

            _currentCellIndex = startCell;
            _event.ListenToEvent<EPlayerType, int, Action>(EEventType.OnPlayerTurn, SetTargetCount);
        }

        public void SetTargetCount(EPlayerType playerType, int moveCount, Action onMoveDone)
        {
            if (_playerType == playerType)
            {
                _onMoveDone = onMoveDone; ;
                _moveCount = moveCount;

                if (CanMoveInThisCount(_currentCellIndex, moveCount))
                {
                    FindNextCell();
                }
                else
                {
                    Debug.Log("cant move");
                    onMoveDone.Invoke();
                }
            }
        }

        private bool CanMoveInThisCount(CellVector currentCellIndex, int moveCount)
        {

            var startPoint = currentCellIndex;
            for (int i = 0; i < moveCount; i++)
            {
                CellVector[] directions = _board.GetCellDirection(startPoint);
                if (directions.Length > 0)
                {
                    int directionLenth = directions.Length;
                    for (int j = 0; j < directionLenth; j++)
                    {
                        var nextPoint = startPoint + directions[j];
                        return CanMoveInThisCount(nextPoint, moveCount - 1);
                    }

                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void FindNextCell()
        {
            CellVector[] directions = _board.GetCellDirection(_currentCellIndex);
            if (directions.Length == 1)
                StartMove(_currentCellIndex + directions[0]);
            else
            {
                _directionDecision.MakeDecision(directions, OnDecisionMade);
            }
        }

        public void OnDecisionMade(CellVector direction)
        {
            StartMove(_currentCellIndex + direction);
        }

        private void StartMove(CellVector cellIndex)
        {
            _currentCellIndex = cellIndex;
            Vector3 cellPosition = _board.GetCellPosition(cellIndex);
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
                if (_endCell == _currentCellIndex)
                    _event.BroadcastEvent<EPlayerType>(EEventType.OnPlayerWin, _playerType);
            }
        }
    }
}