using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;

namespace CecilSign {
	class Program {
		static void Main(string[] args) {
			var arguments = new CommandLineArgs();
			arguments.Parse(args);

			if(arguments.Help)
			{
				arguments.WriteHelp();
				return;
			}

			try {
				var signatory = new Signatory(arguments.Assembly, arguments.Key, arguments.Out);
				signatory.Sign();
			}
			catch(Exception e)
			{
				Environment.ExitCode = -1;
				Console.WriteLine("An error occurred when running CecilSign.");
				Console.WriteLine(e);
			}
		
			Console.WriteLine("Signing complete.");
		}


		
	}
}
