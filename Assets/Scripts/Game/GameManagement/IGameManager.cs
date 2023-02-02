using UnityEngine;

public interface IGameManager : IService
{
    public UIManager UI { get; }

    public PlayerControls Controls { get; }

    public SprintManager Sprint { get; }

    public ContextManager Context { get; }

    public CameraController Camera { get; }
}
