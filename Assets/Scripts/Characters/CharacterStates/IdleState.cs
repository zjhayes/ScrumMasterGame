using UnityEngine;

public class IdleState : MonoBehaviour, IState<CharacterController>
{
    private CharacterController character;

    public void Handle(CharacterController _controller)
    {
        character = _controller;
    }

    void Start()
    {
        if (!character) { Debug.Log("No controller set on state."); }
    }

    public void Destroy()
    {
        Destroy(this);
    }
}