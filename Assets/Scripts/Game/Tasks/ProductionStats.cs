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

    public int Usability { get { return usability; } set { usability = value; } }
    public int Stability { get { return stability; } set { stability = value; } }
    public int Functionality { get { return functionality; } set { functionality = value; } }
    public int Maintainability { get { return maintainability; } set { maintainability = value; } }

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
}
