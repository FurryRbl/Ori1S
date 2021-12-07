using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System
{
	/// <summary>Provides information about, and means to manipulate, the current environment and platform. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000132 RID: 306
	[ComVisible(true)]
	public static class Environment
	{
		/// <summary>Gets the command line for this process.</summary>
		/// <returns>A string containing command-line arguments.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="Path" />
		/// </PermissionSet>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x000455E8 File Offset: 0x000437E8
		public static string CommandLine
		{
			get
			{
				return string.Join(" ", Environment.GetCommandLineArgs());
			}
		}

		/// <summary>Gets or sets the fully qualified path of the current working directory.</summary>
		/// <returns>A string containing a directory path. </returns>
		/// <exception cref="T:System.ArgumentException">Attempted to set to an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">Attempted to set to null. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Attempted to set a local path that cannot be found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000455FC File Offset: 0x000437FC
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00045604 File Offset: 0x00043804
		public static string CurrentDirectory
		{
			get
			{
				return Directory.GetCurrentDirectory();
			}
			set
			{
				Directory.SetCurrentDirectory(value);
			}
		}

		/// <summary>Gets or sets the exit code of the process.</summary>
		/// <returns>A 32-bit signed integer containing the exit code. The default value is zero.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060010FB RID: 4347
		// (set) Token: 0x060010FC RID: 4348
		public static extern int ExitCode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		/// <summary>Gets a value indicating whether the common language runtime is shutting down or the current application domain is unloading.</summary>
		/// <returns>true if the common language runtime is shutting down or the current <see cref="T:System.AppDomain" /> is unloading; otherwise, false.The current application domain is the <see cref="T:System.AppDomain" /> that contains the object that is calling <see cref="P:System.Environment.HasShutdownStarted" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060010FD RID: 4349
		public static extern bool HasShutdownStarted { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060010FE RID: 4350
		public static extern string EmbeddingHostName { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060010FF RID: 4351
		public static extern bool SocketSecurityEnabled { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0004560C File Offset: 0x0004380C
		public static bool UnityWebSecurityEnabled
		{
			get
			{
				return Environment.SocketSecurityEnabled;
			}
		}

		/// <summary>Gets the NetBIOS name of this local computer.</summary>
		/// <returns>A string containing the name of this computer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The name of this computer cannot be obtained. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="COMPUTERNAME" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001101 RID: 4353
		public static extern string MachineName { [PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Read=\"COMPUTERNAME\"/>\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		/// <summary>Gets the newline string defined for this environment.</summary>
		/// <returns>A string containing "\r\n" for non-Unix platforms,  or a string containing "\n" for Unix platforms.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001102 RID: 4354
		public static extern string NewLine { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001103 RID: 4355
		internal static extern PlatformID Platform { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001104 RID: 4356
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetOSVersionString();

		/// <summary>Gets an <see cref="T:System.OperatingSystem" /> object that contains the current platform identifier and version number.</summary>
		/// <returns>An <see cref="T:System.OperatingSystem" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property was unable to obtain the system version.-or- The obtained platform identifier is not a member of <see cref="T:System.PlatformID" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00045614 File Offset: 0x00043814
		public static OperatingSystem OSVersion
		{
			get
			{
				if (Environment.os == null)
				{
					Version version = Version.CreateFromString(Environment.GetOSVersionString());
					PlatformID platform = Environment.Platform;
					Environment.os = new OperatingSystem(platform, version);
				}
				return Environment.os;
			}
		}

		/// <summary>Gets current stack trace information.</summary>
		/// <returns>A string containing stack trace information. This value can be <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The requested stack trace information is out of range. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00045650 File Offset: 0x00043850
		public static string StackTrace
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
			get
			{
				StackTrace stackTrace = new StackTrace(0, true);
				return stackTrace.ToString();
			}
		}

		/// <summary>Gets the fully qualified path of the system directory.</summary>
		/// <returns>A string containing a directory path.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0004566C File Offset: 0x0004386C
		public static string SystemDirectory
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.System);
			}
		}

		/// <summary>Gets the number of milliseconds elapsed since the system started.</summary>
		/// <returns>A 32-bit signed integer containing the amount of time in milliseconds that has passed since the last time the computer was started.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001108 RID: 4360
		public static extern int TickCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		/// <summary>Gets the network domain name associated with the current user.</summary>
		/// <returns>The network domain name associated with the current user.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system does not support retrieving the network domain name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The network domain name cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="UserName;UserDomainName" />
		/// </PermissionSet>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00045678 File Offset: 0x00043878
		public static string UserDomainName
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Read=\"USERDOMAINNAME\"/>\n</PermissionSet>\n")]
			get
			{
				return Environment.MachineName;
			}
		}

		/// <summary>Gets a value indicating whether the current process is running in user interactive mode.</summary>
		/// <returns>true if the current process is running in user interactive mode; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00045680 File Offset: 0x00043880
		[MonoTODO("Currently always returns false, regardless of interactive state")]
		public static bool UserInteractive
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the user name of the person who is currently logged on to the Windows operating system.</summary>
		/// <returns>The user name of the person who is logged on to Windows.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="UserName" />
		/// </PermissionSet>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600110B RID: 4363
		public static extern string UserName { [PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Read=\"USERNAME;USER\"/>\n</PermissionSet>\n")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		/// <summary>Gets a <see cref="T:System.Version" /> object that describes the major, minor, build, and revision numbers of the common language runtime.</summary>
		/// <returns>A <see cref="T:System.Version" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00045684 File Offset: 0x00043884
		public static Version Version
		{
			get
			{
				return new Version("2.0.50727.1433");
			}
		}

		/// <summary>Gets the amount of physical memory mapped to the process context.</summary>
		/// <returns>A 64-bit signed integer containing the number of bytes of physical memory mapped to the process context.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00045690 File Offset: 0x00043890
		[MonoTODO("Currently always returns zero")]
		public static long WorkingSet
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
			get
			{
				return 0L;
			}
		}

		/// <summary>Terminates this process and gives the underlying operating system the specified exit code.</summary>
		/// <param name="exitCode">Exit code to be given to the operating system. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient security permission to perform this function. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600110E RID: 4366
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(int exitCode);

		/// <summary>Replaces the name of each environment variable embedded in the specified string with the string equivalent of the value of the variable, then returns the resulting string.</summary>
		/// <returns>A string with each environment variable replaced by its value.</returns>
		/// <param name="name">A string containing the names of zero or more environment variables. Each environment variable is quoted with the percent sign character (%). </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600110F RID: 4367 RVA: 0x00045694 File Offset: 0x00043894
		public static string ExpandEnvironmentVariables(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num = name.IndexOf('%');
			if (num == -1)
			{
				return name;
			}
			int length = name.Length;
			int num2;
			if (num == length - 1 || (num2 = name.IndexOf('%', num + 1)) == -1)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(name, 0, num);
			Hashtable hashtable = null;
			do
			{
				string text = name.Substring(num + 1, num2 - num - 1);
				string text2 = Environment.GetEnvironmentVariable(text);
				if (text2 == null && Environment.IsRunningOnWindows)
				{
					if (hashtable == null)
					{
						hashtable = Environment.GetEnvironmentVariablesNoCase();
					}
					text2 = (hashtable[text] as string);
				}
				if (text2 == null)
				{
					stringBuilder.Append('%');
					stringBuilder.Append(text);
					num2--;
				}
				else
				{
					stringBuilder.Append(text2);
				}
				int num3 = num2;
				num = name.IndexOf('%', num2 + 1);
				num2 = ((num != -1 && num2 <= length - 1) ? name.IndexOf('%', num + 1) : -1);
				int count;
				if (num == -1 || num2 == -1)
				{
					count = length - num3 - 1;
				}
				else if (text2 != null)
				{
					count = num - num3 - 1;
				}
				else
				{
					count = num - num3;
				}
				if (num >= num3 || num == -1)
				{
					stringBuilder.Append(name, num3 + 1, count);
				}
			}
			while (num2 > -1 && num2 < length);
			return stringBuilder.ToString();
		}

		/// <summary>Returns a string array containing the command-line arguments for the current process.</summary>
		/// <returns>An array of string where each element contains a command-line argument. The first element is the executable file name, and the following zero or more elements contain the remaining command-line arguments.</returns>
		/// <exception cref="T:System.NotSupportedException">The system does not support command-line arguments. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="Path" />
		/// </PermissionSet>
		// Token: 0x06001110 RID: 4368
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Read=\"PATH\"/>\n</PermissionSet>\n")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetCommandLineArgs();

		// Token: 0x06001111 RID: 4369
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetEnvironmentVariable(string variable);

		/// <summary>Retrieves the value of an environment variable from the current process.</summary>
		/// <returns>The value of the environment variable specified by <paramref name="variable" />, or null if the environment variable is not found.</returns>
		/// <param name="variable">The name of the environment variable. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001112 RID: 4370 RVA: 0x00045808 File Offset: 0x00043A08
		public static string GetEnvironmentVariable(string variable)
		{
			if (SecurityManager.SecurityEnabled)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, variable).Demand();
			}
			return Environment.internalGetEnvironmentVariable(variable);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00045828 File Offset: 0x00043A28
		private static Hashtable GetEnvironmentVariablesNoCase()
		{
			Hashtable hashtable = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
			}
			return hashtable;
		}

		/// <summary>Retrieves all environment variable names and their values from the current process.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains all environment variable names and their values; otherwise, an empty dictionary if no environment variables are found.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The buffer is out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001114 RID: 4372 RVA: 0x00045874 File Offset: 0x00043A74
		public static IDictionary GetEnvironmentVariables()
		{
			StringBuilder stringBuilder = null;
			if (SecurityManager.SecurityEnabled)
			{
				stringBuilder = new StringBuilder();
			}
			Hashtable hashtable = new Hashtable();
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
				if (stringBuilder != null)
				{
					stringBuilder.Append(text);
					stringBuilder.Append(";");
				}
			}
			if (stringBuilder != null)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
			}
			return hashtable;
		}

		// Token: 0x06001115 RID: 4373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetWindowsFolderPath(int folder);

		/// <summary>Gets the path to the system special folder identified by the specified enumeration.</summary>
		/// <returns>The path to the specified system special folder, if that folder physically exists on your computer; otherwise, the empty string ("").A folder will not physically exist if the operating system did not create it, the existing folder was deleted, or the folder is a virtual directory, such as My Computer, which does not correspond to a physical path.</returns>
		/// <param name="folder">An enumerated constant that identifies a system special folder. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="folder" /> is not a member of <see cref="T:System.Environment.SpecialFolder" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001116 RID: 4374 RVA: 0x000458FC File Offset: 0x00043AFC
		public static string GetFolderPath(Environment.SpecialFolder folder)
		{
			string text;
			if (Environment.IsRunningOnWindows)
			{
				text = Environment.GetWindowsFolderPath((int)folder);
			}
			else
			{
				text = Environment.InternalGetFolderPath(folder);
			}
			if (text != null && text.Length > 0 && SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
			}
			return text;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00045954 File Offset: 0x00043B54
		private static string ReadXdgUserDir(string config_dir, string home_dir, string key, string fallback)
		{
			string text = Environment.internalGetEnvironmentVariable(key);
			if (text != null && text != string.Empty)
			{
				return text;
			}
			string path = Path.Combine(config_dir, "user-dirs.dirs");
			if (!File.Exists(path))
			{
				return Path.Combine(home_dir, fallback);
			}
			try
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					string text2;
					while ((text2 = streamReader.ReadLine()) != null)
					{
						text2 = text2.Trim();
						int num = text2.IndexOf('=');
						if (num > 8 && text2.Substring(0, num) == key)
						{
							string text3 = text2.Substring(num + 1).Trim(new char[]
							{
								'"'
							});
							bool flag = false;
							if (text3.StartsWith("$HOME/"))
							{
								flag = true;
								text3 = text3.Substring(6);
							}
							else if (!text3.StartsWith("/"))
							{
								flag = true;
							}
							return (!flag) ? text3 : Path.Combine(home_dir, text3);
						}
					}
				}
			}
			catch (FileNotFoundException)
			{
			}
			return Path.Combine(home_dir, fallback);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00045AB4 File Offset: 0x00043CB4
		internal static string InternalGetFolderPath(Environment.SpecialFolder folder)
		{
			string text = Environment.internalGetHome();
			string text2 = Environment.internalGetEnvironmentVariable("XDG_DATA_HOME");
			if (text2 == null || text2 == string.Empty)
			{
				text2 = Path.Combine(text, ".local");
				text2 = Path.Combine(text2, "share");
			}
			string text3 = Environment.internalGetEnvironmentVariable("XDG_CONFIG_HOME");
			if (text3 == null || text3 == string.Empty)
			{
				text3 = Path.Combine(text, ".config");
			}
			switch (folder)
			{
			case Environment.SpecialFolder.Desktop:
			case Environment.SpecialFolder.DesktopDirectory:
				return Environment.ReadXdgUserDir(text3, text, "XDG_DESKTOP_DIR", "Desktop");
			case Environment.SpecialFolder.Programs:
			case Environment.SpecialFolder.Favorites:
			case Environment.SpecialFolder.Startup:
			case Environment.SpecialFolder.Recent:
			case Environment.SpecialFolder.SendTo:
			case Environment.SpecialFolder.StartMenu:
			case Environment.SpecialFolder.Templates:
			case Environment.SpecialFolder.InternetCache:
			case Environment.SpecialFolder.Cookies:
			case Environment.SpecialFolder.History:
			case Environment.SpecialFolder.System:
			case Environment.SpecialFolder.ProgramFiles:
			case Environment.SpecialFolder.CommonProgramFiles:
				return string.Empty;
			case Environment.SpecialFolder.MyDocuments:
				return text;
			case Environment.SpecialFolder.MyMusic:
				return Environment.ReadXdgUserDir(text3, text, "XDG_MUSIC_DIR", "Music");
			case Environment.SpecialFolder.MyComputer:
				return string.Empty;
			case Environment.SpecialFolder.ApplicationData:
				return text3;
			case Environment.SpecialFolder.LocalApplicationData:
				return text2;
			case Environment.SpecialFolder.CommonApplicationData:
				return "/usr/share";
			case Environment.SpecialFolder.MyPictures:
				return Environment.ReadXdgUserDir(text3, text, "XDG_PICTURES_DIR", "Pictures");
			}
			throw new ArgumentException("Invalid SpecialFolder");
		}

		/// <summary>Returns an array of string containing the names of the logical drives on the current computer.</summary>
		/// <returns>An array of strings where each element contains the name of a logical drive. For example, if the computer's hard drive is the first logical drive, the first element returned is "C:\".</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001119 RID: 4377 RVA: 0x00045C44 File Offset: 0x00043E44
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
		public static string[] GetLogicalDrives()
		{
			return Environment.GetLogicalDrivesInternal();
		}

		// Token: 0x0600111A RID: 4378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void internalBroadcastSettingChange();

		/// <summary>Retrieves the value of an environment variable from the current process or from the Windows operating system registry key for the current user or local machine.</summary>
		/// <returns>The value of the environment variable specified by the <paramref name="variable" /> and <paramref name="target" /> parameters, or null if the environment variable is not found.</returns>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.User" /> or <see cref="F:System.EnvironmentVariableTarget.Machine" /> and the current operating system is Windows 95, Windows 98, or Windows Me.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a valid <see cref="T:System.EnvironmentVariableTarget" /> value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600111B RID: 4379 RVA: 0x00045C4C File Offset: 0x00043E4C
		public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
		{
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				return Environment.GetEnvironmentVariable(variable);
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (!Environment.IsRunningOnWindows)
				{
					return null;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
				{
					object value = registryKey.GetValue(variable);
					return (value != null) ? value.ToString() : null;
				}
				break;
			default:
				goto IL_D7;
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (!Environment.IsRunningOnWindows)
			{
				return null;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
			{
				object value2 = registryKey2.GetValue(variable);
				return (value2 != null) ? value2.ToString() : null;
			}
			IL_D7:
			throw new ArgumentException("target");
		}

		/// <summary>Retrieves all environment variable names and their values from the current process, or from the Windows operating system registry key for the current user or local machine.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> object that contains all environment variable names and their values from the source specified by the <paramref name="target" /> parameter; otherwise, an empty dictionary if no environment variables are found.</returns>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation for the specified value of <paramref name="target" />.</exception>
		/// <exception cref="T:System.NotSupportedException">This method cannot be used on Windows 95 or Windows 98 platforms.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> contains an illegal value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600111C RID: 4380 RVA: 0x00045D74 File Offset: 0x00043F74
		public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
		{
			IDictionary dictionary = new Hashtable();
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				dictionary = Environment.GetEnvironmentVariables();
				break;
			case EnvironmentVariableTarget.User:
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (Environment.IsRunningOnWindows)
				{
					using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Environment"))
					{
						string[] valueNames = registryKey.GetValueNames();
						foreach (string text in valueNames)
						{
							dictionary.Add(text, registryKey.GetValue(text));
						}
					}
				}
				break;
			case EnvironmentVariableTarget.Machine:
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (Environment.IsRunningOnWindows)
				{
					using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
					{
						string[] valueNames2 = registryKey2.GetValueNames();
						foreach (string text2 in valueNames2)
						{
							dictionary.Add(text2, registryKey2.GetValue(text2));
						}
					}
				}
				break;
			default:
				throw new ArgumentException("target");
			}
			return dictionary;
		}

		/// <summary>Creates, modifies, or deletes an environment variable stored in the current process.</summary>
		/// <param name="variable">The name of an environment variable. </param>
		/// <param name="value">A value to assign to <paramref name="variable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="variable" /> contains a zero-length string, an initial hexadecimal zero character (0x00), or an equal sign ("="). -or-The length of <paramref name="variable" /> or <paramref name="value" /> is greater than or equal to 32,767 characters.-or-An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600111D RID: 4381 RVA: 0x00045EE0 File Offset: 0x000440E0
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
		public static void SetEnvironmentVariable(string variable, string value)
		{
			Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Process);
		}

		/// <summary>Creates, modifies, or deletes an environment variable stored in the current process or in the Windows operating system registry key reserved for the current user or local machine.</summary>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="value">A value to assign to <paramref name="variable" />. </param>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="variable" /> contains a zero-length string, an initial hexadecimal zero character (0x00), or an equal sign ("="). -or-The length of <paramref name="variable" /> is greater than or equal to 32,767 characters.-or-<paramref name="target" /> is not a member of the <see cref="T:System.EnvironmentVariableTarget" /> enumeration. -or-<paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.Machine" /> or <see cref="F:System.EnvironmentVariableTarget.User" /> and the length of <paramref name="variable" /> is greater than or equal to 255.-or-<paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.Process" /> and the length of <paramref name="value" /> is greater than or equal to 32,767 characters. -or-An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.User" /> or <see cref="F:System.EnvironmentVariableTarget.Machine" /> and the current operating system is Windows 95, Windows 98, or Windows Me.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600111E RID: 4382 RVA: 0x00045EEC File Offset: 0x000440EC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
		public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (variable == string.Empty)
			{
				throw new ArgumentException("String cannot be of zero length.", "variable");
			}
			if (variable.IndexOf('=') != -1)
			{
				throw new ArgumentException("Environment variable name cannot contain an equal character.", "variable");
			}
			if (variable[0] == '\0')
			{
				throw new ArgumentException("The first char in the string is the null character.", "variable");
			}
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				Environment.InternalSetEnvironmentVariable(variable, value);
				break;
			case EnvironmentVariableTarget.User:
				if (!Environment.IsRunningOnWindows)
				{
					return;
				}
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Environment", true))
				{
					if (string.IsNullOrEmpty(value))
					{
						registryKey.DeleteValue(variable, false);
					}
					else
					{
						registryKey.SetValue(variable, value);
					}
					Environment.internalBroadcastSettingChange();
				}
				break;
			case EnvironmentVariableTarget.Machine:
				if (!Environment.IsRunningOnWindows)
				{
					return;
				}
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
				{
					if (string.IsNullOrEmpty(value))
					{
						registryKey2.DeleteValue(variable, false);
					}
					else
					{
						registryKey2.SetValue(variable, value);
					}
					Environment.internalBroadcastSettingChange();
				}
				break;
			default:
				throw new ArgumentException("target");
			}
		}

		// Token: 0x0600111F RID: 4383
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalSetEnvironmentVariable(string variable, string value);

		/// <summary>Terminates a process but does not execute any active try-finally blocks or finalizers.</summary>
		/// <param name="message">A message that explains why the process was terminated, or null if no explanation is provided. </param>
		// Token: 0x06001120 RID: 4384 RVA: 0x00046078 File Offset: 0x00044278
		[MonoTODO("Not implemented")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static void FailFast(string message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the number of processors on the current machine.</summary>
		/// <returns>The 32-bit signed integer that specifies the number of processors on the current machine. There is no default.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="NUMBER_OF_PROCESSORS" />
		/// </PermissionSet>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001121 RID: 4385
		public static extern int ProcessorCount { [PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Read=\"NUMBER_OF_PROCESSORS\"/>\n</PermissionSet>\n")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00046080 File Offset: 0x00044280
		internal static bool IsRunningOnWindows
		{
			get
			{
				return Environment.Platform < PlatformID.Unix;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0004608C File Offset: 0x0004428C
		private static string GacPath
		{
			get
			{
				if (Environment.IsRunningOnWindows)
				{
					string fullName = new DirectoryInfo(Path.GetDirectoryName(typeof(int).Assembly.Location)).Parent.Parent.FullName;
					return Path.Combine(Path.Combine(fullName, "mono"), "gac");
				}
				return Path.Combine(Path.Combine(Environment.internalGetGacPath(), "mono"), "gac");
			}
		}

		// Token: 0x06001124 RID: 4388
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetGacPath();

		// Token: 0x06001125 RID: 4389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetLogicalDrivesInternal();

		// Token: 0x06001126 RID: 4390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetEnvironmentVariableNames();

		// Token: 0x06001127 RID: 4391
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetMachineConfigPath();

		// Token: 0x06001128 RID: 4392
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetHome();

		// Token: 0x040004DF RID: 1247
		private const int mono_corlib_version = 82;

		// Token: 0x040004E0 RID: 1248
		private static OperatingSystem os;

		/// <summary>Specifies enumerated constants used to retrieve directory paths to system special folders.</summary>
		// Token: 0x02000133 RID: 307
		[ComVisible(true)]
		public enum SpecialFolder
		{
			/// <summary>The "My Documents" folder.</summary>
			// Token: 0x040004E2 RID: 1250
			MyDocuments = 5,
			/// <summary>The logical Desktop rather than the physical file system location.</summary>
			// Token: 0x040004E3 RID: 1251
			Desktop = 0,
			/// <summary>The "My Computer" folder. </summary>
			// Token: 0x040004E4 RID: 1252
			MyComputer = 17,
			/// <summary>The directory that contains the user's program groups.</summary>
			// Token: 0x040004E5 RID: 1253
			Programs = 2,
			/// <summary>The directory that serves as a common repository for documents.</summary>
			// Token: 0x040004E6 RID: 1254
			Personal = 5,
			/// <summary>The directory that serves as a common repository for the user's favorite items.</summary>
			// Token: 0x040004E7 RID: 1255
			Favorites,
			/// <summary>The directory that corresponds to the user's Startup program group.</summary>
			// Token: 0x040004E8 RID: 1256
			Startup,
			/// <summary>The directory that contains the user's most recently used documents.</summary>
			// Token: 0x040004E9 RID: 1257
			Recent,
			/// <summary>The directory that contains the Send To menu items.</summary>
			// Token: 0x040004EA RID: 1258
			SendTo,
			/// <summary>The directory that contains the Start menu items.</summary>
			// Token: 0x040004EB RID: 1259
			StartMenu = 11,
			/// <summary>The "My Music" folder.</summary>
			// Token: 0x040004EC RID: 1260
			MyMusic = 13,
			/// <summary>The directory used to physically store file objects on the desktop.</summary>
			// Token: 0x040004ED RID: 1261
			DesktopDirectory = 16,
			/// <summary>The directory that serves as a common repository for document templates.</summary>
			// Token: 0x040004EE RID: 1262
			Templates = 21,
			/// <summary>The directory that serves as a common repository for application-specific data for the current roaming user.</summary>
			// Token: 0x040004EF RID: 1263
			ApplicationData = 26,
			/// <summary>The directory that serves as a common repository for application-specific data that is used by the current, non-roaming user.</summary>
			// Token: 0x040004F0 RID: 1264
			LocalApplicationData = 28,
			/// <summary>The directory that serves as a common repository for temporary Internet files.</summary>
			// Token: 0x040004F1 RID: 1265
			InternetCache = 32,
			/// <summary>The directory that serves as a common repository for Internet cookies.</summary>
			// Token: 0x040004F2 RID: 1266
			Cookies,
			/// <summary>The directory that serves as a common repository for Internet history items.</summary>
			// Token: 0x040004F3 RID: 1267
			History,
			/// <summary>The directory that serves as a common repository for application-specific data that is used by all users.</summary>
			// Token: 0x040004F4 RID: 1268
			CommonApplicationData,
			/// <summary>The System directory.</summary>
			// Token: 0x040004F5 RID: 1269
			System = 37,
			/// <summary>The program files directory.</summary>
			// Token: 0x040004F6 RID: 1270
			ProgramFiles,
			/// <summary>The "My Pictures" folder.</summary>
			// Token: 0x040004F7 RID: 1271
			MyPictures,
			/// <summary>The directory for components that are shared across applications.</summary>
			// Token: 0x040004F8 RID: 1272
			CommonProgramFiles = 43
		}
	}
}
