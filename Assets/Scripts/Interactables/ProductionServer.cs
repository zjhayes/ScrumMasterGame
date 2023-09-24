
public class ProductionServer : Computer
{
    protected override void IterateWork()
    {
        if(cartridgeReceptacle.TryGet(out Cartridge cartridge))
        {
            cartridge.Task.Status = TaskStatus.DONE;

            if (cartridge.Task.Status == TaskStatus.DONE)
            {
                // Work is deployed, cache cartridge object.
                gameManager.ObjectPool.PoolCartridge(cartridge);
                Sleep();
            }
        }
        else
        {
            Sleep(); // No cartridge.
        }
    }
}
