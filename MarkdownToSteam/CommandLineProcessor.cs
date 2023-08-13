using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace MarkdownToSteam
{
	public class CommandLineProcessor
	{
		public int Execute(string[] args)
		{
			CommandLineOptions? parsedOptions = null;

			try
			{
				ParserResult<CommandLineOptions>? parseResult = Parser.Default.ParseArguments<CommandLineOptions>(args)
					.WithParsed<CommandLineOptions>(options =>
					{
						parsedOptions = options;

						Uri? baseUri = GetBaseUri(options.BaseUri);

						using StreamReader stream = new StreamReader(options.ReadmeFile);

						using TextWriter writer = options.OutputFile == null ?
							Console.Out : new StreamWriter(options.OutputFile) { AutoFlush = true };

						new Converter().Convert(stream, writer, options.RemoveRelativeImages, baseUri);
					});

				if (parseResult.Errors.Any()) return 1;

				return 0;
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine($"The input file could not be found");
				if (parsedOptions is not null)
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

		/// <summary>
		/// Returns the absolute URI for the string version of the URI.
		/// Throws errors if there is a query string.  Fixes missing trailing /
		/// </summary>
		/// <param name="uriString"></param>
		/// <returns>if uriString is empty, will return null.  Else the Uri version of the URL </returns>
		/// <exception cref="ApplicationException"></exception>
		private Uri? GetBaseUri(string? uriString)
		{
			if (string.IsNullOrWhiteSpace(uriString))
			{
				return null;
			}

			Uri uri;

			if (!Uri.TryCreate(uriString, UriKind.Absolute, out uri!))
			{
				throw new ApplicationException("Base URI must be an absolute URI");
			}

			if (!string.IsNullOrEmpty(uri.Query))
			{
				throw new ApplicationException("Base Uri cannot have query parameters");
			}

			if (uri.Segments.Last() != "/")
			{
				//have to use a new URI as http://example.com/ and http://example.com both get parsed as having a trailing slash.
				//	but need an actual uri for the query check above.

				uri = new Uri(uriString + "/", UriKind.Absolute);
			}

			return uri;
		}
	}
}
