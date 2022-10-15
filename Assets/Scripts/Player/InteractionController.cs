using UnityEngine;

[RequireComponent(typeof(Awareness))]
[RequireComponent(typeof(CharacterController))]
public class InteractionController : MonoBehaviour
{
    private CharacterController controller;
    private Awareness awareness;
    private Interactable currentTarget;

    public delegate void OnInteract();
    public OnInteract onInteract;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        awareness = GetComponent<Awareness>();
    }

    void Start()
    {
        awareness.onAwarenessChanged += UpdateTarget;
    }

    public void Interact()
    {
        onInteract?.Invoke();
        currentTarget?.Interact(controller);
    }

    private void UpdateTarget(GameObject target)
    {
        if (target?.tag == Tags.INTERACTABLE)
        {
            currentTarget = target.GetComponent<Interactable>();
        }
        else
        {
            currentTarget = null;
        }
    }

    public Interactable Target
    {
        get { return currentTarget; }
        set { currentTarget = value; }
    }
}
