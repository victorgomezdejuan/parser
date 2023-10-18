namespace Parser;

public record DiscountCondition
{
    public DiscountCondition(char clientType, int minimunOrderAmmount)
    {
        ClienType = clientType;
        MinimunOrderAmmount = minimunOrderAmmount;
    }

    public char ClienType { get; }

    public int MinimunOrderAmmount { get; }

    public override string ToString()
    {
        return $"ClientType = {ClienType} AND OrderAmmount > {MinimunOrderAmmount / 1000}K";
    }
}