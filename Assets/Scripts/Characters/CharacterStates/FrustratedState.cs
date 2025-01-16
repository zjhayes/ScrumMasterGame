using System.Collections;
using UnityEngine;

public class FrustratedState : CharacterState
{
    Coroutine frustractionAction;

    public FrustratedState(ICharacterController character, IGameManager gameManager) : base(character, gameManager) {}

    public override void Enter()
    {
        character.Properties.FrustrationBubble.Show();
        frustractionAction = gameManager.Actions.StartDelayedAction(character.Properties.EmoteTime, EndFrustration); // Wait and then end frustration.
        base.Enter();
    }

    public override void Exit()
    {
        character.Properties.FrustrationBubble.Hide();
        CancelWaitAndFindSomethingToDo();

        base.Exit();
    }

    public override string Status
    {
        get { return "Frustrated"; }
    }

    private void EndFrustration()
    {
        character.FindSomethingToDo();
    }

    private void CancelWaitAndFindSomethingToDo()
    {
        if (frustractionAction != null)
        {
            gameManager.Actions.StopCoroutine(frustractionAction);
            frustractionAction = null;
        }
    }
}