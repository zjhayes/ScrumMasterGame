using UnityEngine;

public interface IGameManager : IService
{
    public UIManager UI { get; }

    public PlayerControls Controls { get; }

    public SprintManager Sprint { get; }

    public IContextManager Context { get; }

    public CharacterManager Team { get; }

    public CameraController Camera { get; }
}
