using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class SelectedIconController : MonoBehaviour
{
    [SerializeField]
    SelectedIcon icon;
    [SerializeField]
    Transform iconLocation;

    Selectable selectable;
    OngoingAction updatePositionAction;

    void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    void Start()
    {
        selectable.onSelect += Show;
        Hide();
    }

    void UpdatePosition()
    {
        Vector3 position = GetIconPosition();
        icon.SetPosition(position);
    }

    void Show()
    {
        this.enabled = true;
        icon.Show();
        updatePositionAction = new OngoingAction(UpdatePosition);
        ActionManager.Instance.Add(updatePositionAction);
        updatePositionAction.Run();
    }

    void Hide()
    {
        this.enabled = false;
        icon.Hide();
        updatePositionAction?.Cancel();
    }

    Vector3 GetIconPosition()
    {
        return Camera.main.WorldToScreenPoint(iconLocation.position);
    }
}
