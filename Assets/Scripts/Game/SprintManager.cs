using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : Singleton<SprintManager>
{
    Sprint currentSprint;
    SprintClock clock;

    float sprintTime = 2.0f;

    protected override void Awake()
    {
        clock = GetComponent<SprintClock>();
        clock.TotalTime = sprintTime;
        StartSprint();
    }

    void Update()
    {
        Debug.Log(clock.CurrentTime);
    }

    private void StartSprint()
    {
        clock.Start();
    }
}
