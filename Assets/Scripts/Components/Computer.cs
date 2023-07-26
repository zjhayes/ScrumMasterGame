using UnityEngine;

public abstract class Computer : MonoBehaviour
{
    [SerializeField]
    Container cartridgeIntake;

    void Awake()
    {
        Sleep();
        cartridgeIntake.onRemove += CartridgeRemoved;
    }

    void Update()
    {
        IterateWork();
    }

    protected abstract void IterateWork();

    public virtual void InputCartridge(Cartridge cartridge)
    {
        // Add cartridge to container.
        cartridgeIntake.Add(cartridge);
        // Move cartridge to intake.
        cartridge.Move(cartridgeIntake.gameObject.transform.position, false);
        Run();
    }

    protected virtual void CartridgeRemoved()
    {
        Sleep();
    }

    void Run()
    {
        // Start Update method.
        this.enabled = true;
    }

    void Sleep()
    {
        // Stop Update method.
        this.enabled = false;
    }

    public bool HasCartridge()
    {
        return cartridgeIntake.GetFirst<Cartridge>() != null;
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
