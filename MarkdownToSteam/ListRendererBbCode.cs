// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace MarkdownToSteam
{
	/// <summary>
	/// A HTML renderer for a <see cref="ListBlock"/>.
	/// </summary>
	/// <seealso cref="HtmlObjectRenderer{ListBlock}" />
	public class ListRendererBbCode : HtmlObjectRenderer<ListBlock>
	{
		protected override void Write(HtmlRenderer renderer, ListBlock listBlock)
		{
			renderer.EnsureLine();
			//if (renderer.EnableHtmlForBlock)
			//{
			if (listBlock.IsOrdered)
			{
				renderer.Write("[olist]");
				if (listBlock.BulletType != '1')
				{
					renderer.Write(listBlock.BulletType);
				}

				if (listBlock.OrderedStart is not null && listBlock.OrderedStart != "1")
				{
					//renderer.Write(" start=\"");
					renderer.Write(listBlock.OrderedStart);
					renderer.Write('"');
				}
				//renderer.WriteAttributes(listBlock);
				renderer.WriteLine(']');
			}
			else
			{
				renderer.Write("[list");
				renderer.WriteAttributes(listBlock);
				renderer.WriteLine(']');
			}
			//}

			foreach (var item in listBlock)
			{
				var listItem = (ListItemBlock)item;
				var previousImplicit = renderer.ImplicitParagraph;
				renderer.ImplicitParagraph = !listBlock.IsLoose;

				renderer.EnsureLine();
				if (renderer.EnableHtmlForBlock)
				{
					renderer.Write("[*]");
				}

				renderer.WriteChildren(listItem);


				renderer.EnsureLine();
				renderer.ImplicitParagraph = previousImplicit;
			}

			renderer.WriteLine(listBlock.IsOrdered ? "[/olist]" : "[/list]");

			renderer.EnsureLine();
		}
	}
}