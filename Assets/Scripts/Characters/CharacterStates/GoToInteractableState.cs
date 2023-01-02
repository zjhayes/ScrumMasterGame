using UnityEngine;

public class GoToInteractableState : MonoBehaviour, IState<CharacterController>
{
    private CharacterController character;

    public void Handle(CharacterController controller)
    {
        character = controller;
        character.Movement.GoTo(character.CurrentInteractable.Position);
    }

    void Start()
    {
        if (!character) { Debug.Log("No controller set on state."); }
    }

    void Update()
    {
        if(!character.CurrentInteractable)
        {
            character.Idle();
            return;
        }

        if(character.Movement.AtDestination())
        {
            character.InteractWithCurrent();
        }
    }

    public void Destroy()
    {
        Destroy(this);
    }
}