using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig.Renderers.Html;
using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using Markdig.Syntax;

namespace MarkdownToSteam
{
	internal class CodeBlockBBCode : HtmlObjectRenderer<CodeBlock>
	{
		protected override void Write(HtmlRenderer renderer, CodeBlock obj)
		{
			renderer.Write("[code]");
			renderer.EnsureLine();
			renderer.WriteLeafRawLines(obj, true, false);
			renderer.Write("[/code]");
			renderer.EnsureLine();
		}
	}

}
