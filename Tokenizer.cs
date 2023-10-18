namespace Parser;

public class Tokenizer {
private readonly string _text;
private int _index;

private readonly Func<Token>[] _tokens = {
    () => new LogicalOperatorToken(),
    () => new ConditionalOperatorToken(),
    () => new IdToken(),
    () => new NumberToken(),
    () => new SymbolToken(),
    () => new NewLineToken(),
    () => new EOFToken()
};

public Tokenizer(string text) {
    _text = text;
    _index = 0;
    Line = 1;
}

public int Line { get; internal set; }

public Token NextToken() {
    string text = _text[_index..];

    SpaceToken space = new();
    if (space.Match(text)) {
        _index += space.ToString().Length;
        text = _text[_index..];
    }

    Token token = _tokens
        .Select(t => t.Invoke())
        .FirstOrDefault(t => t.Match(text)) ?? new EOFToken();

    if (token is NewLineToken) {
        Line++;
    }

    _index += token.ToString()!.Length;

    return token;
}

public Token PeekNextToken() {
    int index = _index;
    string text = _text[index..];

    SpaceToken space = new();
    if (space.Match(text)) {
        index += space.ToString().Length;
        text = _text[index..];
    }

    return _tokens
        .Select(t => t.Invoke())
        .FirstOrDefault(t => t.Match(text)) ?? new EOFToken();
}
}