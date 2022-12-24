using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class SelectedIconController : MonoBehaviour
{
    [SerializeField]
    Transform iconLocation;

    SelectedIcon icon;
    Selectable selectable;
    OngoingAction updatePositionAction;

    void Awake()
    {
        selectable = GetComponent<Selectable>();
        icon = UIManager.Instance.SelectedCharacterIcon;
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
