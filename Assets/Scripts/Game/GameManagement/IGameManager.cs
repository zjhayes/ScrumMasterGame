using UnityEngine;

public interface IGameManager : IService
{
    public UIManager UI { get; }

    public PlayerControls Controls { get; }

    public SprintManager Sprint { get; }

    public BoardManager Board { get; }

    public IContextManager Context { get; }

    public TeamManager Team { get; }

    public ProductionManager Production { get; }

    public InteractableManager Interactables { get; }

    public ObjectPoolController ObjectPool { get; }

    public CameraController Camera { get; }

    public void Quit();
}
