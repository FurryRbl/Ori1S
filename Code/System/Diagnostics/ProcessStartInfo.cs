using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Specifies a set of values that are used when you start a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000249 RID: 585
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public sealed class ProcessStartInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class without specifying a file name with which to start the process.</summary>
		// Token: 0x0600147C RID: 5244 RVA: 0x00036AE4 File Offset: 0x00034CE4
		public ProcessStartInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class and specifies a file name such as an application or document with which to start the process.</summary>
		/// <param name="fileName">An application or document with which to start a process. </param>
		// Token: 0x0600147D RID: 5245 RVA: 0x00036B38 File Offset: 0x00034D38
		public ProcessStartInfo(string filename)
		{
			this.filename = filename;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class, specifies an application file name with which to start the process, and specifies a set of command-line arguments to pass to the application.</summary>
		/// <param name="fileName">An application with which to start a process. </param>
		/// <param name="arguments">Command-line arguments to pass to the application when the process starts. </param>
		// Token: 0x0600147E RID: 5246 RVA: 0x00036B94 File Offset: 0x00034D94
		public ProcessStartInfo(string filename, string arguments)
		{
			this.filename = filename;
			this.arguments = arguments;
		}

		/// <summary>Gets or sets the set of command-line arguments to use when starting the application.</summary>
		/// <returns>File type–specific arguments that the system can associate with the application specified in the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property. The length of the arguments added to the length of the full path to the process must be less than 2080.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00036C04 File Offset: 0x00034E04
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x00036C0C File Offset: 0x00034E0C
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.DefaultValue("")]
		[MonitoringDescription("Command line agruments for this process.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public string Arguments
		{
			get
			{
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to start the process in a new window.</summary>
		/// <returns>true to start the process without creating a new window to contain it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00036C18 File Offset: 0x00034E18
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x00036C20 File Offset: 0x00034E20
		[System.ComponentModel.DefaultValue(false)]
		[MonitoringDescription("Start this process with a new window.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public bool CreateNoWindow
		{
			get
			{
				return this.create_no_window;
			}
			set
			{
				this.create_no_window = value;
			}
		}

		/// <summary>Gets search paths for files, directories for temporary files, application-specific options, and other similar information.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> that provides environment variables that apply to this process and child processes. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00036C2C File Offset: 0x00034E2C
		[MonitoringDescription("Environment variables used for this process.")]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
		[System.ComponentModel.DefaultValue(null)]
		[System.ComponentModel.Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public System.Collections.Specialized.StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.envVars == null)
				{
					this.envVars = new ProcessStringDictionary();
					foreach (object obj in Environment.GetEnvironmentVariables())
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						this.envVars.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
					}
				}
				return this.envVars;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00036CD4 File Offset: 0x00034ED4
		internal bool HaveEnvVars
		{
			get
			{
				return this.envVars != null;
			}
		}

		/// <summary>Gets or sets a value indicating whether an error dialog box is displayed to the user if the process cannot be started.</summary>
		/// <returns>true to display an error dialog box on the screen if the process cannot be started; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00036CE4 File Offset: 0x00034EE4
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x00036CEC File Offset: 0x00034EEC
		[System.ComponentModel.DefaultValue(false)]
		[MonitoringDescription("Thread shows dialogboxes for errors.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public bool ErrorDialog
		{
			get
			{
				return this.error_dialog;
			}
			set
			{
				this.error_dialog = value;
			}
		}

		/// <summary>Gets or sets the window handle to use when an error dialog box is shown for a process that cannot be started.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that identifies the handle of the error dialog box that results from a process start failure.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00036CF8 File Offset: 0x00034EF8
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x00036D00 File Offset: 0x00034F00
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public IntPtr ErrorDialogParentHandle
		{
			get
			{
				return this.error_dialog_parent_handle;
			}
			set
			{
				this.error_dialog_parent_handle = value;
			}
		}

		/// <summary>Gets or sets the application or document to start.</summary>
		/// <returns>The name of the application to start, or the name of a document of a file type that is associated with an application and that has a default open action available to it. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00036D0C File Offset: 0x00034F0C
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00036D14 File Offset: 0x00034F14
		[System.ComponentModel.DefaultValue("")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The name of the resource to start this process.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public string FileName
		{
			get
			{
				return this.filename;
			}
			set
			{
				this.filename = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <returns>true to write error output to <see cref="P:System.Diagnostics.Process.StandardError" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00036D20 File Offset: 0x00034F20
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00036D28 File Offset: 0x00034F28
		[System.ComponentModel.DefaultValue(false)]
		[System.ComponentModel.NotifyParentProperty(true)]
		[MonitoringDescription("Errors of this process are redirected.")]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirect_standard_error;
			}
			set
			{
				this.redirect_standard_error = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the input for an application is read from the <see cref="P:System.Diagnostics.Process.StandardInput" /> stream.</summary>
		/// <returns>true to read input from <see cref="P:System.Diagnostics.Process.StandardInput" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00036D34 File Offset: 0x00034F34
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00036D3C File Offset: 0x00034F3C
		[MonitoringDescription("Standard input of this process is redirected.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.DefaultValue(false)]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirect_standard_input;
			}
			set
			{
				this.redirect_standard_input = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <returns>true to write output to <see cref="P:System.Diagnostics.Process.StandardOutput" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00036D48 File Offset: 0x00034F48
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00036D50 File Offset: 0x00034F50
		[System.ComponentModel.DefaultValue(false)]
		[MonitoringDescription("Standart output of this process is redirected.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirect_standard_output;
			}
			set
			{
				this.redirect_standard_output = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for error output.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object that represents the preferred encoding for error output. The default is null.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00036D5C File Offset: 0x00034F5C
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00036D64 File Offset: 0x00034F64
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.encoding_stderr;
			}
			set
			{
				this.encoding_stderr = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for standard output.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object that represents the preferred encoding for standard output. The default is null.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00036D70 File Offset: 0x00034F70
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00036D78 File Offset: 0x00034F78
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.encoding_stdout;
			}
			set
			{
				this.encoding_stdout = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the operating system shell to start the process.</summary>
		/// <returns>true to use the shell when starting the process; otherwise, the process is created directly from the executable file. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00036D84 File Offset: 0x00034F84
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00036D8C File Offset: 0x00034F8C
		[System.ComponentModel.NotifyParentProperty(true)]
		[MonitoringDescription("Use the shell to start this process.")]
		[System.ComponentModel.DefaultValue(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.use_shell_execute;
			}
			set
			{
				this.use_shell_execute = value;
			}
		}

		/// <summary>Gets or sets the verb to use when opening the application or document specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The action to take with the file that the process opens. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00036D98 File Offset: 0x00034F98
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x00036DA0 File Offset: 0x00034FA0
		[MonitoringDescription("The verb to apply to a used document.")]
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.DefaultValue("")]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.VerbConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Verb
		{
			get
			{
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		/// <summary>Gets the set of verbs associated with the type of file specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The actions that the system can apply to the file indicated by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00036DAC File Offset: 0x00034FAC
		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public string[] Verbs
		{
			get
			{
				string text = (!(this.filename == null | this.filename.Length == 0)) ? Path.GetExtension(this.filename) : null;
				if (text == null)
				{
					return ProcessStartInfo.empty;
				}
				PlatformID platform = Environment.OSVersion.Platform;
				switch (platform)
				{
				case PlatformID.Unix:
				case PlatformID.MacOSX:
					break;
				default:
					if (platform != (PlatformID)128)
					{
						RegistryKey registryKey = null;
						RegistryKey registryKey2 = null;
						RegistryKey registryKey3 = null;
						string[] result;
						try
						{
							registryKey = Registry.ClassesRoot.OpenSubKey(text);
							string text2 = (registryKey == null) ? null : (registryKey.GetValue(null) as string);
							registryKey2 = ((text2 == null) ? null : Registry.ClassesRoot.OpenSubKey(text2));
							registryKey3 = ((registryKey2 == null) ? null : registryKey2.OpenSubKey("shell"));
							result = ((registryKey3 == null) ? null : registryKey3.GetSubKeyNames());
						}
						finally
						{
							if (registryKey3 != null)
							{
								registryKey3.Close();
							}
							if (registryKey2 != null)
							{
								registryKey2.Close();
							}
							if (registryKey != null)
							{
								registryKey.Close();
							}
						}
						return result;
					}
					break;
				}
				return ProcessStartInfo.empty;
			}
		}

		/// <summary>Gets or sets the window state to use when the process is started.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ProcessWindowStyle" /> that indicates whether the process is started in a window that is maximized, minimized, normal (neither maximized nor minimized), or not visible. The default is normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The window style is not one of the <see cref="T:System.Diagnostics.ProcessWindowStyle" /> enumeration members. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00036EEC File Offset: 0x000350EC
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x00036EF4 File Offset: 0x000350F4
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.DefaultValue(typeof(ProcessWindowStyle), "Normal")]
		[MonitoringDescription("The window style used to start this process.")]
		public ProcessWindowStyle WindowStyle
		{
			get
			{
				return this.window_style;
			}
			set
			{
				this.window_style = value;
			}
		}

		/// <summary>Gets or sets the initial directory for the process to be started.</summary>
		/// <returns>The fully qualified name of the directory that contains the process to be started. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00036F00 File Offset: 0x00035100
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x00036F08 File Offset: 0x00035108
		[System.ComponentModel.NotifyParentProperty(true)]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The initial directory for this process.")]
		[System.ComponentModel.Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[System.ComponentModel.RecommendedAsConfigurable(true)]
		[System.ComponentModel.DefaultValue("")]
		public string WorkingDirectory
		{
			get
			{
				return this.working_directory;
			}
			set
			{
				this.working_directory = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the Windows user profile is to be loaded from the registry. </summary>
		/// <returns>true to load the Windows user profile; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00036F24 File Offset: 0x00035124
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x00036F2C File Offset: 0x0003512C
		[System.ComponentModel.NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.load_user_profile;
			}
			set
			{
				this.load_user_profile = value;
			}
		}

		/// <summary>Gets or sets the user name to be used when starting the process.</summary>
		/// <returns>The user name to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00036F38 File Offset: 0x00035138
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x00036F40 File Offset: 0x00035140
		[System.ComponentModel.NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		/// <summary>Gets or sets a value that identifies the domain to use when starting the process. </summary>
		/// <returns>The Active Directory domain to use when starting the process. The domain property is primarily of interest to users within enterprise environments that use Active Directory.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00036F4C File Offset: 0x0003514C
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x00036F54 File Offset: 0x00035154
		[System.ComponentModel.NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets a secure string that contains the user password to use when starting the process.</summary>
		/// <returns>A <see cref="T:System.Security.SecureString" /> that contains the user password to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00036F60 File Offset: 0x00035160
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x00036F68 File Offset: 0x00035168
		public SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x04000629 RID: 1577
		private string arguments = string.Empty;

		// Token: 0x0400062A RID: 1578
		private IntPtr error_dialog_parent_handle = (IntPtr)0;

		// Token: 0x0400062B RID: 1579
		private string filename = string.Empty;

		// Token: 0x0400062C RID: 1580
		private string verb = string.Empty;

		// Token: 0x0400062D RID: 1581
		private string working_directory = string.Empty;

		// Token: 0x0400062E RID: 1582
		private ProcessStringDictionary envVars;

		// Token: 0x0400062F RID: 1583
		private bool create_no_window;

		// Token: 0x04000630 RID: 1584
		private bool error_dialog;

		// Token: 0x04000631 RID: 1585
		private bool redirect_standard_error;

		// Token: 0x04000632 RID: 1586
		private bool redirect_standard_input;

		// Token: 0x04000633 RID: 1587
		private bool redirect_standard_output;

		// Token: 0x04000634 RID: 1588
		private bool use_shell_execute = true;

		// Token: 0x04000635 RID: 1589
		private ProcessWindowStyle window_style;

		// Token: 0x04000636 RID: 1590
		private Encoding encoding_stderr;

		// Token: 0x04000637 RID: 1591
		private Encoding encoding_stdout;

		// Token: 0x04000638 RID: 1592
		private string username;

		// Token: 0x04000639 RID: 1593
		private string domain;

		// Token: 0x0400063A RID: 1594
		private SecureString password;

		// Token: 0x0400063B RID: 1595
		private bool load_user_profile;

		// Token: 0x0400063C RID: 1596
		private static readonly string[] empty = new string[0];
	}
}
