using UnityEngine;
using UnityEngine.EventSystems;

public class ContextManager : Singleton<ContextManager>, IController
{
    [SerializeField]
    CameraController camera;

    public delegate void OnEnableInteractables();
    public OnEnableInteractables onEnableInteractables;

    public delegate void OnDisableInteractables();
    public OnDisableInteractables onDisableInteractables;

    StateContext<ContextManager> stateContext;
    CharacterController currentCharacter;

    protected override void Awake()
    {
        base.Awake();
        stateContext = new StateContext<ContextManager>(this);
    }

    void Start()
    {
        Deselect();
        // Deselect all when player hits Escape, and when sprint ends.
        PlayerControls.Instance.onEscape += Deselect;
        SprintManager.Instance.onBeginRetrospective += Deselect;
    }

    public void CharacterSelected(CharacterController character)
    {
        currentCharacter = character;
        camera.SwitchToFollowCamera(character.gameObject.transform);
        stateContext.Transition<SelectedCharacterState>();
    }

    public void Deselect()
    {
        camera.SwitchToOverworldCamera();
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
        onDisableInteractables?.Invoke();
    }
}
