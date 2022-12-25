using UnityEngine;

public class InteractionState : MonoBehaviour, IState<CharacterController>
{
    private CharacterController character;

    public void Handle(CharacterController _controller)
    {
        character = _controller;
    }

    void Start()
    {
        if (!character) { Debug.Log("No controller set on state."); }

        if (!character.CurrentInteractable)
        {
            character.Idle();
        }
        else
        {
            character.CurrentInteractable.InteractWith(character);
        }
    }

    public void Destroy()
    {
        Destroy(this);
    }
}