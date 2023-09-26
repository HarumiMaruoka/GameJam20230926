// 日本語対応

public static class GameStatusController
{
    private static GameStatus _current;

    public static GameStatus Current => _current;

    public static void ChangeGameStatus(GameStatus next)
    {
        _current = next;
    }
}