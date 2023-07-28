using System.Collections;
using UnityEngine;

public class FrustratedState : CharacterState
{
    [SerializeField]
    private float emoteTime = 2.0f;

    ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        this.character = controller;
        base.Handle(controller);

        // Show frustration emote and find something else to do.
        if(!character.OverHead.HasSpeechBubble())
        {
            character.OverHead.ShowFrustrationBubble();
            StartCoroutine(StopEmoteAfterDelay()); // Wait and then hide frustration.
        }
    }

    IEnumerator StopEmoteAfterDelay()
    {
        yield return new WaitForSeconds(emoteTime);

        character.OverHead.HideFrustrationBubble();
        character.FindSomethingToDo();
    }

    public override string Status
    {
        get { return "Frustrated"; }
    }
}