using System.Reflection;

namespace CecilSign
{
	using System.IO;
	using Mono.Cecil;

	public class Signatory
	{
		private string pathToAssembly;
		private string pathToSnk;
		private string outputFile;

		public Signatory(string pathToAssembly, string pathToSnk, string outputFile)
		{
			this.pathToAssembly = pathToAssembly;
			this.pathToSnk = pathToSnk;
			this.outputFile = outputFile;
		}

		public void Sign()
		{
			ValidateFilesExist();

			var snk = new StrongNameKeyPair(File.ReadAllBytes(pathToSnk));


			var assemblyDef = AssemblyDefinition.ReadAssembly(pathToAssembly);

			assemblyDef.Name.HashAlgorithm = AssemblyHashAlgorithm.SHA1;
			assemblyDef.Name.PublicKey = snk.PublicKey;
			assemblyDef.Name.HasPublicKey = true;
			assemblyDef.Name.Attributes &= AssemblyAttributes.PublicKey;

			assemblyDef.Write(outputFile);

			//Mono.Cecil.
			//assemblyDef.Name.Flags &= AssemblyFlags.PublicKey;
			//AssemblyFactory.SaveAssembly(assemblyDef, outputFile);
		}

		private void ValidateFilesExist()
		{
			if (! File.Exists(pathToAssembly))
			{
				throw new FileNotFoundException(string.Format("Could not find assembly to sign: '{0}'", pathToAssembly), pathToAssembly);
			}

			if (! File.Exists(pathToSnk))
			{
				throw new FileNotFoundException(string.Format("Could not find snk file: '{0}'", pathToAssembly), pathToAssembly);
			}
		}
	}
}