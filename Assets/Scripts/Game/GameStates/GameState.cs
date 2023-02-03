using UnityEngine;

public abstract class GameState : GameBehaviour, IState<ContextManager>
{
    public virtual void Handle(ContextManager controller)
    {
        this.enabled = true;
    }

    public abstract void Escape();

    public virtual void Destroy()
    {
        this.enabled = false;
    }
}
