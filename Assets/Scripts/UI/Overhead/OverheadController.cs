using UnityEngine;

/** A character's Overhead is where icons, emotes and progress indicators will appear. */
public class OverheadController : GameBehaviour
{
    [SerializeField]
    private Transform overheadLocation;

    private SpeechBubble idleSpeechBubble;
    private SpeechBubble frustrationSpeechBubble;

    private void Start()
    {
        // Set up character emotes.
        idleSpeechBubble = gameManager.UI.OverheadCanvas.CreateIdleSpeechBubble();
        frustrationSpeechBubble = gameManager.UI.OverheadCanvas.CreateFrustrationSpeechBubble();
        idleSpeechBubble.AssignController(this);
        frustrationSpeechBubble.AssignController(this);
    }

    public Vector3 GetIconPosition()
    {
        return Camera.main.WorldToScreenPoint(overheadLocation.position);
    }

    public void ShowIdleBubble()
    {
        idleSpeechBubble.Show();
    }

    public void ShowFrustrationBubble()
    {
        frustrationSpeechBubble.Show();
    }

    public void HideIdleBubble()
    {
        idleSpeechBubble.Hide();
    }

    public void HideFrustrationBubble()
    {
        frustrationSpeechBubble.Hide();
    }

    public void HideAll()
    {
        HideIdleBubble();
        HideFrustrationBubble();
    }

    public bool HasSpeechBubble()
    {
        return idleSpeechBubble.IsShowing() || frustrationSpeechBubble.IsShowing();
    }
}
