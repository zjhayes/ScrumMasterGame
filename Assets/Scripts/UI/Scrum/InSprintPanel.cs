using UnityEngine;

public class InSprintPanel : MonoBehaviour
{
    [SerializeField]
    Container container;
    [SerializeField]
    Transform minifiedPosition;
    [SerializeField]
    Transform expandedPosition;

    public void Expand()
    {
        gameObject.transform.SetParent(expandedPosition);
    }

    public void Minify()
    {
        gameObject.transform.SetParent(minifiedPosition);
    }

    public Container Container
    {
        get { return container; }
    }
}
