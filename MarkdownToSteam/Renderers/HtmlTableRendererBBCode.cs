// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license.
// See the license.txt file in the project root for more information.

using System.Globalization;
using Markdig.Extensions.Tables;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace MarkdownToSteam.Renderers;

/// <summary>
/// A HTML renderer for a <see cref="Table"/>
/// </summary>
/// <seealso cref="HtmlObjectRenderer{TableBlock}" />
public class HtmlTableRendererBBCode : HtmlObjectRenderer<Table>
{
    protected override void Write(HtmlRenderer renderer, Table table)
    {
        if (renderer.EnableHtmlForBlock)
        {
            renderer.EnsureLine();
            renderer.Write("[table").WriteAttributes(table).WriteLine(']');

            bool hasColumnWidth = false;
            foreach (var tableColumnDefinition in table.ColumnDefinitions)
            {
                if (tableColumnDefinition.Width != 0.0f && tableColumnDefinition.Width != 1.0f)
                {
                    hasColumnWidth = true;
                    break;
                }
            }

            if (hasColumnWidth)
            {
                foreach (var tableColumnDefinition in table.ColumnDefinitions)
                {
                    var width = Math.Round(tableColumnDefinition.Width * 100) / 100;
                    var widthValue = string.Format(CultureInfo.InvariantCulture, "{0:0.##}", width);
                    renderer.WriteLine($"<col style=\"width:{widthValue}%\" />");
                }
            }

            foreach (var rowObj in table)
            {
                var row = (TableRow)rowObj;

                renderer.Write("[tr").WriteAttributes(row).WriteLine(']');
                for (int i = 0; i < row.Count; i++)
                {
                    var cellObj = row[i];
                    var cell = (TableCell)cellObj;

                    renderer.EnsureLine();
                    renderer.Write("[td");

                    renderer.WriteAttributes(cell);
                    renderer.Write(']');

                    var previousImplicitParagraph = renderer.ImplicitParagraph;
                    if (cell.Count == 1)
                    {
                        renderer.ImplicitParagraph = true;
                    }

                    renderer.Write(cell);
                    renderer.ImplicitParagraph = previousImplicitParagraph;

                    renderer.WriteLine("[/td]");
                }

                renderer.WriteLine("[/tr]");
            }


            renderer.WriteLine("[/table]");
        }
        else
        {
            var impliciParagraph = renderer.ImplicitParagraph;

            renderer.ImplicitParagraph = true;
            foreach (var rowObj in table)
            {
                var row = (TableRow)rowObj;
                for (int i = 0; i < row.Count; i++)
                {
                    var cellObj = row[i];
                    var cell = (TableCell)cellObj;
                    renderer.Write(cell);
                    //write a space after each cell to avoid text being merged with the next cell
                    renderer.Write(' ');
                }
            }
            renderer.ImplicitParagraph = impliciParagraph;
        }
    }
}
