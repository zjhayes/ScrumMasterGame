
public class ProductionServer : Computer
{
    public event Events.GameEvent OnDeploymentComplete;

    protected override void IterateWork()
    {
        if(cartridgeReceptacle.TryGetPickup(out Cartridge cartridge))
        {
            cartridge.Task.Status = TaskStatus.DONE;

            if (cartridge.Task.Status == TaskStatus.DONE)
            {
                // Work is deployed, cache cartridge object.
                gameManager.ObjectPool.PoolCartridge(cartridge);
                OnDeploymentComplete?.Invoke();
                Sleep();
            }
        }
        else
        {
            Sleep(); // No cartridge.
        }
    }


}
