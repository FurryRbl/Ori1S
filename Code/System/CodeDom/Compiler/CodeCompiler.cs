using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> interface.</summary>
	// Token: 0x02000076 RID: 118
	public abstract class CodeCompiler : CodeGenerator, ICodeCompiler
	{
		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDom(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit)" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the source to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x0600040E RID: 1038 RVA: 0x0000E020 File Offset: 0x0000C220
		CompilerResults ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit e)
		{
			return this.FromDom(options, e);
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDomBatch(System.CodeDom.Compiler.CompilerParameters,System.CodeDom.CodeCompileUnit[])" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile. </param>
		// Token: 0x0600040F RID: 1039 RVA: 0x0000E02C File Offset: 0x0000C22C
		CompilerResults ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			return this.FromDomBatch(options, ea);
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFile(System.CodeDom.Compiler.CompilerParameters,System.String)" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileName">The file name to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06000410 RID: 1040 RVA: 0x0000E038 File Offset: 0x0000C238
		CompilerResults ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
		{
			return this.FromFile(options, fileName);
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFileBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileNames">An array of strings that indicates the file names to compile. </param>
		// Token: 0x06000411 RID: 1041 RVA: 0x0000E044 File Offset: 0x0000C244
		CompilerResults ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
		{
			return this.FromFileBatch(options, fileNames);
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSource(System.CodeDom.Compiler.CompilerParameters,System.String)" />.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="source">A string that indicates the source code to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06000412 RID: 1042 RVA: 0x0000E050 File Offset: 0x0000C250
		CompilerResults ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
		{
			return this.FromSource(options, source);
		}

		/// <summary>For a description of this member, see <see cref="M:System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSourceBatch(System.CodeDom.Compiler.CompilerParameters,System.String[])" />. </summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="sources">An array of strings that indicates the source code to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06000413 RID: 1043 RVA: 0x0000E05C File Offset: 0x0000C25C
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			return this.FromSourceBatch(options, sources);
		}

		/// <summary>Gets the name of the compiler executable.</summary>
		/// <returns>The name of the compiler executable.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000414 RID: 1044
		protected abstract string CompilerName { get; }

		/// <summary>Gets the file name extension to use for source files.</summary>
		/// <returns>The file name extension to use for source files.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000415 RID: 1045
		protected abstract string FileExtension { get; }

		/// <summary>Gets the command arguments to be passed to the compiler from the specified <see cref="T:System.CodeDom.Compiler.CompilerParameters" />.</summary>
		/// <returns>The command arguments.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> that indicates the compiler options. </param>
		// Token: 0x06000416 RID: 1046
		protected abstract string CmdArgsFromParameters(CompilerParameters options);

		/// <summary>Compiles the specified compile unit using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> object that indicates the source to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x06000417 RID: 1047 RVA: 0x0000E068 File Offset: 0x0000C268
		protected virtual CompilerResults FromDom(CompilerParameters options, CodeCompileUnit e)
		{
			return this.FromDomBatch(options, new CodeCompileUnit[]
			{
				e
			});
		}

		/// <summary>Compiles the specified compile units using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="ea">An array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects that indicates the source to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.-or-<paramref name="ea" /> is null.</exception>
		// Token: 0x06000418 RID: 1048 RVA: 0x0000E07C File Offset: 0x0000C27C
		protected virtual CompilerResults FromDomBatch(CompilerParameters options, CodeCompileUnit[] ea)
		{
			string[] array = new string[ea.Length];
			int num = 0;
			if (options == null)
			{
				options = new CompilerParameters();
			}
			System.Collections.Specialized.StringCollection referencedAssemblies = options.ReferencedAssemblies;
			foreach (CodeCompileUnit codeCompileUnit in ea)
			{
				array[num] = Path.ChangeExtension(Path.GetTempFileName(), this.FileExtension);
				FileStream fileStream = new FileStream(array[num], FileMode.OpenOrCreate);
				StreamWriter streamWriter = new StreamWriter(fileStream);
				if (codeCompileUnit.ReferencedAssemblies != null)
				{
					foreach (string value in codeCompileUnit.ReferencedAssemblies)
					{
						if (!referencedAssemblies.Contains(value))
						{
							referencedAssemblies.Add(value);
						}
					}
				}
				((ICodeGenerator)this).GenerateCodeFromCompileUnit(codeCompileUnit, streamWriter, new CodeGeneratorOptions());
				streamWriter.Close();
				fileStream.Close();
				num++;
			}
			return this.Compile(options, array, false);
		}

		/// <summary>Compiles the specified file using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileName">The file name to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null. -or-<paramref name="fileName" /> is null.</exception>
		// Token: 0x06000419 RID: 1049 RVA: 0x0000E19C File Offset: 0x0000C39C
		protected virtual CompilerResults FromFile(CompilerParameters options, string fileName)
		{
			return this.FromFileBatch(options, new string[]
			{
				fileName
			});
		}

		/// <summary>Compiles the specified files using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="fileNames">An array of strings that indicates the file names of the files to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.-or-<paramref name="fileNames" /> is null.</exception>
		// Token: 0x0600041A RID: 1050 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		protected virtual CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			return this.Compile(options, fileNames, true);
		}

		/// <summary>Compiles the specified source code string using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="source">The source code string to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.</exception>
		// Token: 0x0600041B RID: 1051 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		protected virtual CompilerResults FromSource(CompilerParameters options, string source)
		{
			return this.FromSourceBatch(options, new string[]
			{
				source
			});
		}

		/// <summary>Compiles the specified source code strings using the specified options, and returns the results from the compilation.</summary>
		/// <returns>The results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="sources">An array of strings containing the source code to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="options" /> is null.-or-<paramref name="sources" /> is null.</exception>
		// Token: 0x0600041C RID: 1052 RVA: 0x0000E1D0 File Offset: 0x0000C3D0
		protected virtual CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			string[] array = new string[sources.Length];
			int num = 0;
			foreach (string value in sources)
			{
				array[num] = Path.ChangeExtension(Path.GetTempFileName(), this.FileExtension);
				FileStream fileStream = new FileStream(array[num], FileMode.OpenOrCreate);
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.Write(value);
				streamWriter.Close();
				fileStream.Close();
				num++;
			}
			return this.Compile(options, array, false);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000E254 File Offset: 0x0000C454
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		private CompilerResults Compile(CompilerParameters options, string[] fileNames, bool keepFiles)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			options.TempFiles = new TempFileCollection();
			foreach (string fileName in fileNames)
			{
				options.TempFiles.AddFile(fileName, keepFiles);
			}
			options.TempFiles.KeepFiles = keepFiles;
			string empty = string.Empty;
			string empty2 = string.Empty;
			string cmd = this.CompilerName + " " + this.CmdArgsFromParameters(options);
			CompilerResults compilerResults = new CompilerResults(new TempFileCollection());
			compilerResults.NativeCompilerReturnValue = Executor.ExecWaitWithCapture(cmd, options.TempFiles, ref empty, ref empty2);
			string[] array = empty.Split(Environment.NewLine.ToCharArray());
			foreach (string line in array)
			{
				this.ProcessCompilerOutputLine(compilerResults, line);
			}
			if (compilerResults.Errors.Count == 0)
			{
				compilerResults.PathToAssembly = options.OutputAssembly;
			}
			return compilerResults;
		}

		/// <summary>Gets the command arguments to use when invoking the compiler to generate a response file.</summary>
		/// <returns>The command arguments to use to generate a response file, or null if there are no response file arguments.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler options. </param>
		/// <param name="cmdArgs">A command arguments string. </param>
		// Token: 0x0600041E RID: 1054 RVA: 0x0000E36C File Offset: 0x0000C56C
		[MonoTODO]
		protected virtual string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Joins the specified string arrays.</summary>
		/// <returns>The concatenated string.</returns>
		/// <param name="sa">The array of strings to join. </param>
		/// <param name="separator">The separator to use. </param>
		// Token: 0x0600041F RID: 1055 RVA: 0x0000E374 File Offset: 0x0000C574
		protected static string JoinStringArray(string[] sa, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = sa.Length;
			if (num > 1)
			{
				for (int i = 0; i < num - 1; i++)
				{
					stringBuilder.Append("\"");
					stringBuilder.Append(sa[i]);
					stringBuilder.Append("\"");
					stringBuilder.Append(separator);
				}
			}
			if (num > 0)
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(sa[num - 1]);
				stringBuilder.Append("\"");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Processes the specified line from the specified <see cref="T:System.CodeDom.Compiler.CompilerResults" />.</summary>
		/// <param name="results">A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> that indicates the results of compilation. </param>
		/// <param name="line">The line to process. </param>
		// Token: 0x06000420 RID: 1056
		protected abstract void ProcessCompilerOutputLine(CompilerResults results, string line);
	}
}
