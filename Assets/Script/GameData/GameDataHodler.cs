
using UnityEngine;

namespace GameData
{
    public static class GameDataHodler
    {
        private static BoardData _boardData;

        public static void SetBoardData(BoardData boardData)
        {
            _boardData = boardData;
        }

        public static BoardData GetBoardData()
        {
            if (_boardData == null)
            {
                BoardData playerPerefsBoardData = JsonUtility.FromJson<BoardData>(PlayerPrefs.GetString("BoardData"));
                if (playerPerefsBoardData != null)
                {
                    _boardData = playerPerefsBoardData;
                }
                else
                {
                    TextAsset mytxtData = Resources.Load<TextAsset>("Level1");
                    _boardData = JsonUtility.FromJson<BoardData>(mytxtData.text);
                }
            }
            return _boardData;
        }
    }
}