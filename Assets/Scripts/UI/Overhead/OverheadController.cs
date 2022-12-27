using UnityEngine;

public class OverheadController : MonoBehaviour
{
    [SerializeField]
    Transform overheadLocation;

    public Vector3 GetIconPosition()
    {
        return Camera.main.WorldToScreenPoint(overheadLocation.position);
    }
}
