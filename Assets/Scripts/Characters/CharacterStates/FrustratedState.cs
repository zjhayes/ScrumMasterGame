using System.Collections;
using UnityEngine;

public class FrustratedState : CharacterState
{
    [SerializeField]
    private float emoteTime = 2.0f;
    [SerializeField]
    private OverheadElement frustrationBubble;

    Coroutine waitAndFindSomethingToDoAction;

    public FrustratedState(ICharacterController character, IGameManager gameManager) : base(character, gameManager) {}

    public override void Enter()
    {
        frustrationBubble.Show();
        //waitAndFindSomethingToDoAction = StartCoroutine(WaitAndFindSomethingToDo()); // Wait and then end frustration.
        base.Enter();
        Debug.Log("Fix");
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
            //StopCoroutine(waitAndFindSomethingToDoAction);
            Debug.Log("Fix");
            waitAndFindSomethingToDoAction = null;
        }
    }
}