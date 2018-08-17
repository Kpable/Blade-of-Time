using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    #region Events 

    public delegate void SecondsChanged(int seconds);
    public event SecondsChanged OnSecondsChanged;

    public delegate void TimeUp();
    public event TimeUp OnTimeUp;

    #endregion 

    #region Private Variables

    private bool running;
    private float duration;
    private float remainingTime;
    private float secondsTracker;

    #endregion 


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (running)
        {
            // Reduce remainingTime
            remainingTime -= Time.deltaTime;
            // Clamp Time so it does not go below 0
            remainingTime = Mathf.Clamp(remainingTime, 0, remainingTime);

            // If 1 second has passed, alert listeners of the change in seconds
            if (secondsTracker - remainingTime >= 1 && OnSecondsChanged != null)
            {
                OnSecondsChanged((int)Mathf.Ceil(remainingTime));
                secondsTracker = remainingTime;
            }

            if (remainingTime <= 0)
            {
                TimeExpired();
            }
        }
    }

    public void StartTimer(float duration = 0)
    {
        if (duration > 0) Set(duration);
        running = true;
    }

    public void Stop()
    {
        running = false;
    }

    public void Set(float duration, bool start = false)
    {
        this.remainingTime = duration;
        this.duration = duration;
        secondsTracker = duration;
        if (start) StartTimer();
    }

    public void Reset()
    {
        remainingTime = duration;
    }

    void TimeExpired()
    {
        Stop();
        if (OnTimeUp != null) OnTimeUp();
    }

}
