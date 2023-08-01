using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using CommandLine;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

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
						Convert(options.ReadmeFile, options.OutputFile, options.RenderRelativeImages);
					});

				if (parseResult.Errors.Any()) return 1;

				return 0;
			}
			catch (FileNotFoundException)
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

		static void Convert(string readmeFile, string? outputFile, bool renderRelativeImages)
		{


			//Debug
			using TextWriter writer = outputFile == null ?
				new StringWriter() : new StreamWriter(outputFile) { AutoFlush = true };
			//new StringWriter() : new StreamWriter(outputFile);

			Convert(readmeFile, writer, renderRelativeImages);

			if(outputFile is null)
			{
				Console.Write(writer.ToString());
			}

		}

		static void Convert(string readmeFile, TextWriter writer, bool renderRelativeImages)
		{

			string text = File.ReadAllText(readmeFile);

			var pipe = new MarkdownPipelineBuilder()
				.UseAdvancedExtensions()
				.UseSoftlineBreakAsHardlineBreak()
				.Build();

			var mdDoc = Markdig.Markdown.Parse(text, pipe);

			
			var renderer = new HtmlRenderer(writer);
			renderer.EnableHtmlEscape = false;

			renderer.ObjectWriteBefore += Renderer_ObjectWriteBefore;
			bool removeResult;

			pipe.Setup(renderer);
			
			removeResult = renderer.ObjectRenderers.Replace<ParagraphRenderer>(new ParagraphRenderBbCode());
			removeResult = renderer.ObjectRenderers.Replace<Markdig.Extensions.Tables.HtmlTableRenderer>(new HtmlTableRendererBBCode());
			removeResult = renderer.ObjectRenderers.Replace<ListRenderer>(new ListRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<HeadingRenderer>(new HeadingRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<LinkInlineRenderer>(new LinkInlineRendererBbCode(renderRelativeImages));
			removeResult = renderer.ObjectRenderers.Replace<LineBreakInlineRenderer>(new LineBreakInlineRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<EmphasisInlineRenderer>(new EmphasisInlineRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<QuoteBlockRenderer>(new QuoteBlockRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<CodeInlineRenderer>(new CodeInlineRendererBBCode());
			removeResult = renderer.ObjectRenderers.Replace<CodeBlockRenderer>(new CodeBlockBBCode());

			renderer.Render(mdDoc);
			writer.Flush();
		}

		private static void Renderer_ObjectWriteBefore(IMarkdownRenderer arg1, MarkdownObject arg2)
		{
		}
	}
}
