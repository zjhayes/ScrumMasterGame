using UnityEngine;

[RequireComponent(typeof(ProductionServer))]
public class ProductionServerStation : Station
{
    private ProductionServer computer;

    private void Awake()
    {
        computer = GetComponent<ProductionServer>();
        computer.onDeploymentComplete += DismissAll;
    }

    protected override void Sit(ICharacterController occupant)
    {
        if(!computer.HasCartridge() && occupant.Inventory.TryGetPickup(out Cartridge cartridge))
        {
            // Character has cartridge, put it in computer.
            computer.InputCartridge(cartridge);
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
        if(character.Inventory.TryGetPickup(out Cartridge cartridge) && cartridge.Task.IsReadyForProduction)
        {
            // Character has task that is ready for production.
            return PriorityScoreConstants.TAKE_TASK_TO_PRODUCTION;
        }
        return PriorityScoreConstants.NO_SCORE;
    }
}
