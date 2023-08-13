using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class ParagraphRenderBbCodeTest : ConvertTestBase
	{
		[Fact]
		public void NoOutputChange()
		{
			string input = @"
test
test
";

			string expected = @"
test
test
";

			Run(input, expected);
		}


	}
}