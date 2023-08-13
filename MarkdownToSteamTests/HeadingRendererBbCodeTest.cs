using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class HeadingRendererBbCodeTest : ConvertTestBase
	{
		[Fact]
		public void HeadingTest_1_6_Success()
		{

			string input;
			string expected;

			for (int i = 1; i <= 6; i++)
			{

				input = $"{new String('#',i)} test";
				expected = $@"[h{i}]test[/h{i}]
";

				Run(input, expected);
			}

			
		}


	}
}