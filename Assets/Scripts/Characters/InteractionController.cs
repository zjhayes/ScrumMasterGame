using UnityEngine;

[RequireComponent(typeof(Awareness))]
[RequireComponent(typeof(CharacterController))]
public class InteractionController : MonoBehaviour
{
    private Awareness awareness;
    private Interactable currentTarget;

    public event Events.CharacterEvent OnInteract;

    void Awake()
    {
        awareness = GetComponent<Awareness>();
    }

    void Start()
    {
        awareness.onAwarenessChanged += UpdateTarget;
    }

    public void Interact()
    {
        OnInteract?.Invoke();
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
