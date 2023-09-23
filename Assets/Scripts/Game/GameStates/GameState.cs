using UnityEngine;

public abstract class GameState : GameBehaviour, IState<ContextManager>
{
    public virtual void Handle(ContextManager controller)
    {
        this.enabled = true;
    }

    public abstract void OnEscaped();

    public virtual void ChangeView()
    {
        // Override if view can be changed during state.
    }

    public virtual void Exit()
    {
        this.enabled = false;
    }
}
