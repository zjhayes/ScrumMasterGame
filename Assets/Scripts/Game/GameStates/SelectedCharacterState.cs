using UnityEngine;

public class SelectedCharacterState : MonoBehaviour, IState<ContextManager>
{
    private ContextManager controller;

    public void Handle(ContextManager _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        if (!controller) { Debug.Log("No controller set on state."); }

        if(controller.CurrentCharacter.Status == CharacterStatus.IDLE)
        {
            controller.EnableInteractables();
        }
        
        UIManager.Instance.SelectedCharacterIcon.Show();
    }

    public void Destroy()
    {
        Destroy(this);
    }
}