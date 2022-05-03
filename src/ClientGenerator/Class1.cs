using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace ICP.ClientGenerator
{
    [Generator]
    public class HelloWorld : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {

        }

        public void Execute(GeneratorExecutionContext context)
        {
            var code = @"
using System;
namespace Hello {
    public static class World 
    {
        public const string Name = ""Khalid"";
        public static void Hi() => Console.WriteLine($""Hi, {Name}!"");
    }
}";
            context.AddSource(
                "hello.world.generator",
                SourceText.From(code, Encoding.UTF8)
            );
        }
    }
}