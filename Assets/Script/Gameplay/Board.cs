using GameEditor.Data;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Cell[] _cellPrefabs;
    [SerializeField]
    private GameObject[] _emptyCells;

    private void Awake()
    {
        BoardData boardData = GameDataHodler.GetBoardData();
        CreateBoard(boardData);
    }

    private void CreateBoard(BoardData boardData)
    {
        int rowLength = boardData.Row.Length;
        for (int i = 0; i < rowLength; i++)
        {
            EditorRowData row = boardData.Row[i];
            int[] cellLevel = row.CellLevel;
            int length = cellLevel.Length;
            for (int j = 0; j < length; j++)
            {
                if (cellLevel[j] > 0)
                {
                    int horizontalIndex = i - (rowLength / 2);
                    int verticalIndex = j - (length / 2);
                    Cell randomCell = _cellPrefabs[Random.Range(0, _cellPrefabs.Length)];
                    Cell cell = Instantiate(randomCell, new Vector3(horizontalIndex, verticalIndex, 0), Quaternion.identity);
                    cell.transform.localScale = new Vector3(1f, 1f, 1 + (cellLevel[j] * 0.4f));
                }
                else
                {
                    int horizontalIndex = i - (rowLength / 2);
                    int verticalIndex = j - (length / 2);
                    GameObject randomCell = _emptyCells[Random.Range(0, _emptyCells.Length)];
                    GameObject emptyCell = Instantiate(randomCell, new Vector3(horizontalIndex, verticalIndex, 0), Quaternion.identity);
                    emptyCell.transform.localScale = new Vector3(1, 1, 1);
                }

            }
        }
    }
}
