
using GameEditor.Data;

public static class GameDataHodler 
{
    private static BoardData _boardData;

    public static void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }

    public static BoardData GetBoardData()
    {
        return _boardData;
    }
}
