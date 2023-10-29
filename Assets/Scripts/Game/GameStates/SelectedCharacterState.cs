using HierarchicalStateMachine;

public class SelectedCharacterState : GameState
{
    private ICharacterController selectedCharacter;

    public SelectedCharacterState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
        gameManager.Interactables.EnableInteractables();

        // Listen to character and update status when state changed.
        selectedCharacter = gameManager.Context.CurrentCharacter;
        selectedCharacter.StateContext.OnTransition += OnCharacterStateChange;

        // Show selected character details card.
        gameManager.UI.CharacterCard.UpdateCard(selectedCharacter);
        gameManager.UI.CharacterCard.Show();

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        // Stop listening to character.
        selectedCharacter.StateContext.OnTransition -= OnCharacterStateChange;
        selectedCharacter.Deselect();

        // Revert state.
        gameManager.Interactables.DisableInteractables();
        gameManager.UI.CharacterCard.Hide();
    }

    private void OnCharacterStateChange()
    {
        gameManager.UI.CharacterCard.UpdateStatus(selectedCharacter);
    }
}