using UnityEngine;

/* A Computer runs methods on a Cartridge. */
public abstract class Computer : GameBehaviour
{
    [SerializeField]
    protected CartridgeReceptacle cartridgeReceptacle;

    public delegate void OnRun();
    public event OnRun onRun;
    public delegate void OnSleep();
    public event OnSleep onSleep;

    protected virtual void Awake()
    {
        cartridgeReceptacle.onRemoved += OnCartridgeRemoved;
        Sleep();
    }

    private void Update()
    {
        // While enabled, progress.
        IterateWork();
    }

    protected abstract void IterateWork();

    public virtual void InputCartridge(Cartridge cartridge)
    {
        // Move cartridge to computer dock.
        if(cartridgeReceptacle.TryPutPickup(cartridge))
        {
            cartridge.ClaimedBy = null;
            Run();
        } // else computer is in use.
    }

    public bool TryGetCartridge(out Cartridge outCartridge)
    {
        return cartridgeReceptacle.TryGetPickup(out outCartridge);
    }

    public bool HasCartridge()
    {
        // Returns true if current cartridge is in intake.
        return cartridgeReceptacle.HasPickup();
    }

    public bool IsRunning
    {
        get { return this.enabled; }
    }

    protected void Run()
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

    protected void OnCartridgeRemoved()
    {
        Sleep();
    }
}
