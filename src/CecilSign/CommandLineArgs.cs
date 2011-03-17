using System;
using Mono.Options;

namespace CecilSign
{
	public class CommandLineArgs
	{
		private OptionSet options;

		public string Assembly { get; set; }
		public string Key { get; set; }
		public string Out { get; set; }
		
		private bool _help;

		public bool Help
		{
			get { return _help || (string.IsNullOrEmpty(Assembly) || string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Out)); }
			set { _help = value; }
		}

		public CommandLineArgs()
		{
			options = new OptionSet {
				{ "a|assembly=", "Specifies the assembly to sign", x => Assembly = x },
				{ "k|key=", "Specifies the key file", x => Key = x },
				{ "o|out=" , "Specifies the output assembly", x => Out = x },
				{ "<>", "Writes help", x => _help = true}
			};
		}

		public void Parse(string[] args)
		{
			options.Parse(args);
		}

		public void WriteHelp()
		{
			Console.WriteLine("CecilSign -a <assembly> -k <key file> -o <output file>");
			Console.WriteLine();
			options.WriteOptionDescriptions(Console.Out);
		}
	}
}