using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    Canvas overheadCanvas;
    [SerializeField]
    SelectedIcon selectedCharacterIcon;
    [SerializeField]
    GameObject frustrationSpeechBubblePrefab;

    public Canvas OverheadCanvas
    {
        get { return overheadCanvas; }
    }

    public SelectedIcon SelectedCharacterIcon
    {
        get { return selectedCharacterIcon; }
    }

    public void CreateFrustrationSpeechBubble(OverheadController controller)
    {
        GameObject frustrationSpeechBubble = Instantiate(frustrationSpeechBubblePrefab);
        frustrationSpeechBubble.transform.SetParent(overheadCanvas.transform);
        //frustrationSpeechBubble.transform.parent = overheadCanvas.transform;
        frustrationSpeechBubble.GetComponent<SpeechBubble>().Show(controller);
    }

    public GameObject FrustrationSpeechBubblePrefab
    {
        get { return frustrationSpeechBubblePrefab; }
    }
}
