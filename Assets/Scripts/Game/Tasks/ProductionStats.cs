using System.Collections;
using System.Collections.Generic;

public class ProductionStats
{
    private float usability;
    private float stability;
    private float database;
    private float security;

    public ProductionStats(float usability, float stability, float database, float security)
    {
        this.usability = usability;
        this.stability = stability;
        this.database = database;
        this.security = security;
    }

    public float Usability { get { return usability; } set { usability = value; } }
    public float Stability { get { return stability; } set { stability = value; } }
    public float Database { get { return database; } set { database = value; } }
    public float Security { get { return security; } set { security = value; } }

    public void Add(ProductionStats other)
    {
        this.Usability += other.Usability;
        this.Stability += other.Stability;
        this.Database += other.Database;
        this.Security += other.Security;
    }

    public void Subtract(ProductionStats other)
    {
        this.Usability -= other.Usability;
        this.Stability -= other.Stability;
        this.Database -= other.Database;
        this.Security -= other.Security;
    }
}
