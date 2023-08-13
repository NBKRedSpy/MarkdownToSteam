using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace MarkdownToSteam.Renderers
{
    public class ParagraphRenderBbCode : HtmlObjectRenderer<ParagraphBlock>
    {
        public static bool SkipNewLine { get; set; }

        protected override void Write(HtmlRenderer renderer, ParagraphBlock obj)
        {

            if (!renderer.ImplicitParagraph)
            {
                renderer.WriteLine();
            }

            renderer.WriteLeafInline(obj);

            if (SkipNewLine)
            {
                SkipNewLine = false;
                return;
            }

            renderer.EnsureLine();
        }
    }
}
