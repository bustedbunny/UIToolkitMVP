using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using UIToolkitMVP;
using UIToolkitMVP.ViewGeneration;

namespace GeneratorTests;

using GeneratorTest = CSharpSourceGeneratorTest<Adapter<ViewGenerator>, XUnitVerifier>;

public class AttributeTest
{
    private const string AttributeTarget = @"namespace UIToolkitMVP.ViewGeneration
{
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public sealed class GenerateViewAttribute : System.Attribute
    {
    }
}";


    [Fact]
    public async Task TestAttributeCreation()
    {
        await new GeneratorTest
        {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net60,
            TestState =
            {
                GeneratedSources =
                {
                    (typeof(Adapter<ViewGenerator>),
                        "UIToolkitMVP.GenerateViewAttribute.g.cs",
                        AttributeTarget)
                }
            },
        }.RunAsync();
    }
}