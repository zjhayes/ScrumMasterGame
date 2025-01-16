using HierarchicalStateMachine;
using System.Collections.Generic;

public class GameContext : StateMachineContext<GameState, GameStates>
{
    public GameContext(IGameManager gameManager, GameStates defaultState = GameStates.SETUP)
        : base(new Dictionary<GameStates, GameState>
        {
            { GameStates.SETUP,  new SetupState(gameManager) },
            { GameStates.PLANNING, new PlanningState(gameManager) },
            { GameStates.SCRUM, new ScrumState(gameManager) },
            { GameStates.DEFAULT_VIEW, new DefaultViewState(gameManager) },
            { GameStates.RELEASE, new ReleaseState(gameManager) },
            { GameStates.RETROSPECTIVE, new RetrospectiveState(gameManager) },
            { GameStates.BOARD_VIEW, new BoardViewState(gameManager) },
            { GameStates.SELECTED_CHARACTER, new SelectedCharacterState(gameManager) },
            { GameStates.STATIC, new StaticGameState(gameManager) }
        }, defaultState)
    {
    }
}