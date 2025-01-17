using UnityEngine;

public class ProductionStats : MonoBehaviour, IProductionStats
{
    [SerializeField]
    private int usability;
    [SerializeField]
    private int stability;
    [SerializeField]
    private int functionality;
    [SerializeField]
    private int maintainability;
    [SerializeField]
    private int maximumValue = 10;

    public const int MINIMUM = Numeric.ZERO;

    public int Usability 
    { 
        get { return usability; } 
        set { usability = Mathf.Clamp(value, MINIMUM, maximumValue); } 
    }

    public int Stability 
    { 
        get { return stability; } 
        set { stability = Mathf.Clamp(value, MINIMUM, maximumValue); } 
    }

    public int Functionality 
    { 
        get { return functionality; } 
        set { functionality = Mathf.Clamp(value, MINIMUM, maximumValue); } 
    }

    public int Maintainability 
    { 
        get { return maintainability; } 
        set { maintainability = Mathf.Clamp(value, MINIMUM, maximumValue); } 
    }

    public void Add(IProductionStats other)
    {
        // Add each stat, then clamp to MINIMUM
        this.Usability = Mathf.Max(this.Usability + other.Usability, MINIMUM);
        this.Stability = Mathf.Max(this.Stability + other.Stability, MINIMUM);
        this.Functionality = Mathf.Max(this.Functionality + other.Functionality, MINIMUM);
        this.Maintainability = Mathf.Max(this.Maintainability + other.Maintainability, MINIMUM);
    }

    public void Subtract(IProductionStats other)
    {
        // Subtract each stat, then clamp to MINIMUM
        this.Usability = Mathf.Max(this.Usability - other.Usability, MINIMUM);
        this.Stability = Mathf.Max(this.Stability - other.Stability, MINIMUM);
        this.Functionality = Mathf.Max(this.Functionality - other.Functionality, MINIMUM);
        this.Maintainability = Mathf.Max(this.Maintainability - other.Maintainability, MINIMUM);
    }

    public int Total
    {
        get { return usability + stability + functionality + maintainability; }
    }

    public int Average
    {
        get { return Mathf.CeilToInt(Total / 4.0f); }
    }

    public int Maximum
    {
        get { return maximumValue; }
        set { maximumValue = value; }
    }
}
