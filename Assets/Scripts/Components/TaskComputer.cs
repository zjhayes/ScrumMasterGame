using System.Collections.Generic;

public class TaskComputer : Computer
{
    private List<ICharacterController> developers;

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
        cartridge.Task.Completeness += .1f * developers.Count;
        if(cartridge.Task.IsReadyForProduction)
        {
            onTaskComplete?.Invoke();
            Sleep();
        }
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
