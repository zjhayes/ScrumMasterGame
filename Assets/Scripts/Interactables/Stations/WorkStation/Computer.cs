using UnityEngine;

/* A Computer runs methods on a Cartridge. */
public abstract class Computer : GameBehaviour
{
    [SerializeField]
    protected Socket cartridgeReceptacle;

    public event Events.GameEvent OnRun;
    public event Events.GameEvent OnSleep;

    protected virtual void Awake()
    {
        cartridgeReceptacle.OnRemove += OnCartridgeRemoved;
        Sleep();
    }

    private void Update()
    {
        // While enabled, progress.
        IterateWork();
    }

    // What does the computer do with the cartridge?
    protected abstract void IterateWork();

    public virtual void InputCartridge(Cartridge cartridge)
    {
        // Move cartridge to computer dock.
        if(cartridgeReceptacle.TryPut(cartridge))
        {
            Run();
        } // else computer is in use.
    }

    public bool TryGetCartridge(out Cartridge outCartridge)
    {
        return cartridgeReceptacle.TryGet(out outCartridge);
    }

    public bool HasCartridge()
    {
        // Returns true if current cartridge is in intake.
        return cartridgeReceptacle.Has<Cartridge>();
    }

    public bool IsRunning
    {
        get { return this.enabled; }
    }

    protected void Run()
    {
        // Start Update method.
        this.enabled = true;
        OnRun?.Invoke();
    }

    protected void Sleep()
    {
        // Stop Update method.
        this.enabled = false;
        OnSleep?.Invoke();
    }

    protected void OnCartridgeRemoved(IContainable cartridge)
    {
        Sleep();
    }
}
