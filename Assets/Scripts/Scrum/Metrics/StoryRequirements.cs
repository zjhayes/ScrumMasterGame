using System;
using UnityEngine;

[Serializable]
public class StoryRequirements : IProductionStats
{
    [SerializeField]
    [Range(0, 10)]
    private int usability;
    [SerializeField]
    [Range(0, 10)]
    private int stability;
    [SerializeField]
    [Range(0, 10)]
    private int functionality;
    [SerializeField]
    [Range(0, 10)]
    private int maintainability;

    private const int MAXIMUM = 10;
    public const int MINIMUM = Numeric.ZERO;

    public int Usability
    {
        get { return usability; }
    }

    public int Stability
    {
        get { return stability; }
    }

    public int Functionality
    {
        get { return functionality; }
    }

    public int Maintainability
    {
        get { return maintainability; }
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
        get { return MAXIMUM; }
    }
}
