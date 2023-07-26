using System.Collections.Generic;
using UnityEngine;

public class TaskComputer : Computer
{
    Task task;
    List<ICharacterController> developers;

    protected override void IterateWork()
    {

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
