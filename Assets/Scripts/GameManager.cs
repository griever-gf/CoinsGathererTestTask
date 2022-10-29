using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager: MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        Initialization();
    }

    public CountdownTimer timer;
    public double defaultTimerValue = 18;

    void Initialization()
    {

    }

    private void Update()
    {
        if (!IsGameOver())
        {
            timer.DecreaseTime(Time.deltaTime);
            GameFieldObjectsController.instance.RefreshTimer();
        }
    }

    public bool IsGameOver()
    {
        return (GameData.instance.IsNoMoreCoins() || timer.finished);
    }

    public void SetTimer(UnityAction act)
    {
        timer = new CountdownTimer(defaultTimerValue);
        timer.eventTimerFinished.AddListener(act);
    }
}
