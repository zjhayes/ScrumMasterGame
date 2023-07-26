using System.Collections.Generic;
using UnityEngine;

public class TaskComputer : Computer
{
    Task task;
    List<ICharacterController> developers;

    public delegate void OnTaskComplete();
    public event OnTaskComplete onTaskComplete;

    protected override void Awake()
    {
        base.Awake();
        developers = new List<ICharacterController>();
    }

    protected override void IterateWork()
    {
        if(task.IsReadyForProduction)
        {
            onTaskComplete?.Invoke();
        }

        Debug.Log(developers);
        Debug.Log(task);
    }

    public override void InputCartridge(Cartridge cartridge)
    {
        // Capture task.
        task = cartridge.Task;
        base.InputCartridge(cartridge);
    }

    protected override void CartridgeRemoved()
    {
        task = null;
        base.CartridgeRemoved();
    }

    public void SignInDeveloper(ICharacterController developer)
    {
        developers.Add(developer);
    }

    public void SignOutDeveloper(ICharacterController developer)
    {
        developers.Remove(developer);
    }
}
