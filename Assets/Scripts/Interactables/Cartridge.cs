using UnityEngine;

public class Cartridge : Pickup
{
    [SerializeField]
    Task task;

    public override void InteractWith(ICharacterController character)
    {
        // Direct character to go to open work station.
        WorkStation openWorkStation = gameManager.Interactables.FindOpenWorkStation();
        if(openWorkStation != null)
        {
            character.GoInteractWith(openWorkStation);
            base.InteractWith(character);
        }
        else
        {
            // Character unable to find open work station.
            character.Frustrated();
        }
    }

    public Task Task
    {
        get { return task; }
        set
        {
            task = value;
            // TODO: Update cartridge appearance based on task.
        }
    }
}
