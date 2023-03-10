using MongoDB.Bson.Serialization.Attributes;

namespace QuantumWorld.Core.Domain;

[BsonKnownTypes(typeof(CarbonFiberFactory), typeof(QuantumGlassFactory), typeof(HiggsBosonDetector), typeof(Labolatory), typeof(SpaceshipFactory))]
public abstract class Building
{
    public string Name { get; protected set; } = string.Empty;
    public BuildingType Type { get; protected set; }
    public abstract string BaseDescription { get; }
    public string Description { get; set; }
    public int Level { get; protected set; } = 0;
    public float TimeToBuildInSeconds { get; protected set; }
    protected abstract float BaseTimeToBuildInSeconds { get; }
    protected abstract float TimeMultiplier { get; }
    protected abstract float CostMultiplier { get; }
    protected abstract List<Resource> BaseCost { get; }
    public List<Resource> Cost { get; protected set; } = new();
    public bool IsUnderConstruction { get; protected set; }
    public DateTime? ConstructionStartDate { get; protected set; } = null;

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
        TimeToBuildInSeconds = BaseTimeToBuildInSeconds;
    }
    private void SetNewTime()
    {
        TimeToBuildInSeconds = BaseTimeToBuildInSeconds * TimeMultiplier * (Level + 1);
    }
    private void AutoSetBasicAttributes()
    {
        SetName();
        SetType();
        SetCost();
        SetTime();
        SetDescription();
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
    private void SetDescription()
    {
        Description = BaseDescription;
    }
    public void UpgradeBuilding()
    {
        SetNewTime();
        SetNewCost();
        IncreaseLevel();
    }

    public void SetLevelForTests(int level)
    {
        Level = level;
    }
    public void SetConstructionStartDate(DateTime date)
    {
        ConstructionStartDate = date;
    }

    public void SetTimeToBuildInSeconds(float seconds)
    {
        TimeToBuildInSeconds = seconds;
    }

    public void ClearConstructionStartDate()
    {
        ConstructionStartDate = null;
    }

    public void IsBuildingUnderConstruction(bool value)
    {
        IsUnderConstruction = value;
    }
}