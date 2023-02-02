using UnityEngine;

public abstract class GameState : GameBehaviour, IState<ContextManager>
{
    public abstract void Handle(ContextManager controller);

    public abstract void Escape();

    public virtual void Destroy()
    {
        Destroy(this);
    }
}
