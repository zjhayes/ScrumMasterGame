using UnityEngine;

public class ProductionServer : Computer
{
    public delegate void OnDeploymentComplete();
    public event OnDeploymentComplete onDeploymentComplete;

    protected override void IterateWork()
    {
        // TODO: Calculate deployment outcome.

        // Deploy production updates.
        task.Status = TaskStatus.DONE;
        
        if(task.Status == TaskStatus.DONE && TryGetCartridge(out Cartridge cartridge))
        {
            gameManager.ObjectPool.PoolCartridge(cartridge);
            onDeploymentComplete?.Invoke();
            this.Sleep();
        }
    }
}
