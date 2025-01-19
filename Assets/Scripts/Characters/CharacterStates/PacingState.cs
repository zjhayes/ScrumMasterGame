using UnityEngine;

public class PacingState : CharacterState
{
    private Coroutine waitAction;

    public PacingState(ICharacterController character, IGameManager gameManager) : base(character, gameManager){}

    public override void Enter()
    {
        character.Movement.OnArrivedAtDestination += WaitAfterPacing;
        OnBegin();
        base.Enter();
    }

    public override void Exit()
    {
        character.Movement.OnArrivedAtDestination -= WaitAfterPacing;
        CancelWait();
        base.Exit();
    }

    public override string Status
    {
        get { return "Pacing..."; }
    }

    protected virtual void OnBegin()
    {
        Pace();
    }

    protected virtual void WaitAfterPacing()
    {
        float delayTime = Random.Range(character.Properties.MinWaitTime, character.Properties.MaxWaitTime);
        waitAction = gameManager.Actions.StartDelayedAction(delayTime, AfterWait);
    }

    protected virtual void AfterWait()
    {
        Pace();
    }

    protected void Pace()
    {
        // Move to randomly position within pacing boundary.
        character.Movement.GoToBoundary(gameManager.Team.PaceBoundary, character.Movement.BaseSpeed * character.Properties.PaceSpeed);
    }

    private void CancelWait()
    {
        if(waitAction != null)
        {
            gameManager.Actions.StopCoroutine(waitAction);
            waitAction = null;
        }
    }
}
