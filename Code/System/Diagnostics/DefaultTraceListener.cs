using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides the default output methods and behavior for tracing.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000216 RID: 534
	public class DefaultTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DefaultTraceListener" /> class with "Default" as its <see cref="P:System.Diagnostics.TraceListener.Name" /> property value.</summary>
		// Token: 0x060011ED RID: 4589 RVA: 0x0002F62C File Offset: 0x0002D82C
		public DefaultTraceListener() : base("Default")
		{
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0002F63C File Offset: 0x0002D83C
		static DefaultTraceListener()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_TRACE_LISTENER");
				if (environmentVariable != null)
				{
					string monoTraceFile;
					string monoTracePrefix;
					if (environmentVariable.StartsWith("Console.Out"))
					{
						monoTraceFile = "Console.Out";
						monoTracePrefix = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Out");
					}
					else if (environmentVariable.StartsWith("Console.Error"))
					{
						monoTraceFile = "Console.Error";
						monoTracePrefix = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Error");
					}
					else
					{
						monoTraceFile = environmentVariable;
						monoTracePrefix = string.Empty;
					}
					DefaultTraceListener.MonoTraceFile = monoTraceFile;
					DefaultTraceListener.MonoTracePrefix = monoTracePrefix;
				}
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0002F6D8 File Offset: 0x0002D8D8
		private static string GetPrefix(string var, string target)
		{
			if (var.Length > target.Length)
			{
				return var.Substring(target.Length + 1);
			}
			return string.Empty;
		}

		/// <summary>Gets or sets a value indicating whether the application is running in user-interface mode.</summary>
		/// <returns>true if user-interface mode is enabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0002F70C File Offset: 0x0002D90C
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0002F714 File Offset: 0x0002D914
		public bool AssertUiEnabled
		{
			get
			{
				return this.assertUiEnabled;
			}
			set
			{
				this.assertUiEnabled = value;
			}
		}

		/// <summary>Gets or sets the name of a log file to write trace or debug messages to.</summary>
		/// <returns>The name of a log file to write trace or debug messages to.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0002F720 File Offset: 0x0002D920
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0002F728 File Offset: 0x0002D928
		[MonoTODO]
		public string LogFileName
		{
			get
			{
				return this.logFileName;
			}
			set
			{
				this.logFileName = value;
			}
		}

		/// <summary>Emits or displays a message and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011F4 RID: 4596 RVA: 0x0002F734 File Offset: 0x0002D934
		public override void Fail(string message)
		{
			base.Fail(message);
		}

		/// <summary>Emits or displays detailed messages and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <param name="detailMessage">The detailed message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060011F5 RID: 4597 RVA: 0x0002F740 File Offset: 0x0002D940
		public override void Fail(string message, string detailMessage)
		{
			base.Fail(message, detailMessage);
			if (this.ProcessUI(message, detailMessage) == DefaultTraceListener.DialogResult.Abort)
			{
				try
				{
					Thread.CurrentThread.Abort();
				}
				catch (MethodAccessException)
				{
				}
			}
			this.WriteLine(new StackTrace().ToString());
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		private DefaultTraceListener.DialogResult ProcessUI(string message, string detailMessage)
		{
			if (!this.AssertUiEnabled)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			object obj;
			MethodInfo method;
			try
			{
				Assembly assembly = Assembly.Load("System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
				if (assembly == null)
				{
					return DefaultTraceListener.DialogResult.None;
				}
				Type type = assembly.GetType("System.Windows.Forms.MessageBoxButtons");
				obj = Enum.Parse(type, "AbortRetryIgnore");
				method = assembly.GetType("System.Windows.Forms.MessageBox").GetMethod("Show", new Type[]
				{
					typeof(string),
					typeof(string),
					type
				});
			}
			catch
			{
				return DefaultTraceListener.DialogResult.None;
			}
			if (method == null || obj == null)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			string text = string.Format("Assertion Failed: {0} to quit, {1} to debug, {2} to continue", "Abort", "Retry", "Ignore");
			string text2 = string.Format("{0}{1}{2}{1}{1}{3}", new object[]
			{
				message,
				Environment.NewLine,
				detailMessage,
				new StackTrace()
			});
			string text3 = method.Invoke(null, new object[]
			{
				text2,
				text,
				obj
			}).ToString();
			if (text3 != null)
			{
				if (DefaultTraceListener.<>f__switch$map3 == null)
				{
					DefaultTraceListener.<>f__switch$map3 = new Dictionary<string, int>(2)
					{
						{
							"Ignore",
							0
						},
						{
							"Abort",
							1
						}
					};
				}
				int num;
				if (DefaultTraceListener.<>f__switch$map3.TryGetValue(text3, out num))
				{
					if (num == 0)
					{
						return DefaultTraceListener.DialogResult.Ignore;
					}
					if (num == 1)
					{
						return DefaultTraceListener.DialogResult.Abort;
					}
				}
			}
			return DefaultTraceListener.DialogResult.Retry;
		}

		// Token: 0x060011F7 RID: 4599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteWindowsDebugString(string message);

		// Token: 0x060011F8 RID: 4600 RVA: 0x0002F938 File Offset: 0x0002DB38
		private void WriteDebugString(string message)
		{
			if (DefaultTraceListener.OnWin32)
			{
				DefaultTraceListener.WriteWindowsDebugString(message);
			}
			else
			{
				this.WriteMonoTrace(message);
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0002F958 File Offset: 0x0002DB58
		private void WriteMonoTrace(string message)
		{
			string monoTraceFile = DefaultTraceListener.MonoTraceFile;
			if (monoTraceFile != null)
			{
				if (DefaultTraceListener.<>f__switch$map4 == null)
				{
					DefaultTraceListener.<>f__switch$map4 = new Dictionary<string, int>(2)
					{
						{
							"Console.Out",
							0
						},
						{
							"Console.Error",
							1
						}
					};
				}
				int num;
				if (DefaultTraceListener.<>f__switch$map4.TryGetValue(monoTraceFile, out num))
				{
					if (num == 0)
					{
						Console.Out.Write(message);
						return;
					}
					if (num == 1)
					{
						Console.Error.Write(message);
						return;
					}
				}
			}
			this.WriteLogFile(message, DefaultTraceListener.MonoTraceFile);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		private void WritePrefix()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				this.WriteMonoTrace(DefaultTraceListener.MonoTracePrefix);
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0002FA10 File Offset: 0x0002DC10
		private void WriteImpl(string message)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
				this.WritePrefix();
			}
			this.WriteDebugString(message);
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
			}
			this.WriteLogFile(message, this.LogFileName);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0002FA5C File Offset: 0x0002DC5C
		private void WriteLogFile(string message, string logFile)
		{
			try
			{
				this.WriteLogFileImpl(message, logFile);
			}
			catch (MethodAccessException)
			{
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0002FA98 File Offset: 0x0002DC98
		private void WriteLogFileImpl(string message, string logFile)
		{
			if (logFile != null && logFile.Length != 0)
			{
				FileInfo fileInfo = new FileInfo(logFile);
				StreamWriter streamWriter = null;
				try
				{
					if (fileInfo.Exists)
					{
						streamWriter = fileInfo.AppendText();
					}
					else
					{
						streamWriter = fileInfo.CreateText();
					}
				}
				catch
				{
					return;
				}
				using (streamWriter)
				{
					streamWriter.Write(message);
					streamWriter.Flush();
				}
			}
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method.</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060011FE RID: 4606 RVA: 0x0002FB44 File Offset: 0x0002DD44
		public override void Write(string message)
		{
			this.WriteImpl(message);
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method, followed by a carriage return and line feed (\r\n).</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060011FF RID: 4607 RVA: 0x0002FB50 File Offset: 0x0002DD50
		public override void WriteLine(string message)
		{
			string message2 = message + Environment.NewLine;
			this.WriteImpl(message2);
			base.NeedIndent = true;
		}

		// Token: 0x04000518 RID: 1304
		private const string ConsoleOutTrace = "Console.Out";

		// Token: 0x04000519 RID: 1305
		private const string ConsoleErrorTrace = "Console.Error";

		// Token: 0x0400051A RID: 1306
		private static readonly bool OnWin32 = Path.DirectorySeparatorChar == '\\';

		// Token: 0x0400051B RID: 1307
		private static readonly string MonoTracePrefix;

		// Token: 0x0400051C RID: 1308
		private static readonly string MonoTraceFile;

		// Token: 0x0400051D RID: 1309
		private string logFileName;

		// Token: 0x0400051E RID: 1310
		private bool assertUiEnabled;

		// Token: 0x02000217 RID: 535
		private enum DialogResult
		{
			// Token: 0x04000522 RID: 1314
			None,
			// Token: 0x04000523 RID: 1315
			Retry,
			// Token: 0x04000524 RID: 1316
			Ignore,
			// Token: 0x04000525 RID: 1317
			Abort
		}
	}
}
