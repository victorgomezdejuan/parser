using Parser;

string inputText =
    @"RULES:
    ClientType = A AND OrderAmount > 100K
    ClientType = B AND OrderAmount > 200K
    ACTION:
    Discount = 10%";

Tokenizer tokenizer = new(inputText);
DiscountRulesParser parser = new(tokenizer);

List<DiscountCondition> conditions = new();

parser.CheckLiteral("RULES");
parser.CheckLiteral(":");
parser.ParseNewLines();

while(parser.GetLiteral() != "ACTION")
{
    parser.CheckLiteral("ClientType");
    parser.CheckLiteral("=");
    char clientType = parser.GetClientType();
    parser.CheckLiteral("AND");
    parser.CheckLiteral("OrderAmount");
    parser.CheckLiteral(">");
    int minimunOrderAmmount = parser.GetAmount();
    parser.ParseNewLines();

    conditions.Add(new DiscountCondition(clientType, minimunOrderAmmount));
}

parser.CheckLiteral("ACTION");
parser.CheckLiteral(":");
parser.ParseNewLines();
parser.CheckLiteral("Discount");
parser.CheckLiteral("=");
decimal discount = parser.GetDiscount();
DiscountRule discountRule = new(discount, conditions);

Console.WriteLine(discountRule);