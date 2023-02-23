using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour, IBehaviour<IGameManager>
{
    protected IGameManager gameManager;

    public void Inject(IGameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public IGameManager GameManager
    {
        get { return gameManager; }
    }
}
