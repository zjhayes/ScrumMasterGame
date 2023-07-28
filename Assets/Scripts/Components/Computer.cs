using UnityEngine;

public abstract class Computer : GameBehaviour
{
    [SerializeField]
    Container cartridgeIntake;

    protected Task task;

    public delegate void OnSleep();
    public event OnSleep onSleep;

    protected virtual void Awake()
    {
        Sleep();
    }

    void Update()
    {
        if(CurrentCartridge == null)
        {
            CartridgeRemoved();
        }

        IterateWork();
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

    void CartridgeRemoved()
    {
        Sleep();
    }

    void Run()
    {
        // Start Update method.
        this.enabled = true;
    }

    protected void Sleep()
    {
        // Stop Update method.
        this.enabled = false;
        onSleep?.Invoke();
    }

    public Cartridge CurrentCartridge
    {
        get
        {
            return cartridgeIntake.GetFirst<Cartridge>() as Cartridge;
        }
    }

    public Container CartridgeIntake
    {
        get { return cartridgeIntake; }
    }
}
