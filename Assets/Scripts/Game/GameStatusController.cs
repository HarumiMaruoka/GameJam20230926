// 日本語対応

using System;

public static class GameStatusController
{
    private static GameStatus _current;

    public static GameStatus Current => _current;

    public static event Action<GameStatus> OnStatusChanged;

    public static void ChangeGameStatus(GameStatus next)
    {
        var old = _current;
        _current = next;

        if (old != _current) OnStatusChanged?.Invoke(_current);
    }
}