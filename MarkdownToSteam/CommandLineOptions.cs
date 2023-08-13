using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace MarkdownToSteam
{
	public  class CommandLineOptions
	{
		[Option('i', "input", Required = true, HelpText = "The full path to the ReadMe.md file to parse.")]
		public string ReadmeFile{ get; set; } = null!;

		[Option('o',"output", Required = false, HelpText = "The file to output the result to.  If not provided, will output to the console.")]
		public string? OutputFile { get; set;}

		[Option('r', "remove-relative-images", Required = false, HelpText = "Removes images that have a relative path")]
		public bool RemoveRelativeImages { get; set; }

		[Option('b', "base-url", Required = false, HelpText = "Any relative URI's will be converted to absolute URLs using this URL as the base.", Default = "")]
		public string BaseUri { get; set; } = string.Empty;


	}
}
