using UnityEngine;

[RequireComponent(typeof(Computer))]
public class TaskStationProgression : StationProgression
{
    private Computer computer;

    protected override void Awake()
    {
        computer = GetComponent<Computer>();

        computer.onRun += ShowProgressBar;
        computer.onSleep += HideProgressBar;

        base.Awake();
    }

    private void Update()
    {
        //progressBar.CurrentFill = computer.Task?.Completeness;
    }
}
