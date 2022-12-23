using UnityEngine;

/** An Interactable is a Selectable that requires a character to interact. **/
public class Interactable : Selectable
{

    void Start()
    {
        ContextManager.Instance.onCharacterSelected += Enable;
        ContextManager.Instance.onDeselect += Disable;
        Disable();
    }

    protected override void Select()
    {
        ContextManager.Instance.OnInteractableSelected(this);
        base.Select();
    }
}