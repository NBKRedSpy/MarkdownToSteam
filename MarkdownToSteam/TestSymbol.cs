using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace MarkdownToSteam
{
	public class TestSymbol<TObject> : HtmlObjectRenderer<TObject> where TObject : MarkdownObject
	{
		//protected override void Write(HtmlRenderer renderer, LinkReferenceDefinition obj)
		//{

		//}
		protected override void Write(HtmlRenderer renderer, TObject obj)
		{
			renderer.Write("******************* hit *********************");
		}
	}
}
