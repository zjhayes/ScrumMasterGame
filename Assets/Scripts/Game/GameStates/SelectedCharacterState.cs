using HierarchicalStateMachine;

public class SelectedCharacterState : GameState
{
    private ICharacterController selectedCharacter;

    public SelectedCharacterState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
        gameManager.Interactables.EnableInteractables();

        // Listen to character and update when state and stats change.
        selectedCharacter = gameManager.Context.CurrentCharacter;
        selectedCharacter.Context.OnTransition += OnCharacterStateTransition;
        selectedCharacter.Stats.OnStatUpdated += OnCharacterStatsUpdated;

        // Show selected character details card.
        gameManager.UI.CharacterCard.UpdateCard(selectedCharacter);
        gameManager.UI.CharacterCard.Show();

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        // Stop listening to character.
        selectedCharacter.Context.OnTransition -= OnCharacterStateTransition;
        selectedCharacter.Stats.OnStatUpdated -= OnCharacterStatsUpdated;
        selectedCharacter.Deselect();

        // Revert state.
        gameManager.Interactables.DisableInteractables();
        gameManager.UI.CharacterCard.Hide();
    }

    private void OnCharacterStateTransition()
    {
        gameManager.UI.CharacterCard.UpdateStatus(selectedCharacter);
    }

    private void OnCharacterStatsUpdated(CharacterStat stat)
    {
        gameManager.UI.CharacterCard.UpdateProgress(selectedCharacter);
    }
}