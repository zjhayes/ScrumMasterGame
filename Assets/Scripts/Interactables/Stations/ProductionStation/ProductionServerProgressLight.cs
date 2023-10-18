using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ProductionServerProgressLight : MonoBehaviour
{
    private const string PROGRESS_BORDER_PARAMETER = "_ProgressBorder";
    private const string FILL_RATE_PARAMETER = "_FillRate";
    private const float MAX_FILL_VALUE = 0.16f;
    private const float MIN_FILL_VALUE = -0.83f;

    private Material progressBarMaterial;

    private void Start()
    {
        progressBarMaterial = GetComponent<Renderer>().materials[1];
        float progressBorder = GetComponent<MeshFilter>().mesh.bounds.size.x / 2f;
        progressBarMaterial.SetFloat(PROGRESS_BORDER_PARAMETER, progressBorder);
    }

    public void UpdateFill(float percentFill)
    {
        float fill = MAX_FILL_VALUE + percentFill * (MIN_FILL_VALUE - MAX_FILL_VALUE);
        progressBarMaterial.SetFloat(FILL_RATE_PARAMETER, fill);
    }
}
