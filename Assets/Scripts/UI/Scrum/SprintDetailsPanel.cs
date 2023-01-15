using UnityEngine;

public class SprintDetailsPanel : PanelController
{
    [SerializeField]
    Transform minifiedPosition;
    [SerializeField]
    Transform expandedPosition;

    public void Expand()
    {
        gameObject.transform.parent = expandedPosition;
    }

    public void Minify()
    {
        gameObject.transform.parent = minifiedPosition;
    }
}
