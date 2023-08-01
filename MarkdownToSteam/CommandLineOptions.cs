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

		[Option('m', "render-relative-images", Required = false, HelpText = "Does not remove images that have a relative path.", Default = false)]
		public bool RenderRelativeImages { get; set; }

	}
}
