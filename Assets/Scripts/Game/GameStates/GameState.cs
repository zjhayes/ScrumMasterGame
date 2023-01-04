using UnityEngine;

public abstract class GameState : MonoBehaviour, IState<ContextManager>
{
    public abstract void Handle(ContextManager controller);

    public void Destroy()
    {
        Destroy(this);
    }
}
