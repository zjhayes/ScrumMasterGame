using UnityEngine;

// Moves panel between parents to expand and minify.
public class ExpandablePanel : MenuController
{
    [SerializeField]
    Transform minifiedPosition;
    [SerializeField]
    int minifiedSiblingIndex = 1;
    [SerializeField]
    Transform expandedPosition;
    [SerializeField]
    int expandedSiblingIndex = 1;

    public void Expand()
    {
        gameObject.transform.SetParent(expandedPosition);
        gameObject.transform.SetSiblingIndex(expandedSiblingIndex);
    }

    public void Minify()
    {
        gameObject.transform.SetParent(minifiedPosition);
        gameObject.transform.SetSiblingIndex(minifiedSiblingIndex);
    }
}
