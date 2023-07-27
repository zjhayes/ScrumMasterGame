using UnityEngine;

public class ProductionServer : Computer
{
    public delegate void OnDeploymentComplete();
    public event OnDeploymentComplete onDeploymentComplete;

    protected override void IterateWork()
    {
        // TODO: Calculate deployment outcome.

        // Release production updates.
        task.Status = TaskStatus.DONE;
        
        if(task.Status == TaskStatus.DONE)
        {
            Cartridge cartridge = this.CurrentCartridge;
            gameManager.ObjectPool.PoolCartridge(this.CurrentCartridge);
            onDeploymentComplete?.Invoke();
            this.Sleep();
        }
    }
}
