using DependencyInjection;
using GameData;
using Gameplay.NPC;
using UnityEngine;

namespace Gameplay.BoardManagment
{
    public class Board : MonoBehaviour, IBoard
    {
        [SerializeField]
        private Cell[] _cellPrefabs;

        [SerializeField]
        private Cell _endPointCell;

        [SerializeField]
        private GameObject[] _emptyCells;
        [SerializeField]
        private Cell[,] _cells;

        [SerializeField]
        private PlayerWayFinder _playerPrefab;
        [SerializeField]
        private PlayerWayFinder _aiPrefab;
        private IServiceLocator _serviceLocator;

        public void Init(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            BoardData boardData = GameDataHodler.GetBoardData();
            CreateBoard(boardData, boardData.EndCell);
            CreatePlayer(boardData.StartCell, boardData.EndCell);
        }

        private void CreatePlayer(CellVector startCell, CellVector endCell)
        {
            PlayerWayFinder player = Instantiate(_playerPrefab, _cells[startCell.X, startCell.Y].transform.position, Quaternion.identity);
            player.SetData(startCell, endCell, _serviceLocator);

            PlayerWayFinder ai = Instantiate(_aiPrefab, _cells[startCell.X, startCell.Y].transform.position, Quaternion.identity);
            ai.SetData(startCell, endCell, _serviceLocator);
        }

        private bool[,] CreateBoard(BoardData boardData, CellVector endCell)
        {
            int rowLength = boardData.Row.Length;
            bool[,] filledCellMap = new bool[boardData.Row.Length, boardData.Row[0].CellData.Length];
            _cells = new Cell[boardData.Row.Length, boardData.Row[0].CellData.Length];
            for (int i = 0; i < rowLength; i++)
            {
                EditorRowData row = boardData.Row[i];
                EditorCellData[] cellData = row.CellData;

                int length = cellData.Length;

                GameObject rowGameObject = new GameObject($"Row{i}");
                rowGameObject.transform.SetParent(transform);
                for (int j = 0; j < length; j++)
                {

                    if (cellData[j].IsOn)
                    {
                        int horizontalIndex = i - (rowLength / 2);
                        int verticalIndex = j - (length / 2);
                        CellVector cellIndex = new CellVector(i, j);

                        Cell selectedCellPrefab = null;

                        if (cellIndex == endCell)
                        {
                            selectedCellPrefab = _endPointCell;
                        }
                        else
                        {
                            selectedCellPrefab = _cellPrefabs[Random.Range(0, _cellPrefabs.Length)];
                        }

                        Cell cell = Instantiate(selectedCellPrefab, new Vector3(horizontalIndex, verticalIndex, 0), Quaternion.identity);
                        cell.transform.localScale = new Vector3(1f, 1f, 1f);
                        cell.transform.SetParent(rowGameObject.transform);
                        cell.SetData(cellIndex, cellData[j].AvalabelDirections);
                        filledCellMap[i, j] = true;
                        _cells[i, j] = cell;
                    }
                    else
                    {
                        int horizontalIndex = i - (rowLength / 2);
                        int verticalIndex = j - (length / 2);
                        GameObject randomCell = _emptyCells[Random.Range(0, _emptyCells.Length)];
                        GameObject emptyCell = Instantiate(randomCell, new Vector3(horizontalIndex, verticalIndex, 0), Quaternion.identity);
                        emptyCell.transform.localScale = new Vector3(1, 1, 1);
                        emptyCell.transform.SetParent(rowGameObject.transform);
                        filledCellMap[i, j] = false;
                    }
                }
            }
            return filledCellMap;
        }

        public Vector3 GetCellPosition(CellVector cellIndex)
        {
            return _cells[cellIndex.X, cellIndex.Y].transform.position;
        }

        public CellVector[] GetCellDirection(CellVector cellIndex)
        {

            return _cells[cellIndex.X, cellIndex.Y].AvalabelDirections;
        }
    }

}