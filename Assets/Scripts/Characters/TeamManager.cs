using System.Collections.Generic;
using UnityEngine;

public class TeamManager : GameBehaviour
{
    [SerializeField]
    private List<CharacterController> characters;
    [SerializeField]
    private Boundary paceBoundary;


    private void Start()
    {
        gameManager.Sprint.OnBeginRetrospective += RallyTeam;
        gameManager.Sprint.OnBeginSprint += Scrum;
    }

    public void RallyTeam()
    {
        foreach(CharacterController character in characters)
        {
            character.Rally();
        }
    }

    public void Scrum()
    {
        foreach(CharacterController character in characters)
        {
            character.FindSomethingToDo();
        }
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
