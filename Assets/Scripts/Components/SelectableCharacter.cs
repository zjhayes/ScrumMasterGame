using UnityEngine;

public class SelectableCharacter : Selectable
{
    protected override void Select()
    {
        ContextManager.Instance.SwitchToCharacterContext(this);
        base.Select();
    }

    protected override void HoverEnter()
    {
        base.HoverEnter();
    }

    protected override void HoverExit()
    {
        base.HoverExit();
    }
}
