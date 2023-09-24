/* This character state directs a character to an interactable, and interacts on arrival. */
public class GoToInteractableState : CharacterState
{
    protected ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        character.Movement.GoTo(character.TargetInteractable.Position);
        base.Handle(controller);
    }

    void Update()
    {
        if(character.TargetInteractable == null)
        {
            // No target interactable, do something else.
            character.FindSomethingToDo();
            return;
        }

        if(character.Movement.AtDestination())
        {
            // On arrival, interact.
            character.InteractWithTarget();
        }
    }

    public override string Status 
    { 
        get { return "Walking"; }
    }
}