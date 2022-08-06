using System.Text.RegularExpressions;
using EasyParse.Native;
using EasyParse.Native.Annotations;

namespace EasyParseSample
{
    public class CompilerTests
    {
        [Fact]
        public void Compile_test()
        {
            var compiler = new MyGrammar().BuildCompiler<Program>();

            var result = compiler.Compile("abc,def,gh,i");

            Assert.True(result.IsSuccess);
            Assert.Equal(new[]{"abc", "def", "gh", "i" }, result.Result.Identifiers.Select(x => x.Name));
        }
    }
}

class MyGrammar : NativeGrammar 
{
    public Identifier Identifier([R("identifier", @"\w*")]string id) => new Identifier(id);
	
    public IdentifierList IdentifierList(Identifier identifier) => new() { identifier };
	
    public IdentifierList IdentifierList(Identifier identifier, [L(",")] string comma, IdentifierList identifierList) => new(new[]{ identifier }.Concat(identifierList));
	
    [Start]
    public Program Program(IdentifierList identifierList) => new Program(identifierList);
	
    protected override IEnumerable<Regex> IgnorePatterns => new[] { new Regex(@"\s+") };
}

public class Identifier
{
    public Identifier(string name)
    {
        Name = name;
    }
	
    public string Name { get; }
}

public class IdentifierList : List<Identifier>
{
    public IdentifierList()
    {
    }

    public IdentifierList(IEnumerable<Identifier> list) : base(list)
    {
    }
}

public class Program
{
    public IEnumerable<Identifier> Identifiers { get; }

    public Program(IEnumerable<Identifier> identifiers)
    {
        Identifiers = identifiers;
    }
}