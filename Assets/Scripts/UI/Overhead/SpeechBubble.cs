using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    float time = 2.0f;

    Image graphic;
    OverheadController controller;

    void Awake()
    {
        graphic = GetComponent<Image>();
        Hide();
    }

    void Start()
    {
        // Destroy after given amount of time.
        Object.Destroy(gameObject, time);
        UpdatePosition();
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        // Set position to current selected character's UI overhead position.
        transform.position = controller.GetIconPosition();
    }

    public void Show(OverheadController _controller)
    {
        controller = _controller;
        graphic.enabled = true;
    }


    public void Hide()
    {
        graphic.enabled = false;
    }
}
