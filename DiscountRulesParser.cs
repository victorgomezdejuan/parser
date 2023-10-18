namespace Parser;

public class DiscountRulesParser
{
    private readonly Tokenizer tokenizer;

    public DiscountRulesParser(Tokenizer tokenizer) => this.tokenizer = tokenizer;

    public string GetLiteral() => tokenizer.PeekNextToken();

    public void CheckLiteral(string literal)
    {
        if (tokenizer.NextToken() != literal)
        {
            throw new Exception($"Missing '{literal}' in line {tokenizer.Line}");
        }
    }

    public void ParseNewLines()
    {
        while (tokenizer.PeekNextToken() is NewLineToken)
        {
            tokenizer.NextToken();
        }
    }

    public char GetClientType() => tokenizer.NextToken()?.ToString()?[0] ?? throw new Exception("Invalid input");

    public int GetAmount()
    {
        int amount = int.Parse(tokenizer.NextToken() ?? throw new Exception("Invalid input")) * 1000;
        CheckLiteral("K");
        return amount;
    }

    public decimal GetDiscount()
    {
        decimal discount = decimal.Parse(tokenizer.NextToken() ?? throw new Exception("Invalid input")) / 100M;
        CheckLiteral("%");
        return discount;
    }
}