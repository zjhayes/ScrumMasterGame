using UnityEngine;

public abstract class GameState : MonoBehaviour, IState<ContextManager>
{
    public abstract void Handle(ContextManager controller);

    public abstract void Escape();

    public virtual void Destroy()
    {
        Destroy(this);
    }
}
