using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //ContextManager.OnLeftClick(this, eventData);
        Debug.Log(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        Debug.Log("Hello - Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        Debug.Log("Hello - Mouse Exit");
    }

}
