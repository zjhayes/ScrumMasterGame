using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpeechBubble : MonoBehaviour
{
    private Image graphic;
    private OverheadController controller;

    private void Awake()
    {
        graphic = GetComponent<Image>();
        Hide();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // Set position to current selected character's UI overhead position.
        transform.position = controller.GetIconPosition();
    }

    public void AssignController(OverheadController controller)
    {
        this.controller = controller;
    }

    public void Show()
    {
        UpdatePosition();
        graphic.enabled = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        graphic.enabled = false;
        gameObject.SetActive(false);
    }

    public bool IsShowing()
    {
        return graphic.enabled;
    }
}
