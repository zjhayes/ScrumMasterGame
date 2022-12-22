using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    [Multiline()]
    private string content;
    [SerializeField]
    private string header;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    private void Show()
    {
        TooltipSystem.Show(content, header);
    }

    private void Hide()
    {
        TooltipSystem.Hide();
    }
}
