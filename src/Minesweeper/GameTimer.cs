using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace Minesweeper;

internal sealed class GameTimer : IDisposable
{
    private readonly long _startTime;
    private readonly Timer _timer;

    private GameTimer(Action<TimeSpan> onTick)
    {
        _startTime = Stopwatch.GetTimestamp();
        _timer = new Timer(TimeSpan.FromSeconds(1));
        _timer.Elapsed += (_, _) => onTick(Stopwatch.GetElapsedTime(_startTime));
    }

    public TimeSpan Elapsed => Stopwatch.GetElapsedTime(_startTime);

    public static GameTimer StartNew(Action<TimeSpan> onTick)
    {
        var gameTimer = new GameTimer(onTick);
        gameTimer._timer.Start();
        return gameTimer;
    }

    public void Dispose() => _timer.Dispose();
}