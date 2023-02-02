using UnityEngine;

/** A character's Overhead is where icons, emotes and progress indicators will appear. */
public class OverheadController : GameBehaviour
{
    [SerializeField]
    Transform overheadLocation;

    public Vector3 GetIconPosition()
    {
        return Camera.main.WorldToScreenPoint(overheadLocation.position);
    }

    public void ShowFrustrationBubble()
    {
        gameManager.UI.ShowFrustrationEmote(this);
    }
}
