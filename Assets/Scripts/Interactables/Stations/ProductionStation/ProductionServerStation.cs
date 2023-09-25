using UnityEngine;

[RequireComponent(typeof(ProductionServer))]
public class ProductionServerStation : Station
{
    private ProductionServer computer;

    private void Awake()
    {
        computer = GetComponent<ProductionServer>();
        computer.OnSleep += DismissAll;
    }

    public override void InteractWith(ICharacterController character)
    {
        base.InteractWith(character);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if(character.Inventory.TryGet(out Cartridge cartridge) && cartridge.Task.IsReadyForProduction)
        {
            // Character has task that is ready for production.
            return PriorityScoreConstants.TAKE_TASK_TO_PRODUCTION;
        }
        return PriorityScoreConstants.NO_SCORE;
    }

    protected override void OnChairOccupied(ICharacterController occupant)
    {
        if (!computer.HasCartridge() && occupant.Inventory.TryGet(out Cartridge cartridge))
        {
            // Character has cartridge, put it in computer.
            computer.InputCartridge(cartridge);
        }
        else
        {
            occupant.Frustrated();
            DismissAll();
            return; // No cartridge, do something else.
        }
    }

    protected override void OnChairUnoccupied(ICharacterController occupant)
    {
        
    }

    protected override void OnCharacterDismiss(ICharacterController occupant)
    {
        occupant.FindSomethingToDo();
    }
}
