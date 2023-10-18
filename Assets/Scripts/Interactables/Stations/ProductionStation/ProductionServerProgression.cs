using UnityEngine;

[RequireComponent(typeof(ProductionServer))]
public class ProductionServerProgression : GameBehaviour
{
    [SerializeField]
    private ProductionServerProgressLight usabilityLight;
    [SerializeField]
    private ProductionServerProgressLight stabilityLight;
    [SerializeField]
    private ProductionServerProgressLight functionalityLight;
    [SerializeField]
    private ProductionServerProgressLight maintainabilityLight;

    private void Awake()
    {
        gameManager.Sprint.OnBeginSprint += UpdateProgress;
    }

    /* Update progress bar lights on production server*/
    private void UpdateProgress()
    {
        ProductionStats stats = gameManager.Production.Stats;

        float usabilityPercent = stats.Usability / (float) stats.Maximum;
        usabilityLight.UpdateFill(usabilityPercent);
        float stabilityPercent = stats.Stability / (float) stats.Maximum;
        stabilityLight.UpdateFill(stabilityPercent);
        float functionalityPercent = stats.Functionality / (float) stats.Maximum;
        functionalityLight.UpdateFill(functionalityPercent);
        float maintainabilityPercent = stats.Maintainability / (float) stats.Maximum;
        maintainabilityLight.UpdateFill(maintainabilityPercent);
    }
}
