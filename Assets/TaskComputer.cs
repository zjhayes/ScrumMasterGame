using UnityEngine;

public class TaskComputer : MonoBehaviour
{
    [SerializeField]
    Container cartridgeIntake;

    public void InputCartridge(Cartridge cartridge)
    {
        cartridgeIntake.Add(cartridge);
        cartridge.EnablePhysics(false);
        cartridge.SetPositionToContainer(cartridgeIntake);
        cartridge.SetToHoldRotation();
    }

    public Container CartridgeIntake
    {
        get { return cartridgeIntake; }
    }
}
