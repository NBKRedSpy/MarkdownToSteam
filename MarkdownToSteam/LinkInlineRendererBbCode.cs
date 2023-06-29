// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace MarkdownToSteam;

/// <summary>
/// A HTML renderer for a <see cref="LinkInline"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{LinkInline}" />
public class LinkInlineRendererBbCode : HtmlObjectRenderer<LinkInline>
{
	protected override void Write(HtmlRenderer renderer, LinkInline link)
	{
		//Remove any images that are relative.  These are generally from GitHub.
		if(link.IsImage && link.Url?.StartsWith("http", StringComparison.OrdinalIgnoreCase) == false)
		{
			ParagraphRenderBbCode.SkipNewLine = true;
			return;
		}

		string? linkLiteralText = GetLinkText(link);
		if (!link.IsImage && link.Url == linkLiteralText)
		{
			//Do not encode as [url] if this is a plain link.
			//Oddly Steam will flag GitHub links wrapped in a url dangerous.
			//This allows the user to declare if they want a [url] or not.
			renderer.Write(link.Url);
			return;

		}
		renderer.Write(link.IsImage ? "[img]" : "[url=");
		renderer.WriteEscapeUrl(link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? link.Url : link.Url);
		if (!link.IsImage)
		{
			renderer.Write(']');
		}

		if (link.IsImage)
		{
				renderer.Write("[/img]");
		}
		else
		{
			LiteralInline? linkText = link.Descendants<LiteralInline>().FirstOrDefault();
			if (linkText is not null)
			{
				renderer.Write(linkText.Content);
			}
			renderer.Write("[/url]");
		}
	}

	private string? GetLinkText(LinkInline link)
	{
		var content = link.Descendants<LiteralInline>().FirstOrDefault()?.Content;

		if(content is null)
		{
			return null;
		}

		return content.Value.Text.Substring(content.Value.Start, content.Value.Length);
	}
}