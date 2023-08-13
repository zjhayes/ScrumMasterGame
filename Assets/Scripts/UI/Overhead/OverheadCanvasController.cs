using UnityEngine;

public class OverheadCanvasController : CanvasController
{
    [SerializeField]
    private Transform speechBubbleParent;
    [SerializeField]
    private Transform stationProgressBarParent;
    [SerializeField]
    private GameObject idleSpeechBubblePrefab;
    [SerializeField]
    private GameObject frustrationSpeechBubblePrefab;
    [SerializeField]
    private GameObject stationProgressionBar;

    public SpeechBubble CreateIdleSpeechBubble()
    {
        GameObject idleSpeechBubble = Instantiate(idleSpeechBubblePrefab, speechBubbleParent);
        return idleSpeechBubble.GetComponent<SpeechBubble>();
    }
    
    public SpeechBubble CreateFrustrationSpeechBubble()
    {
        GameObject frustrationSpeechBubble = Instantiate(frustrationSpeechBubblePrefab, speechBubbleParent);
        return frustrationSpeechBubble.GetComponent<SpeechBubble>();
    }

    public ProgressBar CreateStationProgressionBar()
    {
        GameObject progressBar = Instantiate(stationProgressionBar, stationProgressBarParent);
        return progressBar.GetComponent<ProgressBar>();
    }
}
