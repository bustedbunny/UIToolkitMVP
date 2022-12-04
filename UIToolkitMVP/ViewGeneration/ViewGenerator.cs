using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UIToolkitMVP.ViewGeneration;

[Generator]
public sealed class ViewGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register attribute declaration
        context.RegisterPostInitializationOutput(PostInitializationCallback);


        // var classProvider = context.SyntaxProvider.CreateSyntaxProvider(SyntaxPredicate, Transform)
        //     .Where(static x => x.HasValue).Collect();


        // context.RegisterSourceOutput(classProvider,
        //     static (ctx, data) => Execute(ctx, data));
    }

    // private T Transform<T>(GeneratorSyntaxContext arg1, CancellationToken arg2)
    // {
    //     
    // }

    private static bool SyntaxPredicate(SyntaxNode node, CancellationToken tkn)
    {
        return node is FieldDeclarationSyntax { AttributeLists.Count: > 0 }
            or PropertyDeclarationSyntax { AttributeLists.Count: > 0 };
    }

    private void PostInitializationCallback(IncrementalGeneratorPostInitializationContext obj)
    {
        obj.AddSource("UIToolkitMVP.GenerateViewAttribute.g.cs",
            AttributeSyntaxBuilder.GenerateAttribute().GetText(Encoding.UTF8));
    }
}