using UnityEngine;

public class OverheadCanvasController : CanvasController
{
    [SerializeField]
    private Transform speechBubbleParent;
    [SerializeField]
    private GameObject idleSpeechBubblePrefab;
    [SerializeField]
    private GameObject frustrationSpeechBubblePrefab;

    public SpeechBubble CreateIdleSpeechBubble()
    {
        GameObject idleSpeechBubble = Instantiate(idleSpeechBubblePrefab);
        idleSpeechBubble.transform.SetParent(speechBubbleParent); // Set parent to canvas.
        return idleSpeechBubble.GetComponent<SpeechBubble>();
    }

    public SpeechBubble CreateFrustrationSpeechBubble()
    {
        GameObject frustrationSpeechBubble = Instantiate(frustrationSpeechBubblePrefab);
        frustrationSpeechBubble.transform.SetParent(speechBubbleParent); // Set parent to canvas.
        return frustrationSpeechBubble.GetComponent<SpeechBubble>();
    }
}
