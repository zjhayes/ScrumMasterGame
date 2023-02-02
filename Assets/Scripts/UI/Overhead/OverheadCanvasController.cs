using UnityEngine;

public class OverheadCanvasController : CanvasController
{
    [SerializeField]
    GameObject frustrationSpeechBubblePrefab;

    void OnEnable()
    {
        gameManager.UI.onShowFrustrationEmote += CreateFrustrationSpeechBubble;
    }

    public void CreateFrustrationSpeechBubble(OverheadController controller)
    {
        GameObject frustrationSpeechBubble = Instantiate(frustrationSpeechBubblePrefab);
        frustrationSpeechBubble.transform.SetParent(transform);
        frustrationSpeechBubble.GetComponent<SpeechBubble>().Show(controller);
    }

    void OnDisable()
    {
        gameManager.UI.onShowFrustrationEmote -= CreateFrustrationSpeechBubble;
    }
}
