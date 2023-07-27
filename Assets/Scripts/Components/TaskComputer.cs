using System.Collections.Generic;

public class TaskComputer : Computer
{
    List<ICharacterController> developers;

    public delegate void OnTaskComplete();
    public event OnTaskComplete onTaskComplete;

    protected override void Awake()
    {
        base.Awake();
        developers = new List<ICharacterController>();
    }

    // Update task completeness and developer progression.
    protected override void IterateWork()
    {
        task.Completeness += .1f * developers.Count;
        if(task.IsReadyForProduction)
        {
            onTaskComplete?.Invoke();
            Sleep();
        }
    }

    public override void InputCartridge(Cartridge cartridge)
    {
        // Capture task.
        task = cartridge.Task;
        base.InputCartridge(cartridge);
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
