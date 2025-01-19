using UnityEngine;

[RequireComponent(typeof(ProductionStats))]
public class ProductionServer : Computer
{
    public Events.GameEvent OnDeployed;

    protected override void IterateWork()
    {
        if(cartridgeReceptacle.TryGet(out Cartridge cartridge))
        {
            ResolveStory(cartridge.Story);

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

    private void ResolveStory(Story story)
    {
        story.Status = StoryStatus.DONE;
        story.Outcome.EndTime = gameManager.Sprint.Clock.CurrentTime;
    }
}
