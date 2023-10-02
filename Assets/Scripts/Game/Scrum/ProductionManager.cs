using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    [SerializeField]
    private ProductionStats stats;

    private float numberOfErrors = 0f;

    public ProductionStats Stats
    {
        get { return stats; }
    }

    public float NumberOfErrors
    {
        get { return numberOfErrors; }
        set { numberOfErrors = value; }
    }
}
