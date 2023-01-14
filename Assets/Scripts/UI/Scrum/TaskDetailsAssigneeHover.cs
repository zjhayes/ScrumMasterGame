using UnityEngine;
using UnityEngine.EventSystems;

public class TaskDetailsAssigneeHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TaskDetailsPanel taskDetailsPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        int itemIndex = transform.GetSiblingIndex() - 1; // One less than sibling.
        taskDetailsPanel.UpdateModifiers(itemIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        taskDetailsPanel.ClearModifiers();
    }

}
