using UnityEngine;

public class NoSelectionState : MonoBehaviour, IState<ContextManager>
{
    private ContextManager controller;

    public void Handle(ContextManager _controller)
    {
        controller = _controller;
    }

    void Start()
    {
        if (!controller) { Debug.Log("No controller set on state."); }

        controller.CurrentCharacter = null;
        controller.DisableInteractables();
        UIManager.Instance.SelectedCharacterIcon.Hide();
        UIManager.Instance.CharacterCard.Hide();
    }

    public void Destroy()
    {
        Destroy(this);
    }
}