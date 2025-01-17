using System.Collections.Generic;
using UnityEngine;

public class TeamManager : GameBehaviour
{
    [SerializeField]
    List<CharacterController> characters;
    [SerializeField]
    private Boundary paceBoundary;


    void Start()
    {
        gameManager.Sprint.OnBeginPlanning += RallyAtScrumBoard;
        gameManager.Sprint.OnBeginSprint += Scrum;
    }

    void RallyAtScrumBoard()
    {
        // TODO: Have character huddle around scrum board.
    }

    void Scrum()
    {
        foreach(CharacterController character in characters)
        {
            character.FindSomethingToDo();
        }
    }

    void OnDisable()
    {
        gameManager.Sprint.OnBeginPlanning -= RallyAtScrumBoard;
        gameManager.Sprint.OnBeginSprint -= Scrum;
    }

    public List<CharacterController> Characters
    {
        get { return characters; }
    }

    public Boundary PaceBoundary
    {
        get { return paceBoundary; }
        set { paceBoundary = value; }
    }
}
