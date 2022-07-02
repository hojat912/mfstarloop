using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameEditor.Core
{

    public class GameEditorManager : MonoBehaviour
    {

        [SerializeField]
        private EditorRow[] _rows;

        [SerializeField]
        private Button _saveButton;

        [SerializeField]
        private Button _clearButton;

        [SerializeField]
        private Button _gameButton;

        [SerializeField]
        private Button _selectStartCellButton;

        [SerializeField]
        private Button _selectEndCellButton;

        [SerializeField]
        private Button _selectDirectionMode;

        public EditorRow[] Rows => _rows;

        private CellVector _startCellIndex;
        private CellVector _endCellIndex;


        private void Awake()
        {
            _saveButton.onClick.AddListener(SaveData);
            _clearButton.onClick.AddListener(OnClearButtonClicked);
            _gameButton.onClick.AddListener(OnGameButtonClicked);
            _selectStartCellButton.onClick.AddListener(OnSelectStartCellButtonClicked);
            _selectEndCellButton.onClick.AddListener(OnSelectEndCellButtonClicked);
            _selectDirectionMode.onClick.AddListener(OnSelectDirectionModeButtonClicked);

            BoardData boardData = GameDataHodler.GetBoardData();
            SetBoard(boardData);

        }

        private void OnSelectDirectionModeButtonClicked()
        {
            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int length = _rows[i].Cells.Length;
                for (int j = 0; j < length; j++)
                {
                    _rows[i].Cells[j].SelecCellMode(OnSelectDirectionCellClicked);
                }
            }
        }

        private CellVector? _selectDirectionStartCell;

        private void OnSelectDirectionCellClicked(CellVector index)
        {

            if (_selectDirectionStartCell == null)
            {
                _selectDirectionStartCell = index;
                _rows[_selectDirectionStartCell.Value.X].Cells[_selectDirectionStartCell.Value.Y].SetAsDirectionSelector();
            }
            else
            {
                if (_selectDirectionStartCell == index)
                {
                    _rows[_selectDirectionStartCell.Value.X].Cells[_selectDirectionStartCell.Value.Y].CleareMoveDirections();
                    _selectDirectionStartCell = null;

                }
                else
                {

                    CellVector direction = index - _selectDirectionStartCell.Value;
                    if (direction.X <= 1 && direction.X >= -1 && direction.Y <= 1 && direction.Y >= -1)
                        _rows[_selectDirectionStartCell.Value.X].Cells[_selectDirectionStartCell.Value.Y].AddMoveDirection(direction);

                    _selectDirectionStartCell = null;
                }

                int rowLength = _rows.Length;
                for (int i = 0; i < rowLength; i++)
                {
                    int length = _rows[i].Cells.Length;
                    for (int j = 0; j < length; j++)
                    {
                        _rows[i].Cells[j].SetAsDefult();
                    }
                }
                if (_startCellIndex != null)
                {
                    _rows[_startCellIndex.X].Cells[_startCellIndex.Y].SetAsStartNode();
                }
                if (_endCellIndex != null)
                {
                    _rows[_endCellIndex.X].Cells[_endCellIndex.Y].SetAsEndNode();
                }
            }
        }

        private void OnSelectEndCellButtonClicked()
        {
            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int length = _rows[i].Cells.Length;
                for (int j = 0; j < length; j++)
                {
                    _rows[i].Cells[j].SelecCellMode(OnEndCellChoosed);
                }
            }
        }

        private void OnEndCellChoosed(CellVector cellIndex)
        {
            _endCellIndex = cellIndex;
            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int length = _rows[i].Cells.Length;
                for (int j = 0; j < length; j++)
                {
                    _rows[i].Cells[j].ToggleCellMode();
                    _rows[i].Cells[j].SetAsDefult();
                }
            }
            if (_startCellIndex != null)
            {
                _rows[_startCellIndex.X].Cells[_startCellIndex.Y].SetAsStartNode();
            }
            if (_endCellIndex != null)
            {
                _rows[_endCellIndex.X].Cells[_endCellIndex.Y].SetAsEndNode();
            }
        }

        private void OnSelectStartCellButtonClicked()
        {
            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int length = _rows[i].Cells.Length;
                for (int j = 0; j < length; j++)
                {
                    _rows[i].Cells[j].SelecCellMode(OnStartCellChoosed);
                }
            }
        }

        private void OnStartCellChoosed(CellVector cellIndex)
        {
            _startCellIndex = cellIndex;
            int rowLength = _rows.Length;


            for (int i = 0; i < rowLength; i++)
            {
                int length = _rows[i].Cells.Length;
                for (int j = 0; j < length; j++)
                {
                    _rows[i].Cells[j].ToggleCellMode();
                    _rows[i].Cells[j].SetAsDefult();
                }
            }
            if (_startCellIndex != null)
            {
                _rows[_startCellIndex.X].Cells[_startCellIndex.Y].SetAsStartNode();
            }
            if (_endCellIndex != null)
            {
                _rows[_endCellIndex.X].Cells[_endCellIndex.Y].SetAsEndNode();
            }
        }


        private void SetBoard(BoardData boardData)
        {
            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int cellLength = _rows[i].Cells.Length;
                for (int j = 0; j < cellLength; j++)
                {
                    EditorCell cell = _rows[i].Cells[j];
                    cell.SetData(boardData.Row[i].CellData[j], new CellVector(i, j));
                    cell.ToggleCellMode();
                    cell.SetAsDefult();
                }
            }
            _startCellIndex = boardData.StartCell;
            _endCellIndex = boardData.EndCell;

            if (_startCellIndex != null)
            {
                _rows[_startCellIndex.X].Cells[_startCellIndex.Y].SetAsStartNode();
            }
            if (_endCellIndex != null)
            {
                _rows[_endCellIndex.X].Cells[_endCellIndex.Y].SetAsEndNode();
            }
        }


        private void OnGameButtonClicked()
        {
            SaveData();
            SceneManager.LoadScene(0);
        }

        private void OnClearButtonClicked()
        {
            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int cellLength = _rows[i].Cells.Length;
                for (int j = 0; j < cellLength; j++)
                {
                    EditorCell cell = _rows[i].Cells[j];
                    cell.Clear();
                }
            }
        }

        private void SaveData()
        {

            BoardData boardData = new BoardData(_startCellIndex, _endCellIndex, _rows.Length, _rows[0].Cells.Length);

            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int cellLength = _rows[i].Cells.Length;
                for (int j = 0; j < cellLength; j++)
                {
                    EditorCell cell = _rows[i].Cells[j];
                    int count = cell.Arrows.Count;
                    CellVector[] avalabelDirection = new CellVector[count];

                    for (int h = 0; h < count; h++)
                    {
                        CellVector moveDirection = cell.Arrows[h].MoveDirection;
                        avalabelDirection[h] = moveDirection;
                    }

                    boardData.AddCellData(i, j, new EditorCellData(cell.IsCellEnable, avalabelDirection));

                }
            }

            string stringData = JsonUtility.ToJson(boardData);
            PlayerPrefs.SetString("BoardData", stringData);
            GameDataHodler.SetBoardData(boardData);
        }
    }
}