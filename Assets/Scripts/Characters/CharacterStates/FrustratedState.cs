using System.Collections;
using UnityEngine;

public class FrustratedState : CharacterState
{
    [SerializeField]
    private float emoteTime = 2.0f;
    [SerializeField]
    private OverheadElement frustrationBubble;

    ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        this.character = controller;
        base.Handle(controller);
        frustrationBubble.Show();
        StartCoroutine(StopEmoteAfterDelay()); // Wait and then end frustration.
    }

    IEnumerator StopEmoteAfterDelay()
    {
        yield return new WaitForSeconds(emoteTime);
        frustrationBubble.Hide();
        character.FindSomethingToDo();
    }

    public override string Status
    {
        get { return "Frustrated"; }
    }
}