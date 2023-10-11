using System.Collections.Generic;
using UnityEngine;

public class SetupState : GameState
{
    [SerializeField]
    private List<StoryDetails> starterStories;
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;
        base.Handle(controller);
        
        // Initialize game.
        gameManager.Board.ImportStoryDetails(starterStories);
        gameManager.Sprint.BeginPlanning();
    }

    public override void OnEscaped()
    {
        // Do nothing, cannot escape.
    }

    public override void Exit()
    {
        base.Exit();
    }
}
