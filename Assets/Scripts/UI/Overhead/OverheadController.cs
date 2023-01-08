using UnityEngine;

/** A character's Overhead is where icons, emotes and progress indicators will appear. */
public class OverheadController : MonoBehaviour
{
    [SerializeField]
    Transform overheadLocation;

    public Vector3 GetIconPosition()
    {
        return Camera.main.WorldToScreenPoint(overheadLocation.position);
    }

    public void ShowFrustrationBubble()
    {
        UIManager.Instance.CreateFrustrationSpeechBubble(this);
    }
}
