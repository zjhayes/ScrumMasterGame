using HierarchicalStateMachine;
using System.Collections.Generic;

public class CharacterContext : StateMachineContext<CharacterState, CharacterStates>
{
    public CharacterContext(ICharacterController character, IGameManager gameManager, CharacterStates defaultState = CharacterStates.IDLE)
        : base(new Dictionary<CharacterStates, CharacterState>
        {
            { CharacterStates.IDLE,  new IdleState(character, gameManager) },
            { CharacterStates.FIND_SOMETHING_TO_DO,  new FindSomethingToDoState(character, gameManager) },
            { CharacterStates.FRUSTRATED,  new FrustratedState(character, gameManager) },
            { CharacterStates.GO_TO_INTERACTABLE,  new GoToInteractableState(character, gameManager) },
            { CharacterStates.INTERACT,  new InteractionState(character, gameManager) },
        }, defaultState)
    {
    }

}
