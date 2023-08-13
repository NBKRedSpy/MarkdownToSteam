using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace MarkdownToSteam.Renderers
{
    internal class EmphasisInlineRendererBbCode : HtmlObjectRenderer<EmphasisInline>
    {
        protected override void Write(HtmlRenderer renderer, EmphasisInline obj)
        {
            var tag = obj.DelimiterChar switch
            {
                '*' or '_' when obj.DelimiterCount == 1 => "i",
                '*' or '_' when obj.DelimiterCount == 2 => "b",
                '~' when obj.DelimiterCount == 2 => "strike",
                _ => null
            };

            if (tag == null)
            {
                for (int i = 0; i < obj.DelimiterCount; i++)
                    renderer.Write(obj.DelimiterChar);
            }
            else
            {
                renderer.Write($"[{tag}]");
            }
            renderer.WriteChildren(obj);
            if (tag == null)
            {
                for (int i = 0; i < obj.DelimiterCount; i++)
                    renderer.Write(obj.DelimiterChar);
            }
            else
            {
                renderer.Write($"[/{tag}]");
            }
        }
    }
}
