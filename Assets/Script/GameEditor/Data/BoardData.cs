using UnityEngine;

namespace GameEditor.Data
{

    [System.Serializable]
    public class BoardData
    {

        [SerializeField]
        private EditorRowData[] _row;

        public EditorRowData[] Row { get => _row; set => _row = value; }


        public BoardData(int width, int height)
        {
            _row = new EditorRowData[width];
            for (int i = 0; i < width; i++)
            {
                _row[i] = new EditorRowData(height);
            }
        }

        public void AddCellData(int horizontalIndex, int verticalIndex, int value)
        {
            Row[horizontalIndex].SetData(verticalIndex, value);
        }
    }

    [System.Serializable]
    public class EditorRowData
    {
        [SerializeField]
        private int[] _cellLevel;

        public int[] CellLevel => _cellLevel;

        public EditorRowData(int width)
        {
            _cellLevel = new int[width];
        }

        public void SetData(int verticalIndex, int value)
        {
            _cellLevel[verticalIndex] = value;
        }
    }
}
