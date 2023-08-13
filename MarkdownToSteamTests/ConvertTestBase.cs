using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownToSteam;

namespace MarkdownToSteamTests
{
	public class ConvertTestBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="expected"></param>
		/// <param name="removeRelativeImages"></param>
		/// <param name="baseUri"></param>
		/// <param name="autoDocumentLinefeeds">If true, adds the \n's that are always added to the start
		/// and the end of the outputted document.</param>
		public void Run(string input, string expected, bool removeRelativeImages = false, Uri? baseUri = null)
		{
			Converter converter = new Converter();


			StringReader reader = new(input);
			StringWriter writer = new();

			converter.Convert(reader, writer, removeRelativeImages, baseUri);

			string actual = writer.ToString();

			//Replace CRLF with LF
			expected = expected.Replace("\r\n", "\n");

			Assert.Equal(expected, actual);

		}

		//-----------Template
		//		[Fact]
		//		public void NoOutputChange()
		//		{
		//			string input = @"
		//";

		//			string expected = @"
		//";

		//			Run(input, expected);
		//		}



	}
}
