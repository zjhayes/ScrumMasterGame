using UnityEngine;

public class SprintClock : MonoBehaviour
{
    float totalTime;
    float currentTime;
    bool isRunning;

    public delegate void OnExpiration();
    public event OnExpiration onExpiration;

    void Start()
    {
        Stop(); // Stopped by default.
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if(currentTime <= 0)
        {
            Expired();
        }
    }

    public void Begin()
    {
        currentTime = totalTime;
        this.enabled = true;
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
        onExpiration?.Invoke();
    }
}
