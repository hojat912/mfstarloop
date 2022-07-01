using GameEditor.Data;
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


        public EditorRow[] Rows => _rows;

        private void Awake()
        {
            _saveButton.onClick.AddListener(SaveData);
            _clearButton.onClick.AddListener(OnClearButtonClicked);
            _gameButton.onClick.AddListener(OnGameButtonClicked);
        }

        private void OnGameButtonClicked()
        {
            SaveData();
            SceneManager.LoadScene(1);
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

            BoardData boardData = new BoardData(_rows.Length, _rows[0].Cells.Length);

            int rowLength = _rows.Length;
            for (int i = 0; i < rowLength; i++)
            {
                int cellLength = _rows[i].Cells.Length;
                for (int j = 0; j < cellLength; j++)
                {
                    EditorCell cell = _rows[i].Cells[j];
                    int heightLevel = cell.IsCellEnable ? cell.HeightLevel : 0;
                    boardData.AddCellData(i, j, heightLevel);

                }
            }

            GameDataHodler.SetBoardData(boardData);
        }
    }
}