/* This character state directs a character to an interactable, and interacts on arrival. */
public class GoToInteractableState : CharacterState
{

    public GoToInteractableState(ICharacterController character, IGameManager gameManager) : base(character, gameManager) {}

    public override void Enter()
    {
        character.Movement.GoTo(character.TargetInteractable.Position);
        character.Movement.OnArrivedAtDestination += character.InteractWithTarget;
        base.Enter();
    }

    public override void Update()
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