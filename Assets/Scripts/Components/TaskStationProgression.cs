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
        if(computer.TryGetCartridge(out Cartridge cartridge))
        {
            progressBar.CurrentFill = cartridge.Task.Completeness;
        }
        else
        {
            HideProgressBar();
        }
    }
}
