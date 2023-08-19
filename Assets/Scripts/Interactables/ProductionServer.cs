
public class ProductionServer : Computer
{
    public delegate void OnDeploymentComplete();
    public event OnDeploymentComplete onDeploymentComplete;

    protected override void IterateWork()
    {
        if(cartridgeReceptacle.TryGetPickup(out Cartridge cartridge))
        {
            cartridge.Task.Status = TaskStatus.DONE;

            if (cartridge.Task.Status == TaskStatus.DONE)
            {
                // Work is deployed, cache cartridge object.
                gameManager.ObjectPool.PoolCartridge(cartridge);
                onDeploymentComplete?.Invoke();
                Sleep();
            }
        }
        else
        {
            Sleep(); // No cartridge.
        }
    }


}
