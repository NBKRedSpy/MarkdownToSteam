using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class CodeInlineRendererBBCodeTest : ConvertTestBase
	{
		[Fact]
		public void InlineWrite_Success()
		{
			string input = @"
```test test```
";

			string expected = @"
[i]test test[/i]
";
			Run(input, expected);
		}


	}
}