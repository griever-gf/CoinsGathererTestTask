using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer
{
    public bool finished { get; set; }
    double currentTime;
    public UnityEvent eventTimerFinished = new UnityEvent();

    public CountdownTimer(double value)
    {
        this.finished = false;
        currentTime = value;
    }

    public double GetTime()
    {
        return currentTime;
    }

    public void DecreaseTime(float delta)
    {
        if (currentTime > 0)
            currentTime -= delta;
        else
        {
            currentTime = 0;
            finished = true;
            eventTimerFinished.Invoke();
        }
    }
}
