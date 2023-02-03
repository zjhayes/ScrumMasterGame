using UnityEngine;

public class CharacterContext : MonoBehaviour
{
    [SerializeField]
    IState<CharacterController> idleState;
    [SerializeField]
    IState<CharacterController> goToInteractableState;
    [SerializeField]
    IState<CharacterController> interactionState;
}
