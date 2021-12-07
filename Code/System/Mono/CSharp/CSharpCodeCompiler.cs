using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Mono.CSharp
{
	// Token: 0x0200000B RID: 11
	internal class CSharpCodeCompiler : CSharpCodeGenerator, System.CodeDom.Compiler.ICodeCompiler
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002178 File Offset: 0x00000378
		public CSharpCodeCompiler()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002180 File Offset: 0x00000380
		public CSharpCodeCompiler(IDictionary<string, string> providerOptions) : base(providerOptions)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000218C File Offset: 0x0000038C
		static CSharpCodeCompiler()
		{
			if (Path.DirectorySeparatorChar == '\\')
			{
				PropertyInfo property = typeof(Environment).GetProperty("GacPath", BindingFlags.Static | BindingFlags.NonPublic);
				MethodInfo getMethod = property.GetGetMethod(true);
				string directoryName = Path.GetDirectoryName((string)getMethod.Invoke(null, null));
				CSharpCodeCompiler.windowsMonoPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(directoryName)), "bin\\mono.bat");
				if (!File.Exists(CSharpCodeCompiler.windowsMonoPath))
				{
					CSharpCodeCompiler.windowsMonoPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(directoryName)), "bin\\mono.exe");
				}
				if (!File.Exists(CSharpCodeCompiler.windowsMonoPath))
				{
					CSharpCodeCompiler.windowsMonoPath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(directoryName))), "mono\\mono\\mini\\mono.exe");
				}
				if (!File.Exists(CSharpCodeCompiler.windowsMonoPath))
				{
					throw new FileNotFoundException("Windows mono path not found: " + CSharpCodeCompiler.windowsMonoPath);
				}
				CSharpCodeCompiler.windowsMcsPath = Path.Combine(directoryName, "2.0\\gmcs.exe");
				if (!File.Exists(CSharpCodeCompiler.windowsMcsPath))
				{
					CSharpCodeCompiler.windowsMcsPath = Path.Combine(Path.GetDirectoryName(directoryName), "lib\\net_2_0\\gmcs.exe");
				}
				if (!File.Exists(CSharpCodeCompiler.windowsMcsPath))
				{
					throw new FileNotFoundException("Windows mcs path not found: " + CSharpCodeCompiler.windowsMcsPath);
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C4 File Offset: 0x000004C4
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromDom(System.CodeDom.Compiler.CompilerParameters options, System.CodeDom.CodeCompileUnit e)
		{
			return this.CompileAssemblyFromDomBatch(options, new System.CodeDom.CodeCompileUnit[]
			{
				e
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D8 File Offset: 0x000004D8
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002334 File Offset: 0x00000534
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromFile(System.CodeDom.Compiler.CompilerParameters options, string fileName)
		{
			return this.CompileAssemblyFromFileBatch(options, new string[]
			{
				fileName
			});
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002348 File Offset: 0x00000548
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

		// Token: 0x06000015 RID: 21 RVA: 0x000023A4 File Offset: 0x000005A4
		public System.CodeDom.Compiler.CompilerResults CompileAssemblyFromSource(System.CodeDom.Compiler.CompilerParameters options, string source)
		{
			return this.CompileAssemblyFromSourceBatch(options, new string[]
			{
				source
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023B8 File Offset: 0x000005B8
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

		// Token: 0x06000017 RID: 23 RVA: 0x00002414 File Offset: 0x00000614
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
			if (Path.DirectorySeparatorChar == '\\')
			{
				process.StartInfo.FileName = CSharpCodeCompiler.windowsMonoPath;
				process.StartInfo.Arguments = "\"" + CSharpCodeCompiler.windowsMcsPath + "\" " + CSharpCodeCompiler.BuildArgs(options, fileNames, base.ProviderOptions);
			}
			else
			{
				process.StartInfo.FileName = "gmcs";
				process.StartInfo.Arguments = CSharpCodeCompiler.BuildArgs(options, fileNames, base.ProviderOptions);
			}
			this.mcsOutput = new System.Collections.Specialized.StringCollection();
			this.mcsOutMutex = new Mutex();
			string text = Environment.GetEnvironmentVariable("MONO_PATH");
			if (text == null)
			{
				text = string.Empty;
			}
			string privateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
			if (privateBinPath != null && privateBinPath.Length > 0)
			{
				text = string.Format("{0}:{1}", privateBinPath, text);
			}
			if (text.Length > 0)
			{
				System.Collections.Specialized.StringDictionary environmentVariables = process.StartInfo.EnvironmentVariables;
				if (environmentVariables.ContainsKey("MONO_PATH"))
				{
					environmentVariables["MONO_PATH"] = text;
				}
				else
				{
					environmentVariables.Add("MONO_PATH", text);
				}
			}
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.ErrorDataReceived += this.McsStderrDataReceived;
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
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				compilerResults.NativeCompilerReturnValue = process.ExitCode;
			}
			finally
			{
				process.CancelErrorRead();
				process.CancelOutputRead();
				process.Close();
			}
			System.Collections.Specialized.StringCollection stringCollection = this.mcsOutput;
			bool flag = true;
			foreach (string error_string in this.mcsOutput)
			{
				System.CodeDom.Compiler.CompilerError compilerError = CSharpCodeCompiler.CreateErrorFromString(error_string);
				if (compilerError != null)
				{
					compilerResults.Errors.Add(compilerError);
					if (!compilerError.IsWarning)
					{
						flag = false;
					}
				}
			}
			if (stringCollection.Count > 0)
			{
				stringCollection.Insert(0, process.StartInfo.FileName + " " + process.StartInfo.Arguments + Environment.NewLine);
				compilerResults.Output = stringCollection;
			}
			if (flag)
			{
				if (!File.Exists(options.OutputAssembly))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string str in stringCollection)
					{
						stringBuilder.Append(str + Environment.NewLine);
					}
					throw new Exception("Compiler failed to produce the assembly. Output: '" + stringBuilder.ToString() + "'");
				}
				if (options.GenerateInMemory)
				{
					using (FileStream fileStream = File.OpenRead(options.OutputAssembly))
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						compilerResults.CompiledAssembly = Assembly.Load(array, null, options.Evidence);
						fileStream.Close();
					}
				}
				else
				{
					compilerResults.PathToAssembly = options.OutputAssembly;
				}
			}
			else
			{
				compilerResults.CompiledAssembly = null;
			}
			return compilerResults;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000287C File Offset: 0x00000A7C
		private void McsStderrDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs args)
		{
			if (args.Data != null)
			{
				this.mcsOutMutex.WaitOne();
				this.mcsOutput.Add(args.Data);
				this.mcsOutMutex.ReleaseMutex();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028C0 File Offset: 0x00000AC0
		private static string BuildArgs(System.CodeDom.Compiler.CompilerParameters options, string[] fileNames, IDictionary<string, string> providerOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (options.GenerateExecutable)
			{
				stringBuilder.Append("/target:exe ");
			}
			else
			{
				stringBuilder.Append("/target:library ");
			}
			string privateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
			if (privateBinPath != null && privateBinPath.Length > 0)
			{
				stringBuilder.AppendFormat("/lib:\"{0}\" ", privateBinPath);
			}
			if (options.Win32Resource != null)
			{
				stringBuilder.AppendFormat("/win32res:\"{0}\" ", options.Win32Resource);
			}
			if (options.IncludeDebugInformation)
			{
				stringBuilder.Append("/debug+ /optimize- ");
			}
			else
			{
				stringBuilder.Append("/debug- /optimize+ ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror ");
			}
			if (options.WarningLevel >= 0)
			{
				stringBuilder.AppendFormat("/warn:{0} ", options.WarningLevel);
			}
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string extension = (!options.GenerateExecutable) ? "dll" : "exe";
				options.OutputAssembly = CSharpCodeCompiler.GetTempFileNameWithExtension(options.TempFiles, extension, !options.GenerateInMemory);
			}
			stringBuilder.AppendFormat("/out:\"{0}\" ", options.OutputAssembly);
			foreach (string text in options.ReferencedAssemblies)
			{
				if (text != null && text.Length != 0)
				{
					stringBuilder.AppendFormat("/r:\"{0}\" ", text);
				}
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions);
				stringBuilder.Append(" ");
			}
			foreach (string arg in options.EmbeddedResources)
			{
				stringBuilder.AppendFormat("/resource:\"{0}\" ", arg);
			}
			foreach (string arg2 in options.LinkedResources)
			{
				stringBuilder.AppendFormat("/linkresource:\"{0}\" ", arg2);
			}
			if (providerOptions != null && providerOptions.Count > 0)
			{
				string text2;
				if (!providerOptions.TryGetValue("CompilerVersion", out text2))
				{
					text2 = "2.0";
				}
				if (text2.Length >= 1 && text2[0] == 'v')
				{
					text2 = text2.Substring(1);
				}
				string text3 = text2;
				if (text3 != null)
				{
					if (CSharpCodeCompiler.<>f__switch$map1 == null)
					{
						CSharpCodeCompiler.<>f__switch$map1 = new Dictionary<string, int>(2)
						{
							{
								"2.0",
								0
							},
							{
								"3.5",
								1
							}
						};
					}
					int num;
					if (CSharpCodeCompiler.<>f__switch$map1.TryGetValue(text3, out num))
					{
						if (num != 0)
						{
							if (num != 1)
							{
							}
						}
						else
						{
							stringBuilder.Append("/langversion:ISO-2");
						}
					}
				}
			}
			stringBuilder.Append(" -- ");
			foreach (string arg3 in fileNames)
			{
				stringBuilder.AppendFormat("\"{0}\" ", arg3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002C90 File Offset: 0x00000E90
		private static System.CodeDom.Compiler.CompilerError CreateErrorFromString(string error_string)
		{
			if (error_string.StartsWith("BETA"))
			{
				return null;
			}
			if (error_string == null || error_string == string.Empty)
			{
				return null;
			}
			System.CodeDom.Compiler.CompilerError compilerError = new System.CodeDom.Compiler.CompilerError();
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^(\\s*(?<file>.*)\\((?<line>\\d*)(,(?<column>\\d*))?\\)(:)?\\s+)*(?<level>\\w+)\\s*(?<number>.*):\\s(?<message>.*)", System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Compiled);
			System.Text.RegularExpressions.Match match = regex.Match(error_string);
			if (!match.Success)
			{
				compilerError.ErrorText = error_string;
				compilerError.IsWarning = false;
				compilerError.ErrorNumber = string.Empty;
				return compilerError;
			}
			if (string.Empty != match.Result("${file}"))
			{
				compilerError.FileName = match.Result("${file}");
			}
			if (string.Empty != match.Result("${line}"))
			{
				compilerError.Line = int.Parse(match.Result("${line}"));
			}
			if (string.Empty != match.Result("${column}"))
			{
				compilerError.Column = int.Parse(match.Result("${column}"));
			}
			string a = match.Result("${level}");
			if (a == "warning")
			{
				compilerError.IsWarning = true;
			}
			else if (a != "error")
			{
				return null;
			}
			compilerError.ErrorNumber = match.Result("${number}");
			compilerError.ErrorText = match.Result("${message}");
			return compilerError;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002DF0 File Offset: 0x00000FF0
		private static string GetTempFileNameWithExtension(System.CodeDom.Compiler.TempFileCollection temp_files, string extension, bool keepFile)
		{
			return temp_files.AddExtension(extension, keepFile);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002DFC File Offset: 0x00000FFC
		private static string GetTempFileNameWithExtension(System.CodeDom.Compiler.TempFileCollection temp_files, string extension)
		{
			return temp_files.AddExtension(extension);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E08 File Offset: 0x00001008
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
				array[i] = CSharpCodeCompiler.GetTempFileNameWithExtension(options.TempFiles, i + ".cs");
				FileStream fileStream = new FileStream(array[i], FileMode.OpenOrCreate);
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002F3C File Offset: 0x0000113C
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
				array[i] = CSharpCodeCompiler.GetTempFileNameWithExtension(options.TempFiles, i + ".cs");
				FileStream fileStream = new FileStream(array[i], FileMode.OpenOrCreate);
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
				{
					streamWriter.Write(sources[i]);
					streamWriter.Close();
				}
				fileStream.Close();
			}
			return this.CompileFromFileBatch(options, array);
		}

		// Token: 0x04000020 RID: 32
		private static string windowsMcsPath;

		// Token: 0x04000021 RID: 33
		private static string windowsMonoPath;

		// Token: 0x04000022 RID: 34
		private Mutex mcsOutMutex;

		// Token: 0x04000023 RID: 35
		private System.Collections.Specialized.StringCollection mcsOutput;
	}
}
