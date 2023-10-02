using System.Collections;
using UnityEngine;

public class FrustratedState : CharacterState
{
    [SerializeField]
    private float emoteTime = 2.0f;
    [SerializeField]
    private OverheadElement frustrationBubble;

    Coroutine waitAndFindSomethingToDoAction;

    ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        this.character = controller;
        base.Handle(controller);
        frustrationBubble.Show();
        waitAndFindSomethingToDoAction = StartCoroutine(WaitAndFindSomethingToDo()); // Wait and then end frustration.
    }

    public override void Exit()
    {
        frustrationBubble.Hide();
        CancelWaitAndFindSomethingToDo();

        base.Exit();
    }

    public override string Status
    {
        get { return "Frustrated"; }
    }

    private IEnumerator WaitAndFindSomethingToDo()
    {
        yield return new WaitForSeconds(emoteTime);

        character.FindSomethingToDo();
    }

    private void CancelWaitAndFindSomethingToDo()
    {
        if (waitAndFindSomethingToDoAction != null)
        {
            StopCoroutine(waitAndFindSomethingToDoAction);
            waitAndFindSomethingToDoAction = null;
        }
    }
}