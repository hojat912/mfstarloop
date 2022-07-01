
using GameEditor.Data;
using UnityEngine;

public static class GameDataHodler
{
    private static BoardData _boardData;

    public static void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }

    public static BoardData GetBoardData()
    {
        return _boardData != null ? _boardData : JsonUtility.FromJson<BoardData>(PlayerPrefs.GetString("BoardData"));
    }
}
