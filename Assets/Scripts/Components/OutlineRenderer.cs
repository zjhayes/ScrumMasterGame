using UnityEngine;

/** Add to object with Renderer and materials using Outline shader. **/
[RequireComponent(typeof(Renderer))]
public class OutlineRenderer : MonoBehaviour
{
    Renderer materialRenderer;

    const string ENABLE_OUTLINE_PROPERTY = "_Enable_Outline";

    void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        Hide();
    }

    public void Show()
    {
        materialRenderer.material.SetInt(ENABLE_OUTLINE_PROPERTY, 1);
    }

    public void Hide()
    {
        materialRenderer.material.SetInt(ENABLE_OUTLINE_PROPERTY, 0);
    }
}
