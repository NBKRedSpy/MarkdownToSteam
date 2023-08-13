using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig;
using MarkdownToSteam.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;

namespace MarkdownToSteam
{
	public class Converter
	{
		public void Convert(TextReader markdownFile, TextWriter writer, bool removeRelativeImages, Uri? baseUri)
		{

			var pipe = new MarkdownPipelineBuilder()
				.UseAdvancedExtensions()
				.UseSoftlineBreakAsHardlineBreak()
				.Build();


			var mdDoc = Markdig.Markdown.Parse(markdownFile.ReadToEnd(), pipe);

			var renderer = new HtmlRenderer(writer);
			renderer.EnableHtmlEscape = false;

			renderer.ObjectWriteBefore += Renderer_ObjectWriteBefore;
			bool removeResult;

			pipe.Setup(renderer);

			removeResult = renderer.ObjectRenderers.Replace<ParagraphRenderer>(new ParagraphRenderBbCode());
			removeResult = renderer.ObjectRenderers.Replace<Markdig.Extensions.Tables.HtmlTableRenderer>(new HtmlTableRendererBBCode());
			removeResult = renderer.ObjectRenderers.Replace<ListRenderer>(new ListRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<HeadingRenderer>(new HeadingRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<LinkInlineRenderer>(new LinkInlineRendererBbCode(removeRelativeImages, baseUri));
			removeResult = renderer.ObjectRenderers.Replace<LineBreakInlineRenderer>(new LineBreakInlineRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<EmphasisInlineRenderer>(new EmphasisInlineRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<QuoteBlockRenderer>(new QuoteBlockRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<CodeInlineRenderer>(new CodeInlineRendererBBCode());
			removeResult = renderer.ObjectRenderers.Replace<CodeBlockRenderer>(new CodeBlockBBCode());

			renderer.Render(mdDoc);
			writer.Flush();
		}

		//Used for debugging
		private static void Renderer_ObjectWriteBefore(IMarkdownRenderer arg1, MarkdownObject arg2)
		{
		}
	}
}
