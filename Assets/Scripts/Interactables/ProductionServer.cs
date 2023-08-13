
public class ProductionServer : Computer
{
    public delegate void OnDeploymentComplete();
    public event OnDeploymentComplete onDeploymentComplete;

    protected override void IterateWork()
    {
        // TODO: Calculate deployment outcome.

        // Deploy production updates.
        cartridge.Task.Status = TaskStatus.DONE;
        
        if(cartridge.Task.Status == TaskStatus.DONE)
        {
            cartridge.ClaimedBy = null;
            gameManager.ObjectPool.PoolCartridge(cartridge);
            onDeploymentComplete?.Invoke();
            Sleep();
        }
    }


}
