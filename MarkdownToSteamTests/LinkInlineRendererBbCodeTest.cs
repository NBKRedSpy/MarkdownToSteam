using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class LinkInlineRendererBbCodeTest : ConvertTestBase
	{

		public Uri BaseUri => new Uri("https://www.example.com");

		[Fact]
		public void EscapedUrl_Success()
		{
			string input = @"[test](https://whatever/foo1[foo2]<)";


			string expected = @"
[url=https://whatever/foo1%5Bfoo2%5D%3C]test[/url]
";

			Run(input, expected);
		}

		[Fact]
		public void Link_Relative_NoBaseUriProvided_ReturnsAbsoluteUri()
		{
			string input = @"[test](https://whatever/foo1/foo2)";

			string expected = @"
[url=https://whatever/foo1/foo2]test[/url]
";

			Run(input, expected);
		}
		[Fact]
		public void Link_SameTextAsUri_ConvertedToPlainLinkText()
		{
			string input = @"[https://whatever/foo1/foo2](https://whatever/foo1/foo2)";

			string expected = @"
https://whatever/foo1/foo2
";

			Run(input, expected);
		}


		[Fact]
		public void Link_Relative_RemoteRelativeImages_LinkRendered()
		{
			string input = @"[test](./foo1/foo2)";

			string expected = @"
[url=./foo1/foo2]test[/url]
";

			Run(input, expected, true);
		}

		[Fact]
		public void Link_Absolute_BaseUriProvided_ReturnsOriginalUri()
		{
			string input = @"[test](https://whatever/foo1/foo2)";

			string expected = @"
[url=https://whatever/foo1/foo2]test[/url]
";

			Run(input, expected, false, BaseUri);
		}

		[Fact]
		public void Link_Relative_NoBaseUri_ReturnsRelative()
		{
			string input = @"[test](./foo1/foo2)";

			string expected = @"
[url=./foo1/foo2]test[/url]
";

			Run(input, expected);
		}

		[Fact]
		public void Image_Realative_NoRemoveOption_ReturnsRelative()
		{
			string input = @"![test](./foo1/foo2/test.png)";

			string expected = @"
[img]./foo1/foo2/test.png[/img]
";

			Run(input, expected);
		}

		[Fact]
		public void Image_Relative_ImageRemoveOption_ReturnsRelative()
		{
			string input = @"![test](./foo1/foo2/test.png)";

			string expected = @"
";

			Run(input, expected, true);
		}



		[Fact]
		public void Image_RelativeImage_ImageRemoveOption_BaseUri_RemovesImage()
		{
			string input = @"![test](./foo1/foo2/test.png)";

			string expected = @"
";

			Run(input, expected, true, BaseUri);
		}

		[Fact]
		public void Image_AbsoluteImage__BaseUri_ReturnsOriginalLink()
		{
			string input = @"![test](https://test.com/./foo1/foo2)";

			string expected = @"
[img]https://test.com/foo1/foo2[/img]
";

			Run(input, expected, false, BaseUri);
		}

		[Fact]
		public void Image_RelativeImage__BaseUri_ReturnsAbsoluteLink()
		{
			string input = @"![test](https://test.com/./foo1/foo2)";

			string expected = @"
[img]https://test.com/foo1/foo2[/img]
";

			Run(input, expected, false, BaseUri);
		}

		[Fact]
		public void Link_DocumentReference_ChangeToLinkText_Success()
		{
			string input = @"test
See [Settings](#settings) below.
Some other text";


			string expected = @"
test
See Settings below.
Some other text
";

			Run(input, expected);
		}

		//Skip - This is handled in the command line part.  
		//		[Fact]
		//		public void BaseUri_Subfolder_HasNoTrailingSlash_Success()
		//		{
		//			string input = @"![test](./foo1/foo2)";

		//			string expected = @"
		//[img]https://example.com/SubFolder/foo1/foo2[/img]
		//";

		//			Run(input, expected, false, new Uri("https://example.com/SubFolder"));
		//		}


	}
}