using UnityEngine;

public class SprintInformation : MenuController
{
    [SerializeField]
    Vector2 shrunkenSize;
    [SerializeField]
    CanvasController menu;

    RectTransform rectTransform;
    Vector2 expandedSize;

    void SetUp()
    {
        rectTransform = GetComponent<RectTransform>();
        expandedSize = rectTransform.sizeDelta;
    }

    public void Shrink()
    {
        rectTransform.sizeDelta = shrunkenSize;
    }

    public void Expand()
    {
        rectTransform.sizeDelta = expandedSize;
    }
}
