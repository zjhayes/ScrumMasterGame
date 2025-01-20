using UnityEngine;

public class SprintClock : MonoBehaviour
{
    private float totalTime;
    private float currentTime;

    public event Events.GameEvent OnExpiration;

    private void Start()
    {
        Stop(); // Stopped by default.
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if(currentTime <= 0)
        {
            Expired();
        }
    }

    public void StartTime()
    {
        this.enabled = true;
    }

    public void ResetTime()
    {
        currentTime = totalTime;
    }

    public void Stop()
    {
        this.enabled = false; ;
    }

    public float TotalTime 
    { 
        get { return totalTime; } 
        set { totalTime = value; }
    }

    public float CurrentTime 
    { 
        get { return currentTime; }
    }

    public int SimpleTime
    {
        get { return (int) currentTime; }
    }

    public string BeautifulTime
    {
        get
        {
            int time = (int) currentTime;
            int minutes = time / 60;
            int seconds = time % 60;
            return string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    public bool IsRunning
    {
        get { return this.enabled; }
    }

    private void Expired()
    {
        Stop();
        OnExpiration?.Invoke();
    }
}
