using UnityEngine;

public abstract class Computer : GameBehaviour
{
    [SerializeField]
    private Container cartridgeIntake;

    protected Task task;

    public delegate void OnRun();
    public event OnRun onRun;
    public delegate void OnSleep();
    public event OnSleep onSleep;

    protected virtual void Awake()
    {
        Sleep();
    }

    private void Update()
    {
        if(HasCartridge())
        {
            IterateWork();
        }
        else
        {
            // Cartridge removed, stop work
            OnCartridgeRemoved();
        }
    }

    protected abstract void IterateWork();

    public virtual void InputCartridge(Cartridge cartridge)
    {
        // Add cartridge to container.
        cartridge.ClaimedBy = null;
        cartridgeIntake.Add(cartridge);
        // Move cartridge to intake.
        cartridge.Move(cartridgeIntake.gameObject.transform.position, false);
        // Capture task.
        task = cartridge.Task;
        Run();
    }

    private void OnCartridgeRemoved()
    {
        Sleep();
    }

    private void Run()
    {
        // Start Update method.
        this.enabled = true;
        onRun?.Invoke();
    }

    protected void Sleep()
    {
        // Stop Update method.
        this.enabled = false;
        onSleep?.Invoke();
    }

    public bool TryGetCartridge(out Cartridge cartridge)
    {
        return cartridgeIntake.TryGetFirst(out cartridge);
    }

    public bool HasCartridge()
    {
        return !cartridgeIntake.IsEmpty;
    }

    public Container CartridgeIntake
    {
        get { return cartridgeIntake; }
    }

    public bool IsRunning
    {
        get { return this.enabled; }
    }
}
