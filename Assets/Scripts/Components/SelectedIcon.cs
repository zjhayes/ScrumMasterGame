using UnityEngine;

[RequireComponent(typeof(Selectable))]
public class SelectedIcon : MonoBehaviour
{
    Selectable selectable;

    void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    void Start()
    {
        selectable.onSelect += Show;
    }

    void Show()
    {

    }


}
