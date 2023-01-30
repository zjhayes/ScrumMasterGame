using UnityEngine;
// Game Service Locator
[RequireComponent(typeof(PlayerControls))]
[RequireComponent(typeof(SprintManager))]
public class GameManager : Singleton<GameManager>
{
    UIManager ui;
    PlayerControls controls;
    SprintManager sprint;

    protected override void Awake()
    {
        controls = GetComponent<PlayerControls>();
        sprint = GetComponent<SprintManager>();

        // Keep alive for duration of game.
        DontDestroyOnLoad(gameObject);
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
}
