using UnityEngine;

public class SprintClock : MonoBehaviour
{
    float totalTime;
    float currentTime;
    bool isRunning;

    void Start()
    {
        Stop(); // Stopped by default.
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        Debug.Log(currentTime);

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
    public bool IsRunning
    {
        get { return this.enabled; }
    }

    private void Expired()
    {
        Stop();
    }
}
