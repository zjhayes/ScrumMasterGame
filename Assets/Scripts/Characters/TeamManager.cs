using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : GameBehaviour
{
    [SerializeField]
    List<CharacterController> characters;

    void OnEnable()
    {
        gameManager.Sprint.onBeginPlanning += RallyAtScrumBoard;
        gameManager.Sprint.onBeginSprint += Scrum;
    }

    void RallyAtScrumBoard()
    {

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
        gameManager.Sprint.onBeginPlanning -= RallyAtScrumBoard;
        gameManager.Sprint.onBeginSprint -= Scrum;
    }

    public List<CharacterController> Characters
    {
        get { return characters; }
    }
}
