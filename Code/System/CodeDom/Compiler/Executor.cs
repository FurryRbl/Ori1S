using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides command execution functions for invoking compilers. This class cannot be inherited.</summary>
	// Token: 0x02000086 RID: 134
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public sealed class Executor
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x000118C4 File Offset: 0x0000FAC4
		private Executor()
		{
		}

		/// <summary>Executes the command using the specified temporary files and waits for the call to return.</summary>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		// Token: 0x06000597 RID: 1431 RVA: 0x000118CC File Offset: 0x0000FACC
		public static void ExecWait(string cmd, TempFileCollection tempFiles)
		{
			string text = null;
			string text2 = null;
			Executor.ExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref text, ref text2);
		}

		/// <summary>Executes the specified command using the specified user token, current directory, and temporary files; then waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="userToken">The token to start the compiler process with. </param>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="currentDir">The directory to start the process in. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06000598 RID: 1432 RVA: 0x000118F0 File Offset: 0x0000FAF0
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			int result;
			using (WindowsIdentity.Impersonate(userToken))
			{
				result = Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
			}
			return result;
		}

		/// <summary>Executes the specified command using the specified user token and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="userToken">The token to start the compiler process with. </param>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x06000599 RID: 1433 RVA: 0x00011948 File Offset: 0x0000FB48
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(userToken, cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified current directory and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="currentDir">The current directory. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x0600059A RID: 1434 RVA: 0x0001195C File Offset: 0x0000FB5C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static int ExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, currentDir, tempFiles, ref outputName, ref errorName);
		}

		/// <summary>Executes the specified command using the specified temporary files and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <returns>The return value from the compiler.</returns>
		/// <param name="cmd">The command to execute. </param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output. </param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered. </param>
		// Token: 0x0600059B RID: 1435 RVA: 0x0001196C File Offset: 0x0000FB6C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static int ExecWaitWithCapture(string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.InternalExecWaitWithCapture(cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001197C File Offset: 0x0000FB7C
		private static int InternalExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			if (cmd == null || cmd.Length == 0)
			{
				throw new ExternalException(Locale.GetText("No command provided for execution."));
			}
			if (outputName == null)
			{
				outputName = tempFiles.AddExtension("out");
			}
			if (errorName == null)
			{
				errorName = tempFiles.AddExtension("err");
			}
			int result = -1;
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			process.StartInfo.FileName = cmd;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.WorkingDirectory = currentDir;
			try
			{
				process.Start();
				Executor.ProcessResultReader processResultReader = new Executor.ProcessResultReader(process.StandardOutput, outputName);
				Executor.ProcessResultReader @object = new Executor.ProcessResultReader(process.StandardError, errorName);
				Thread thread = new Thread(new ThreadStart(@object.Read));
				thread.Start();
				processResultReader.Read();
				thread.Join();
				process.WaitForExit();
			}
			finally
			{
				result = process.ExitCode;
				process.Close();
			}
			return result;
		}

		// Token: 0x02000087 RID: 135
		private class ProcessResultReader
		{
			// Token: 0x0600059D RID: 1437 RVA: 0x00011AA8 File Offset: 0x0000FCA8
			public ProcessResultReader(StreamReader reader, string file)
			{
				this.reader = reader;
				this.file = file;
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x00011AC0 File Offset: 0x0000FCC0
			public void Read()
			{
				StreamWriter streamWriter = new StreamWriter(this.file);
				try
				{
					string value;
					while ((value = this.reader.ReadLine()) != null)
					{
						streamWriter.WriteLine(value);
					}
				}
				finally
				{
					streamWriter.Close();
				}
			}

			// Token: 0x0400015F RID: 351
			private StreamReader reader;

			// Token: 0x04000160 RID: 352
			private string file;
		}
	}
}
