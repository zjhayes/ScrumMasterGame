using UnityEngine;
// Game Service Locator
[RequireComponent(typeof(IContextManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(InteractableManager))]
[RequireComponent(typeof(PlayerControls))]
[RequireComponent(typeof(SprintManager))]
[RequireComponent(typeof(BoardManager))]
[RequireComponent(typeof(TeamManager))]
public class GameManager : MonoBehaviour, IGameManager
{
    UIManager ui;
    PlayerControls controls;
    SprintManager sprint;
    BoardManager board;
    IContextManager context;
    TeamManager teamManager;
    InteractableManager interactables;
    CameraController cameraController;

    void Awake()
    {
        // Inject gameManager into dependents.
        ServiceInjector.Resolve<IGameManager, GameBehaviour>(this);

        context = GetComponent<IContextManager>();
        ui = GetComponent<UIManager>();
        interactables = GetComponent<InteractableManager>();
        controls = GetComponent<PlayerControls>();
        sprint = GetComponent<SprintManager>();
        board = GetComponent<BoardManager>();
        teamManager = GetComponent<TeamManager>();

        cameraController = FindObjectOfType<CameraController>();
    }

    public IContextManager Context
    {
        get { return context; }
    }

    public UIManager UI
    {
        get { return ui; }
        set { ui = value; }
    }

    public InteractableManager Interactables
    {
        get { return interactables; }
    }

    public PlayerControls Controls
    {
        get { return controls; }
    }

    public SprintManager Sprint
    {
        get { return sprint; } 
    }

    public BoardManager Board
    {
        get { return board; }
    }

    public TeamManager Team
    {
        get { return teamManager; }
    }

    public CameraController Camera
    {
        get { return cameraController; }
    }
}
