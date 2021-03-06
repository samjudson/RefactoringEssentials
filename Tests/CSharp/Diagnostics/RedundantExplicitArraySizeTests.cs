using NUnit.Framework;
using RefactoringEssentials.CSharp.Diagnostics;

namespace RefactoringEssentials.Tests.CSharp.Diagnostics
{
    [TestFixture]
    [Ignore("TODO: Issue not ported yet")]
    public class RedundantExplicitArraySizeTests : CSharpDiagnosticTestBase
    {
        [Test]
        public void TestSimpleCase()
        {
            Test<RedundantExplicitArraySizeAnalyzer>(@"
class Test
{
	void Foo ()
	{
		var foo = new int[3] { 1, 2, 3 };
	}
}
", @"
class Test
{
	void Foo ()
	{
		var foo = new int[] { 1, 2, 3 };
	}
}
");
        }

        [Test]
        public void TestInvalidCase1()
        {
            Analyze<RedundantExplicitArraySizeAnalyzer>(@"
class Test
{
	void Foo (int i)
	{
		var foo = new int[i] { 1, 2, 3 };
	}
}
");
        }

        [Test]
        public void TestInvalidCase2()
        {
            Analyze<RedundantExplicitArraySizeAnalyzer>(@"
class Test
{
	void Foo ()
	{
		var foo = new int[10];
	}
}
");
        }

        [Test]
        public void TestInvalidCase3()
        {
            Analyze<RedundantExplicitArraySizeAnalyzer>(@"
class Test
{
	void Foo ()
	{
		var foo = new int[0];
	}
}
");
        }

        [Test]
        public void TestDisable()
        {
            Analyze<RedundantExplicitArraySizeAnalyzer>(@"
class Test
{
	void Foo ()
	{
		// ReSharper disable once RedundantExplicitArraySize
		var foo = new int[3] { 1, 2, 3 };
	}
}
");
        }
    }
}

