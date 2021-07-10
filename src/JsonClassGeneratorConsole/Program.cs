using System;
using System.IO;
using Mono.Options;
using Xamasoft.JsonClassGenerator;

namespace JsonClassGeneratorConsole
{
	class Program
	{
		private static string _namespace = null;
		private static string _mainClassName = "MainObject";
		private static string _targetFolder = Environment.CurrentDirectory;
		private static bool _showHelp = false;
		private static bool _pascalCase = false;
		private static bool _singleFile = false;
		private static string _inputFilename;

		static int Main(string[] args)
		{
			var parameters = new OptionSet
				                 {
					                 {"?|help", "display help information", help => _showHelp = !String.IsNullOrWhiteSpace(help)},
					                 {"n|namespace=", "the namespace for the generated classes", n => _namespace = n},
					                 {"c|class=", "the main class name", cn => _mainClassName = cn},
					                 {"t|target=", "the target output folder", output => _targetFolder = output},
					                 {"p|pascal", "use PascalCase", pc => _pascalCase = !string.IsNullOrWhiteSpace(pc)},
					                 {"sf|single", "generate a single file", sf => _singleFile = !string.IsNullOrWhiteSpace(sf)},
									 {"i|input=", "input json file to process", json => _inputFilename = json}
				                 };

			try
			{
				parameters.Parse(args);
			}
			catch (Exception)
			{
				ShowHelp(parameters);
				return 1;
			}

			if (_showHelp)
			{
				ShowHelp(parameters);
				return 0;
			}

			var jsonFile = File.ReadAllText(_inputFilename);

			var gen = new JsonClassGenerator
				          {
					          Namespace = _namespace,
					          TargetFolder = _targetFolder,
					          MainClass = _mainClassName,
					          UsePascalCase = _pascalCase,
					          SingleFile = _singleFile,
							  Example = jsonFile
				          };

			using (var sw = new StringWriter())
			{
				gen.OutputStream = sw;
				gen.GenerateClasses();
				sw.Flush();
			}

			return 0;
		}

		static void ShowHelp(OptionSet p)
		{
			Console.WriteLine("Usage: json2cs [OPTIONS] input.json");
			Console.WriteLine("Converts a json file to a c# classes.");
			Console.WriteLine();
			Console.WriteLine("Options:");
			p.WriteOptionDescriptions(Console.Out);
		}
	}
}
