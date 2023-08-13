using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class HtmlTableRendererBBCodeTest : ConvertTestBase
	{
		[Fact]
		public void DynamicWidthTable_Success()
		{
			string input = @"
|Test|Test 2|Test 3|
|--|--|--|
|Some Value| Other value|  Other other value |
|Foo Value| Foo 2 Other value|  Foo 3 ........... Other other value |
";

			string expected = @"[table]
[tr]
[td]Test
[/td]
[td]Test 2
[/td]
[td]Test 3
[/td]
[/tr]
[tr]
[td]Some Value
[/td]
[td]Other value
[/td]
[td]Other other value
[/td]
[/tr]
[tr]
[td]Foo Value
[/td]
[td]Foo 2 Other value
[/td]
[td]Foo 3 ........... Other other value
[/td]
[/tr]
[/table]
";

			Run(input, expected);
		}


		[Fact]
		public void DynamicWidthTable_NoOuterPipes_Success()
		{
			string input = @"
|Test|Test 2|Test 3|
|--|--|--|
Some Value| Other value|  Other other value 
Foo Value| Foo 2 Other value|  Foo 3 ........... Other other value 
";

			string expected = @"[table]
[tr]
[td]Test
[/td]
[td]Test 2
[/td]
[td]Test 3
[/td]
[/tr]
[tr]
[td]Some Value
[/td]
[td]Other value
[/td]
[td]Other other value
[/td]
[/tr]
[tr]
[td]Foo Value
[/td]
[td]Foo 2 Other value
[/td]
[td]Foo 3 ........... Other other value
[/td]
[/tr]
[/table]
";

			Run(input, expected);
		}
	}
}