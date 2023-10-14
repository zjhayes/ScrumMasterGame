using UnityEngine;

public class ProductionManager : MonoBehaviour
{
    [SerializeField]
    private ProductionStats stats;
    [SerializeField]
    private AnimationCurve loadScale; // How much is required based on user load.

    private float availability = 1.0f;
    private int userCount = 0;

    public ProductionStats Stats
    {
        get { return stats; }
    }

    public float Availability
    {
        get { return availability; }
        // TODO: Set availability based on incidents.
    }

    public int UserCount
    {
        get { return userCount; }
        set 
        { 
            userCount = value;
            UpdateLoad();
        }
    }

    public void UpdateLoad()
    {
        // Update production requirements based on user count.
        stats.Maximum = (int) loadScale.Evaluate(userCount);
    }
}
