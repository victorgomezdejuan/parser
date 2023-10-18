namespace Parser;

public record DiscountRule
{
    public DiscountRule(decimal discountToBeApplied, ICollection<DiscountCondition> conditions)
    {
        DiscountToBeApplied = discountToBeApplied;
        Conditions = conditions.ToArray().AsReadOnly();
    }

    public decimal DiscountToBeApplied { get; }

    public IReadOnlyCollection<DiscountCondition> Conditions { get; }

    public bool IsApplicable(char clientType, int orderAmmount)
    {
        return Conditions.Any(c => c.ClienType == clientType && c.MinimunOrderAmmount <= orderAmmount);
    }

    public override string ToString()
    {
        return $"Discount = {DiscountToBeApplied * 100}%\n" +
            string.Join("\n", Conditions.Select(c => c.ToString()));
    }
}