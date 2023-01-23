using UnityEngine;

/** Add to object with Renderer and materials using Outline shader. **/
[RequireComponent(typeof(Renderer))]
public class OutlineRenderer : MonoBehaviour
{
    Renderer renderer;

    const string ENABLE_OUTLINE_PROPERTY = "_Enable_Outline";

    void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        Hide();
    }

    public void Show()
    {
        renderer.material.SetInt(ENABLE_OUTLINE_PROPERTY, 1);
    }

    public void Hide()
    {
        renderer.material.SetInt(ENABLE_OUTLINE_PROPERTY, 0);
    }
}
