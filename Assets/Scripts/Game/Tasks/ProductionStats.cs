using System.Collections;
using System.Collections.Generic;

public class ProductionStats
{
    private int usability;
    private int stability;
    private int functionality;
    private int maintainability;

    public ProductionStats(int usability, int stability, int functionality, int maintainability)
    {
        this.usability = usability;
        this.stability = stability;
        this.functionality = functionality;
        this.maintainability = maintainability;
    }

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
}
