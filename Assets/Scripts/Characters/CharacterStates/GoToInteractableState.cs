/* This character state directs a character to an interactable, and interacts on arrival. */
public class GoToInteractableState : CharacterState
{
    protected ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        character.Movement.GoTo(character.TargetInteractable.Position);
        character.Movement.OnArrivedAtDestination += character.InteractWithTarget;
        base.Handle(controller);
    }

    void Update()
    {
        if(character.TargetInteractable == null || !character.TargetInteractable.isActiveAndEnabled)
        {
            // No target interactable, do something else.
            character.FindSomethingToDo();
            return;
        }
    }

    public override void Exit()
    {
        character.Movement.OnArrivedAtDestination -= character.InteractWithTarget;
        base.Exit();
    }

    public override string Status 
    { 
        get { return "Walking"; }
    }
}