using UnityEngine;

public class SprintClock : MonoBehaviour
{
    float totalTime;
    float currentTime;
    bool isRunning;

    void Update()
    {
        if(isRunning)
        {
            currentTime -= Time.deltaTime;
        }
    }

    public void Start()
    {
        currentTime = totalTime;
        isRunning = true;
    }

    public void Stop()
    {
        isRunning = false;
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
        get { return isRunning; }
    }
}
