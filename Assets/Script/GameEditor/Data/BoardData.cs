using UnityEngine;

namespace GameEditor.Data
{

    [System.Serializable]
    public class BoardData
    {
        [SerializeField]
        private CellVector _startCell;
        [SerializeField]
        private CellVector _endCell;
        [SerializeField]
        private EditorRowData[] _row;

        public EditorRowData[] Row { get => _row; set => _row = value; }
        public CellVector StartCell => _startCell;

        public CellVector EndCell  => _endCell; 

        public BoardData(CellVector startCell, CellVector endCell, int width, int height)
        {
            _startCell = startCell;
            _endCell = endCell;
            _row = new EditorRowData[width];
            for (int i = 0; i < width; i++)
            {
                _row[i] = new EditorRowData(height);
            }
        }

        public void AddCellData(int horizontalIndex, int verticalIndex, EditorCellData value)
        {
            Row[horizontalIndex].SetData(verticalIndex, value);
        }
    }

    [System.Serializable]
    public class EditorRowData
    {
        [SerializeField]
        private EditorCellData[] _cellData;

        public EditorCellData[] CellData=> _cellData;

        public EditorRowData(int width)
        {
            _cellData = new EditorCellData[width];
        }

        public void SetData(int verticalIndex, EditorCellData value)
        {
            _cellData[verticalIndex] = value;
        }
    }
    [System.Serializable]
    public class EditorCellData
    {
        [SerializeField]
        private bool _isOn;

        [SerializeField]
        private CellVector[] _avalabelDirections;


        public EditorCellData(bool isCellEnable, CellVector[] avalabelDirections)
        {
            _isOn = isCellEnable;
            _avalabelDirections = avalabelDirections;
        }

        public bool IsOn => _isOn;

        public CellVector[] AvalabelDirections => _avalabelDirections;
    }
}
