using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.VisualBasic
{
	// Token: 0x0200000E RID: 14
	internal class VBCodeCompiler : VBCodeGenerator, System.CodeDom.Compiler.ICodeCompiler
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00006090 File Offset: 0x00004290
		static VBCodeCompiler()
		{
			if (Path.DirectorySeparatorChar == '\\')
			{
				PropertyInfo property = typeof(Environment).GetProperty("GacPath", BindingFlags.Static | BindingFlags.NonPublic);
				MethodInfo getMethod = property.GetGetMethod(true);
				string directoryName = Path.GetDirectoryName((string)getMethod.Invoke(null, null));
				VBCodeCompiler.windowsMonoPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(directoryName)), "bin\\mono.bat");
				if (!File.Exists(VBCodeCompiler.windowsMonoPath))
				{
					VBCodeCompiler.windowsMonoPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(directoryName)), "bin\\mono.exe");
				}
				VBCodeCompiler.windowsvbncPath = Path.Combine(directoryName, "2.0\\vbnc.exe");
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00006130 File Offset: 0x00004330
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromDom(System.CodeDom.Compiler.CompilerParameters options, System.CodeDom.CodeCompileUnit e)
		{
			return this.CompileAssemblyFromDomBatch(options, new System.CodeDom.CodeCompileUnit[]
			{
				e
			});
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00006144 File Offset: 0x00004344
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromDomBatch(System.CodeDom.Compiler.CompilerParameters options, System.CodeDom.CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			System.CodeDom.Compiler.CompilerResults result;
			try
			{
				result = this.CompileFromDomBatch(options, ea);
			}
			finally
			{
				options.TempFiles.Delete();
			}
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000061A0 File Offset: 0x000043A0
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromFile(System.CodeDom.Compiler.CompilerParameters options, string fileName)
		{
			return this.CompileAssemblyFromFileBatch(options, new string[]
			{
				fileName
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000061B4 File Offset: 0x000043B4
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromFileBatch(System.CodeDom.Compiler.CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			System.CodeDom.Compiler.CompilerResults result;
			try
			{
				result = this.CompileFromFileBatch(options, fileNames);
			}
			finally
			{
				options.TempFiles.Delete();
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006210 File Offset: 0x00004410
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromSource(System.CodeDom.Compiler.CompilerParameters options, string source)
		{
			return this.CompileAssemblyFromSourceBatch(options, new string[]
			{
				source
			});
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006224 File Offset: 0x00004424
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromSourceBatch(System.CodeDom.Compiler.CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			System.CodeDom.Compiler.CompilerResults result;
			try
			{
				result = this.CompileFromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.Delete();
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006280 File Offset: 0x00004480
		private static string BuildArgs(System.CodeDom.Compiler.CompilerParameters options, string[] fileNames)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("/quiet ");
			if (options.GenerateExecutable)
			{
				stringBuilder.Append("/target:exe ");
			}
			else
			{
				stringBuilder.Append("/target:library ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror ");
			}
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string extension = (!options.GenerateExecutable) ? "dll" : "exe";
				options.OutputAssembly = VBCodeCompiler.GetTempFileNameWithExtension(options.TempFiles, extension, !options.GenerateInMemory);
			}
			stringBuilder.AppendFormat("/out:\"{0}\" ", options.OutputAssembly);
			bool flag = false;
			if (options.ReferencedAssemblies != null)
			{
				foreach (string text in options.ReferencedAssemblies)
				{
					if (string.Compare(text, "Microsoft.VisualBasic", true, CultureInfo.InvariantCulture) == 0)
					{
						flag = true;
					}
					stringBuilder.AppendFormat("/r:\"{0}\" ", text);
				}
			}
			if (!flag)
			{
				stringBuilder.Append("/r:\"Microsoft.VisualBasic.dll\" ");
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions);
				stringBuilder.Append(" ");
			}
			foreach (string arg in fileNames)
			{
				stringBuilder.AppendFormat(" \"{0}\" ", arg);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006440 File Offset: 0x00004640
		private static System.CodeDom.Compiler.CompilerError CreateErrorFromString(string error_string)
		{
			System.CodeDom.Compiler.CompilerError compilerError = new System.CodeDom.Compiler.CompilerError();
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^(\\s*(?<file>.*)?\\((?<line>\\d*)(,(?<column>\\d*))?\\)\\s+)?:\\s*(?<level>Error|Warning)?\\s*(?<number>.*):\\s(?<message>.*)", System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Compiled);
			System.Text.RegularExpressions.Match match = regex.Match(error_string);
			if (!match.Success)
			{
				return null;
			}
			if (string.Empty != match.Result("${file}"))
			{
				compilerError.FileName = match.Result("${file}").Trim();
			}
			if (string.Empty != match.Result("${line}"))
			{
				compilerError.Line = int.Parse(match.Result("${line}"));
			}
			if (string.Empty != match.Result("${column}"))
			{
				compilerError.Column = int.Parse(match.Result("${column}"));
			}
			if (match.Result("${level}").Trim() == "Warning")
			{
				compilerError.IsWarning = true;
			}
			compilerError.ErrorNumber = match.Result("${number}");
			compilerError.ErrorText = match.Result("${message}");
			return compilerError;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006550 File Offset: 0x00004750
		private static string GetTempFileNameWithExtension(System.CodeDom.Compiler.TempFileCollection temp_files, string extension, bool keepFile)
		{
			return temp_files.AddExtension(extension, keepFile);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000655C File Offset: 0x0000475C
		private static string GetTempFileNameWithExtension(System.CodeDom.Compiler.TempFileCollection temp_files, string extension)
		{
			return temp_files.AddExtension(extension);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006568 File Offset: 0x00004768
		private System.CodeDom.Compiler.CompilerResults CompileFromFileBatch(System.CodeDom.Compiler.CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			System.CodeDom.Compiler.CompilerResults compilerResults = new System.CodeDom.Compiler.CompilerResults(options.TempFiles);
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			string text = string.Empty;
			if (Path.DirectorySeparatorChar == '\\')
			{
				process.StartInfo.FileName = VBCodeCompiler.windowsMonoPath;
				process.StartInfo.Arguments = VBCodeCompiler.windowsvbncPath + ' ' + VBCodeCompiler.BuildArgs(options, fileNames);
			}
			else
			{
				process.StartInfo.FileName = "vbnc";
				process.StartInfo.Arguments = VBCodeCompiler.BuildArgs(options, fileNames);
			}
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				System.ComponentModel.Win32Exception ex2 = ex as System.ComponentModel.Win32Exception;
				if (ex2 != null)
				{
					throw new SystemException(string.Format("Error running {0}: {1}", process.StartInfo.FileName, System.ComponentModel.Win32Exception.W32ErrorMessage(ex2.NativeErrorCode)));
				}
				throw;
			}
			try
			{
				text = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
			}
			finally
			{
				compilerResults.NativeCompilerReturnValue = process.ExitCode;
				process.Close();
			}
			bool flag = true;
			if (compilerResults.NativeCompilerReturnValue == 1)
			{
				flag = false;
				string[] array = text.Split(Environment.NewLine.ToCharArray());
				foreach (string error_string in array)
				{
					System.CodeDom.Compiler.CompilerError compilerError = VBCodeCompiler.CreateErrorFromString(error_string);
					if (compilerError != null)
					{
						compilerResults.Errors.Add(compilerError);
					}
				}
			}
			if ((!flag && !compilerResults.Errors.HasErrors) || (compilerResults.NativeCompilerReturnValue != 0 && compilerResults.NativeCompilerReturnValue != 1))
			{
				flag = false;
				System.CodeDom.Compiler.CompilerError value = new System.CodeDom.Compiler.CompilerError(string.Empty, 0, 0, "VBNC_CRASH", text);
				compilerResults.Errors.Add(value);
			}
			if (flag)
			{
				if (options.GenerateInMemory)
				{
					using (FileStream fileStream = File.OpenRead(options.OutputAssembly))
					{
						byte[] array3 = new byte[fileStream.Length];
						fileStream.Read(array3, 0, array3.Length);
						compilerResults.CompiledAssembly = Assembly.Load(array3, null, options.Evidence);
						fileStream.Close();
					}
				}
				else
				{
					compilerResults.CompiledAssembly = Assembly.LoadFrom(options.OutputAssembly);
					compilerResults.PathToAssembly = options.OutputAssembly;
				}
			}
			else
			{
				compilerResults.CompiledAssembly = null;
			}
			return compilerResults;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006850 File Offset: 0x00004A50
		private System.CodeDom.Compiler.CompilerResults CompileFromDomBatch(System.CodeDom.Compiler.CompilerParameters options, System.CodeDom.CodeCompileUnit[] ea)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (ea == null)
			{
				throw new ArgumentNullException("ea");
			}
			string[] array = new string[ea.Length];
			System.Collections.Specialized.StringCollection referencedAssemblies = options.ReferencedAssemblies;
			for (int i = 0; i < ea.Length; i++)
			{
				System.CodeDom.CodeCompileUnit codeCompileUnit = ea[i];
				array[i] = VBCodeCompiler.GetTempFileNameWithExtension(options.TempFiles, i + ".vb");
				FileStream fileStream = new FileStream(array[i], FileMode.OpenOrCreate);
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
				((System.CodeDom.Compiler.ICodeGenerator)this).GenerateCodeFromCompileUnit(codeCompileUnit, streamWriter, new System.CodeDom.Compiler.CodeGeneratorOptions());
				streamWriter.Close();
				fileStream.Close();
			}
			return this.CompileAssemblyFromFileBatch(options, array);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000697C File Offset: 0x00004B7C
		private System.CodeDom.Compiler.CompilerResults CompileFromSourceBatch(System.CodeDom.Compiler.CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			string[] array = new string[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				array[i] = VBCodeCompiler.GetTempFileNameWithExtension(options.TempFiles, i + ".vb");
				FileStream fileStream = new FileStream(array[i], FileMode.OpenOrCreate);
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					streamWriter.Write(sources[i]);
					streamWriter.Close();
				}
				fileStream.Close();
			}
			return this.CompileFromFileBatch(options, array);
		}

		// Token: 0x0400002B RID: 43
		private static string windowsMonoPath;

		// Token: 0x0400002C RID: 44
		private static string windowsvbncPath;
	}
}
