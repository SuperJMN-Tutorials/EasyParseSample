[Easy-Parse](https://github.com/zoran-horvat/easy-parse) is a powerful parser creation library.

The documentation is, however very limited.

This is a sample for others to see how to define a list in a `NativeGrammar`, something that isn't explicitly explained in the original docs, but a rather common scenario.

Take this grammar:

```
class MyGrammar : NativeGrammar 
{
    public Identifier Identifier([R("identifier", @"\w*")]string id) => new Identifier(id);
	
    public IdentifierList IdentifierList(Identifier identifier) => new() { identifier };
	
    public IdentifierList IdentifierList(Identifier identifier, [L(",")] string comma, IdentifierList identifierList) => new(new[]{ identifier }.Concat(identifierList));
	
    [Start]
    public Program Program(IdentifierList identifierList) => new Program(identifierList);
	
    protected override IEnumerable<Regex> IgnorePatterns => new[] { new Regex(@"\s+") };
}
```

See how we define a type `Identifier` and another `IdentifierList`. We won't be able to use `List<Identifier>` for a list of identifiers. We need to create a named type for that.
