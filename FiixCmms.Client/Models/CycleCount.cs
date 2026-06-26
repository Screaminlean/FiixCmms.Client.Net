namespace FiixCmms.Client.Models;

/// <summary>
/// Represents a record in the 'CycleCount' table.
/// </summary>
public class CycleCount : ClientCmmsDto
{
    public long? Id { get; set; }
    public long? BolComplete { get; set; }
    public long? BolIncludeClassA { get; set; }
    public long? BolIncludeClassB { get; set; }
    public long? BolIncludeClassC { get; set; }
    public long? BolIncludeNotClassified { get; set; }
    public double? DblGrossVariance { get; set; }
    public double? DblNetVariance { get; set; }
    public double? DblTotalValueCounted { get; set; }
    public double? DblTotalValueExpected { get; set; }
    public DateTime? DtmCompleted { get; set; }
    public DateTime? DtmCreated { get; set; }
    public long? IntCompletedBy { get; set; }
    public long? IntCreatedBy { get; set; }
    public long? IntSiteID { get; set; }
}

