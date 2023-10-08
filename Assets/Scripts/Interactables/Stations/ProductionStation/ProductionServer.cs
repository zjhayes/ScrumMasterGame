using UnityEngine;

[RequireComponent(typeof(ProductionStats))]
public class ProductionServer : Computer
{
    protected override void IterateWork()
    {
        if(cartridgeReceptacle.TryGet(out Cartridge cartridge))
        {
            cartridge.Story.Status = StoryStatus.DONE;

            // Work is deployed, cache cartridge object.
            gameManager.ObjectPool.PoolCartridge(cartridge);
            gameManager.Sprint.EndSprintEarlyIfAllDone();
        }
        else
        {
            Sleep();
        }
    }


}
