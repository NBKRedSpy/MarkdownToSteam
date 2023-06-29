// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace MarkdownToSteam;

/// <summary>
/// An HTML renderer for a <see cref="HeadingBlock"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{HeadingBlock}" />
public class HeadingRendererBbCode : HtmlObjectRenderer<HeadingBlock>
{
	private static readonly string[] HeadingTexts = {
		"h1",
		"h2",
		"h3",
		"h4",
		"h5",
		"h6",
	};

	protected override void Write(Markdig.Renderers.HtmlRenderer renderer, HeadingBlock obj)
	{
		int index = obj.Level - 1;
		string[] headings = HeadingTexts;
		string headingText = ((uint)index < (uint)headings.Length)
			? headings[index]
			: $"h{obj.Level}";


		if(renderer.IsFirstInContainer == false)
		{
			renderer.WriteLine();
		}
		else
		{
			renderer.EnsureLine();
		}

		renderer.Write("[");
		renderer.Write(headingText);
		renderer.Write("]");

		renderer.WriteLeafInline(obj);

		renderer.Write("[/");
		renderer.Write(headingText);
		renderer.WriteLine("]");

		//Compensates for markdown which has a line feed after the header,
		//which is usually ignored by MD
		renderer.EnsureLine();

	}
}