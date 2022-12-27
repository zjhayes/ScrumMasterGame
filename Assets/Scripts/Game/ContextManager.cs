using UnityEngine;
using UnityEngine.EventSystems;

public class ContextManager : Singleton<ContextManager>, IController
{
    public delegate void OnEnableInteractables();
    public OnEnableInteractables onEnableInteractables;

    public delegate void OnDisableInteractables();
    public OnDisableInteractables onDisableInteractables;

    StateContext<ContextManager> stateContext;
    CharacterController currentCharacter;

    void Awake()
    {
        stateContext = new StateContext<ContextManager>(this);
        Deselect();
    }

    public void CharacterSelected(CharacterController character)
    {
        currentCharacter = character;
        stateContext.Transition<SelectedCharacterState>();
    }

    public void Deselect()
    {
        stateContext.Transition<NoSelectionState>();
    }

    public CharacterController CurrentCharacter
    {
        get { return currentCharacter; }
        set { currentCharacter = value; }
    }

    public void EnableInteractables()
    {
        onEnableInteractables?.Invoke();
    }

    public void DisableInteractables()
    {
        onDisableInteractables.Invoke();
    }
}
