using UnityEngine;

/* A Computer runs methods on a Cartridge. */
public abstract class Computer : GameBehaviour
{
    [SerializeField]
    private Transform cartridgeIntakePosition;

    protected Cartridge cartridge;

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
        // While enabled, progress.
        IterateWork();
    }

    protected abstract void IterateWork();

    public virtual void InputCartridge(Cartridge newCartridge)
    {
        cartridge = newCartridge;
        cartridge.ClaimedBy = null;
        // Move cartridge to intake.
        cartridge.Move(cartridgeIntakePosition, false);
        cartridge.onParentChanged += OnCartridgeRemoved;
        Run();
    }

    public bool TryGetCartridge(out Cartridge outCartridge)
    {
        outCartridge = cartridge;
        return outCartridge != null;
    }

    public Cartridge Cartridge
    {
        get { return cartridge; }
    }

    public bool HasCartridge()
    {
        // Returns true if current cartridge is in intake.
        return cartridge != null && cartridgeIntakePosition.position == cartridge.transform.position;
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
        cartridge.onParentChanged -= OnCartridgeRemoved;
        cartridge = null;
        Sleep();
    }
}
