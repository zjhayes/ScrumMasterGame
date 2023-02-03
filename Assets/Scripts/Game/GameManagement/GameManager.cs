using UnityEngine;
// Game Service Locator
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(PlayerControls))]
[RequireComponent(typeof(SprintManager))]
[RequireComponent(typeof(IContextManager))]
public class GameManager : MonoBehaviour, IGameManager
{
    UIManager ui;
    PlayerControls controls;
    SprintManager sprint;
    IContextManager context;
    CharacterManager characterManager;
    CameraController cameraController;

    void Awake()
    {
        // Inject gameManager into dependents.
        ServiceInjector.Resolve<IGameManager, GameBehaviour>(this);

        ui = GetComponent<UIManager>();
        controls = GetComponent<PlayerControls>();
        sprint = GetComponent<SprintManager>();
        context = GetComponent<IContextManager>();
        characterManager = GetComponent<CharacterManager>();

        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    public UIManager UI
    {
        get { return ui; }
        set { ui = value; }
    }

    public PlayerControls Controls
    {
        get { return controls; }
    }

    public SprintManager Sprint
    {
        get { return sprint; } 
    }

    public IContextManager Context
    {
        get { return context; }
    }

    public CharacterManager Team
    {
        get { return characterManager; }
    }

    public CameraController Camera
    {
        get { return cameraController; }
    }
}
