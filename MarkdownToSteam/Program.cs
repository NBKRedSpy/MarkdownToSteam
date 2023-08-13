using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using CommandLine;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using MarkdownToSteam.Renderers;

namespace MarkdownToSteam
{
	internal class Program
	{
		static int Main(string[] args)
		{
			return new CommandLineProcessor().Execute(args);
		}

	}
}
