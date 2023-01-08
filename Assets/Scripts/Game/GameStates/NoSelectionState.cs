using UnityEngine;

public class NoSelectionState : GameState
{ 
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.CurrentCharacter = null;
        controller.DisableInteractables();
        UIManager.Instance.SelectedCharacterIcon.Hide();
        UIManager.Instance.CharacterCard.Hide();
    }
}