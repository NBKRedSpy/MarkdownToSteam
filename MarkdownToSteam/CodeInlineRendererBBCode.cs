using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace MarkdownToSteam
{
    internal class CodeInlineRendererBBCode : HtmlObjectRenderer<CodeInline>
    {
        protected override void Write(HtmlRenderer renderer, CodeInline obj)
        {
            renderer.Write("[i]");
            renderer.Write(obj.ContentSpan);
            renderer.Write("[/i]");
        }
    }
}
