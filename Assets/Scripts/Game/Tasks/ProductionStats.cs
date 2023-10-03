using UnityEngine;

public class ProductionStats : MonoBehaviour
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

    public void Add(ProductionStats other)
    {
        this.Usability += other.Usability;
        this.Stability += other.Stability;
        this.Functionality += other.Functionality;
        this.Maintainability += other.Maintainability;
    }

    public void Subtract(ProductionStats other)
    {
        this.Usability -= other.Usability;
        this.Stability -= other.Stability;
        this.Functionality -= other.Functionality;
        this.Maintainability -= other.Maintainability;
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
    }
}
