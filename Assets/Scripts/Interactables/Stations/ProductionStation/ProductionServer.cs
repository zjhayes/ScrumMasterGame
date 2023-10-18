using UnityEngine;

[RequireComponent(typeof(ProductionStats))]
public class ProductionServer : Computer
{
    public Events.GameEvent OnDeployed;

    protected override void IterateWork()
    {
        if(cartridgeReceptacle.TryGet(out Cartridge cartridge))
        {
            cartridge.Story.Status = StoryStatus.DONE;

            // Work is deployed, cache cartridge object.
            gameManager.ObjectPool.PoolCartridge(cartridge);
            OnDeployed?.Invoke();

            gameManager.Sprint.EndSprintEarlyIfAllDone(); // TODO: Move this to SprintManager.
        }
        else
        {
            Sleep();
        }
    }


}
