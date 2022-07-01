using GameEditor.Data;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Cell[] _cellPrefabs;
    [SerializeField]
    private GameObject[] _emptyCells;
    [SerializeField]
    private Cell[,] _cells;

    [SerializeField]
    private PlayerWayFinder _playerPrefab;
    [SerializeField]
    private PlayerWayFinder _aiPrefab;

    private void Awake()
    {
        BoardData boardData = GameDataHodler.GetBoardData();
        CreateBoard(boardData);
        CreatePlayer(boardData.StartCell);
    }

    private void CreatePlayer(CellVector startCell)
    {
        PlayerWayFinder player = Instantiate(_playerPrefab, _cells[startCell.X, startCell.Y].transform.position, Quaternion.identity);
        player.SetMap(startCell);

        PlayerWayFinder ai = Instantiate(_aiPrefab, _cells[startCell.X, startCell.Y].transform.position, Quaternion.identity);
        ai.SetMap(startCell);

        FindObjectOfType<GamePlayUI>().SetPlayers(player, ai);
    }

    private bool[,] CreateBoard(BoardData boardData)
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
            for (int j = 0; j < length; j++)
            {

                if (cellData[j].IsOn)
                {
                    int horizontalIndex = i - (rowLength / 2);
                    int verticalIndex = j - (length / 2);
                    Cell randomCell = _cellPrefabs[Random.Range(0, _cellPrefabs.Length)];
                    Cell cell = Instantiate(randomCell, new Vector3(horizontalIndex, verticalIndex, 0), Quaternion.identity);
                    cell.transform.localScale = new Vector3(1f, 1f, 1f);
                    cell.transform.SetParent(rowGameObject.transform);
                    cell.SetData(new CellVector(i, j), cellData[j].AvalabelDirections);
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

