using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain;

[BsonKnownTypes(typeof(CarbonFiberFactory), typeof(QuantumGlassFactory), typeof(HiggsBosonDetector), typeof(Labolatory), typeof(SpaceshipFactory))]
public abstract class Building
{
    public string Name { get; protected set; } = string.Empty;
    public BuildingType Type { get; protected set; }
    public abstract string Description { get; }
    public int Level { get; protected set; } = 0;
    public TimeSpan TimeToBuild { get; protected set; }
    protected abstract TimeSpan BaseTimeToBuild { get; }
    protected abstract float TimeMultiplier { get; }
    protected abstract float CostMultiplier { get; }
    protected abstract List<Resource> BaseCost { get; }
    public List<Resource> Cost { get; protected set; } = new();
    public bool IsUnderConstruction { get; protected set; }
    public DateTime FinishDate { get; protected set; }

    public Building()
    {
        AutoSetBasicAttributes();
    }
    private void IncreaseLevel()
    {
        Level++;
    }
    private void SetNewCost()
    {
        foreach (var cost in Cost)
        {
            cost.Value *= CostMultiplier;
        }
    }
    private void SetTime()
    {
        TimeToBuild = BaseTimeToBuild;
    }
    private void SetNewTime()
    {
        TimeToBuild = BaseTimeToBuild * TimeMultiplier * (Level + 1);
    }
    private void AutoSetBasicAttributes()
    {
        SetName();
        SetType();
        SetCost();
        SetTime();
    }
    private void SetName()
    {
        Name = this.GetType().Name;
    }
    private void SetType()
    {
        if (!Enum.IsDefined(typeof(BuildingType), Name))
        {
            throw new Exception("Building type not found.");
        }
        BuildingType buildingtype;
        Enum.TryParse(Name, out buildingtype);
        Type = buildingtype;
    }
    private void SetCost()
    {
        Cost = BaseCost;
    }
    public void UpgradeBuilding()
    {
        SetNewTime();
        SetNewCost();
        IncreaseLevel();
    }
}