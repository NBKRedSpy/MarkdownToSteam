using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class CodeBlockBBCodeTest : ConvertTestBase
	{
		[Fact]
		public void CodeBlock_Success ()
		{
			string input = @"
```
test test test
```
";

			//Not sure why, the default always strips the leading CR. 
			//Output is fine so not bothering to track it down currently.
			string expected = @"[code]
test test test
[/code]
"; 
			Run(input, expected);
		}


	}
}