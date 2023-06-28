// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace MarkdownToSteam;

/// <summary>
/// A HTML renderer for an <see cref="AutolinkInline"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{AutolinkInline}" />
public class AutoLinkDirectUrlBbCode : HtmlObjectRenderer<AutolinkInline>
{
	

	protected override void Write(HtmlRenderer renderer, AutolinkInline obj)
	{
			renderer.WriteEscapeUrl(obj.Url);
	
	}
}