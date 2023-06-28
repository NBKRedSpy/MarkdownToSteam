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
}