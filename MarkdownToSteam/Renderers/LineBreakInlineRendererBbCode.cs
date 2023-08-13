// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Syntax.Inlines;
using Markdig.Renderers.Html;
using Markdig.Renderers;

namespace MarkdownToSteam;

/// <summary>
/// A HTML renderer for a <see cref="LineBreakInline"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{LineBreakInline}" />
public class LineBreakInlineRendererBbCode : HtmlObjectRenderer<LineBreakInline>
{
	/// <summary>
	/// Gets or sets a value indicating whether to render this softline break as a HTML hardline break tag (&lt;br /&gt;)
	/// </summary>
	public bool RenderAsHardlineBreak { get; set; }

	protected override void Write(HtmlRenderer renderer, LineBreakInline obj)
	{
		if (renderer.IsLastInContainer) return;

		if (obj.IsHard || RenderAsHardlineBreak)
		{
			renderer.WriteLine();
		}

		renderer.EnsureLine();
	}
}