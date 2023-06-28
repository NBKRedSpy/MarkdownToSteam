using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace MarkdownToSteam
{
	public class ParagraphRenderBbCode : HtmlObjectRenderer<ParagraphBlock>
	{
		protected override void Write(HtmlRenderer renderer, ParagraphBlock obj)
		{
			if(!renderer.ImplicitParagraph)
			{
				renderer.WriteLine();
			}
			
			renderer.WriteLeafInline(obj);

			renderer.EnsureLine();
		}
	}
}
