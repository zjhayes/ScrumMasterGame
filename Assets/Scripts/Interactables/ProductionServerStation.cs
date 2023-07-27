using UnityEngine;

[RequireComponent(typeof(ProductionServer))]
public class ProductionServerStation : Station
{
    ProductionServer computer;

    void Awake()
    {
        computer = GetComponent<ProductionServer>();
        computer.onDeploymentComplete += DismissAll;
    }

    protected override void Sit(ICharacterController occupant)
    {
        if (occupant.Inventory.HasPickup() && computer.CartridgeIntake.IsEmpty 
            && occupant.Inventory.CurrentPickup is Cartridge)
        {
            // Character has cartridge, put it in computer.
            computer.InputCartridge(occupant.Inventory.CurrentPickup as Cartridge);
        }
        else
        {
            occupant.Frustrated();
            return; // No cartridge, do something else.
        }
        base.Sit(occupant);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if (character.Inventory.CurrentPickup is Cartridge)
        {
            Cartridge cartridge = character.Inventory.CurrentPickup as Cartridge;
            if(cartridge.Task.IsReadyForProduction)
            {
                // Character has task that is ready for production.
                return PriorityScoreConstants.TAKE_TASK_TO_PRODUCTION;
            }
        }
        return PriorityScoreConstants.NO_SCORE;
    }
}
