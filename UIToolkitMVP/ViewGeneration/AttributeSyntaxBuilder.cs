using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace UIToolkitMVP.ViewGeneration;

public static class AttributeSyntaxBuilder
{
    public const string Namespace = "UIToolkitMVP.ViewGeneration";

    public const string SystemAttribute = "System.Attribute";
    public const string AttributeUsage = "System.AttributeUsage";

    public const string AttributeTypeName = "GenerateViewAttribute";

    public const string AttributeTargetsEnum = "System.AttributeTargets";

    public const string FieldEnum = nameof(AttributeTargets.Field);
    public const string PropertyEnum = nameof(AttributeTargets.Property);

    public static CompilationUnitSyntax GenerateAttribute()
    {
        // [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
        var attributeUsageArgument = AttributeArgument(BinaryExpression(SyntaxKind.BitwiseOrExpression,
            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName(AttributeTargetsEnum),
                IdentifierName(FieldEnum)),
            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName(AttributeTargetsEnum),
                IdentifierName(PropertyEnum))));

        var attribute = Attribute(IdentifierName(AttributeUsage))
            .AddArgumentListArguments(attributeUsageArgument);

        // public sealed class GenerateViewAttribute : System.Attribute
        var classDeclaration = ClassDeclaration(AttributeTypeName)
            .AddAttributeLists(AttributeList().AddAttributes(attribute))
            .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.SealedKeyword))
            .AddBaseListTypes(SimpleBaseType(IdentifierName(SystemAttribute)));

        // namespace UIToolkitMVP.ViewGeneration
        var namespaceDeclaration = NamespaceDeclaration(IdentifierName(Namespace))
            .AddMembers(classDeclaration);

        return CompilationUnit()
            .AddMembers(namespaceDeclaration)
            .NormalizeWhitespace();
    }
}