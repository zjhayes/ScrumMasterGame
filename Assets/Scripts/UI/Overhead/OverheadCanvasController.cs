using UnityEngine;

public class OverheadCanvasController : CanvasController
{
    [SerializeField]
    GameObject idleSpeechBubblePrefab;
    [SerializeField]
    GameObject frustrationSpeechBubblePrefab;

    public SpeechBubble CreateIdleSpeechBubble()
    {
        GameObject idleSpeechBubble = Instantiate(idleSpeechBubblePrefab);
        idleSpeechBubble.transform.SetParent(transform); // Set parent to canvas.
        return idleSpeechBubble.GetComponent<SpeechBubble>();
    }

    public SpeechBubble CreateFrustrationSpeechBubble()
    {
        GameObject frustrationSpeechBubble = Instantiate(frustrationSpeechBubblePrefab);
        frustrationSpeechBubble.transform.SetParent(transform); // Set parent to canvas.
        return frustrationSpeechBubble.GetComponent<SpeechBubble>();
    }
}
