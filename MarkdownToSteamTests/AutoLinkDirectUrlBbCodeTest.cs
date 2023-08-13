using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownToSteamTests
{
	public class AutoLinkDirectUrlBbCodeTest : ConvertTestBase
	{

		[Fact]
		public void EscapedUrl_Escaped_DoesNotConvert_SuccessOpinionated()
		{

			//The URL should probably be encoded.  Conflicts with this program's
			//renderer.EnableHtmlEscape = false;
			//In theory the links should already be escaped.
			string input = @"(test)[./some place/ >~]";

			string expected = @"
(test)[./some place/ >~]
";

			Run(input, expected);

		}



	}
}
