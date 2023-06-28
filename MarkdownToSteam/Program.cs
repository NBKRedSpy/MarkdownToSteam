using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using CommandLine;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;

namespace MarkdownToSteam
{
	internal class Program
	{
		static int Main(string[] args)
		{


			CommandLineOptions? parsedOptions = null;

			try
			{

				ParserResult<CommandLineOptions>? parseResult = Parser.Default.ParseArguments<CommandLineOptions>(args)
					.WithParsed<CommandLineOptions>(options =>
					{
						parsedOptions = options;
						Convert(options.ReadmeFile, options.OutputFile);
					});

				if (parseResult.Errors.Any()) return 1;

				return 0;
			}
			catch (FileNotFoundException fex)
			{
				Console.WriteLine($"The input file could not be found");
				if(parsedOptions is not null)
				{
					Console.WriteLine($"File: '{parsedOptions.ReadmeFile}'");
				}
				return 1;
			}
			catch (Exception ex)
			{
                Console.WriteLine("An error occurred:");
				Console.WriteLine(ex);
				return 1;
			}

		}

		static void Convert(string readmeFile, string? outputFile)
		{

			string text = File.ReadAllText(readmeFile);

			var pipe = new MarkdownPipelineBuilder()
				.UseAdvancedExtensions()
				.UseSoftlineBreakAsHardlineBreak()
				.Build();

			var mdDoc = Markdig.Markdown.Parse(text, pipe);


			using TextWriter writer = outputFile == null ? 
				Console.Out : new StreamWriter(outputFile);

			var renderer = new HtmlRenderer(writer);

			bool removeResult;

			pipe.Setup(renderer);
			removeResult = renderer.ObjectRenderers.Replace<ParagraphRenderer>(new ParagraphRenderBbCode());
			removeResult = renderer.ObjectRenderers.Replace<Markdig.Extensions.Tables.HtmlTableRenderer>(new HtmlTableRendererBBCode());
			removeResult = renderer.ObjectRenderers.Replace<ListRenderer>(new ListRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<HeadingRenderer>(new HeadingRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<LinkInlineRenderer>(new LinkInlineRendererBbCode());

			renderer.Render(mdDoc);
			writer.Flush();
		}
	}
}
