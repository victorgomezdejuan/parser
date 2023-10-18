using System.Text.RegularExpressions;

namespace Parser;

public abstract class Token {
    internal abstract bool Match(string text);

    public string GetUpper()
        => ToString()!.ToUpper();

    public string GetLower()
        => ToString()!.ToLower();

    public static implicit operator string(Token t) => t.ToString()!;
}

public abstract class RegularToken : Token {
    internal abstract string Regex { get; }
    private readonly Regex _regexText;
    private string _text = "";

    internal RegularToken()
        => _regexText = new(Regex, RegexOptions.IgnoreCase);

    internal override bool Match(string text) {
        Match m = _regexText.Match(text);
        if (m.Success && m.Index == 0)
            _text = m.Value;

        return m.Success && m.Index == 0;
    }

    public override string ToString()
        => _text;
}

public class ConditionalOperatorToken : RegularToken {
    internal override string Regex => @"\>\=|\<\=|\<|\>|\=";
}

public class LogicalOperatorToken : RegularToken {
    internal override string Regex => @"(AND|OR)\b";
}

public class IdToken : RegularToken {
    internal override string Regex => @"[a-zA-Z][a-zA-Z0-9_-]*";
}

public class NumberToken : RegularToken {
    internal override string Regex => @"[0-9]+(\.[0-9]+)?";
}

public class SymbolToken : RegularToken {
    internal override string Regex => @"[\(\):,\[\]%]";
}

public class NewLineToken : RegularToken {
    internal override string Regex => @"(\r)?\n";
}

public class EOFToken : Token {
    internal override bool Match(string text)
        => string.IsNullOrWhiteSpace(text);

    public override string ToString()
        => string.Empty;
}

public class SpaceToken : RegularToken {
    internal override string Regex => @"[^\S\r\n]+"; // s: space/new lines, but no new lines
}