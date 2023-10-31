using System.Timers;
using Timer = System.Timers.Timer;
const int interval = 100;
double duration = 5000;
double totalDuration = 0;
long startTime = 0;
long lastTimerElapsedTime = 0;
Timer timer = new();
timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
timer.Interval = interval;
timer.AutoReset = true;

void OnTimerElapsed(object sender, ElapsedEventArgs e)
{
    var currentTime = GetEpochTime();
    totalDuration += CalculateDuration(currentTime);
    lastTimerElapsedTime = currentTime;
    timer.Interval = interval;
    if (totalDuration >= duration)
    {
        timer.Stop();
        System.Console.WriteLine("Timer Ended.. total duration : " + totalDuration + "ms");
    }

}

void Start()
{
    totalDuration = 0;
    timer?.Start();
    startTime = GetEpochTime();
    lastTimerElapsedTime = startTime;
    System.Console.WriteLine("Time started.. time: " + startTime);
}

void Stop()
{
    timer?.Stop();
    totalDuration = 0;
}

void Pause()
{
    var pauseTime = GetEpochTime();
    timer.Enabled = false;
    var runDuration = CalculateDuration(pauseTime);
    if (runDuration > interval)
    {
        runDuration = interval;
        timer.Interval = interval;
    }
    else
    {
        timer.Interval = timer.Interval - runDuration;
    }
    totalDuration += runDuration;
    System.Console.WriteLine("Time pause.. time: " + pauseTime);
}

void Resume()
{
    startTime = GetEpochTime();
    timer.Enabled = true;
    lastTimerElapsedTime = startTime;
    System.Console.WriteLine("Time resume.. time: " + startTime);
}

long GetEpochTime() => ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
long CalculateDuration(long currentTime) => currentTime - lastTimerElapsedTime;

while (true)
{
    var key = Console.ReadKey(true).Key;
    if (key == ConsoleKey.S)
    {
        Start();
    }
    if (key == ConsoleKey.P)
    {
        Pause();
    }
    else if (key == ConsoleKey.R)
    {
        Resume();
    }
    else if (key == ConsoleKey.Q)
    {
        Stop();
        break;
    }
}