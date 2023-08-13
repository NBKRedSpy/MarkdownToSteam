using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class QuoteBlockRendererBbCodeTest : ConvertTestBase
	{
		[Fact]
		public void NoOutputChange()
		{
			string input = @"> test
> test2";

			string expected = @"[quote]
test
test2
[/quote]
";

			Run(input, expected);
		}


	}
}