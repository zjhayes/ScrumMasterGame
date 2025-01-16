using UnityEngine;
// Game Service Locator
[RequireComponent(typeof(IContextManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(InteractableManager))]
[RequireComponent(typeof(PlayerControls))]
[RequireComponent(typeof(SprintManager))]
[RequireComponent(typeof(BoardManager))]
[RequireComponent(typeof(TeamManager))]
[RequireComponent(typeof(ProductionManager))]
public class GameManager : MonoBehaviour, IGameManager
{
    UIManager ui;
    PlayerControls controls;
    SprintManager sprint;
    BoardManager board;
    ContextManager context;
    TeamManager teamManager;
    ProductionManager productionManager;
    InteractableManager interactables;
    ObjectPoolController objectPool;
    CameraController cameraController;

    void Awake()
    {
        // Inject gameManager into dependents.
        ServiceInjector.Resolve<IGameManager, GameBehaviour>(this);

        context = GetComponent<ContextManager>();
        ui = GetComponent<UIManager>();
        interactables = GetComponent<InteractableManager>();
        controls = GetComponent<PlayerControls>();
        sprint = GetComponent<SprintManager>();
        board = GetComponent<BoardManager>();
        teamManager = GetComponent<TeamManager>();
        productionManager = GetComponent<ProductionManager>();

        objectPool = FindObjectOfType<ObjectPoolController>();
        cameraController = FindObjectOfType<CameraController>();
    }

    public ContextManager Context
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

    public ProductionManager Production
    {
        get { return productionManager; }
    }

    public ObjectPoolController ObjectPool
    {
        get { return objectPool; }
    }

    public CameraController Camera
    {
        get { return cameraController; }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
