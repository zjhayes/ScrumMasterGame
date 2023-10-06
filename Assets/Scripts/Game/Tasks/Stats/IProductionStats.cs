
public interface IProductionStats
{
    public int Usability { get; }
    public int Stability { get; }
    public int Functionality { get; }
    public int Maintainability { get; }
    public int Total { get; }
    public int Average { get; }
    public int Maximum { get; }
}
