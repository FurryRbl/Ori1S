using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a collection of static methods that return information about the common language runtime environment.</summary>
	// Token: 0x020003B8 RID: 952
	[ComVisible(true)]
	public class RuntimeEnvironment
	{
		/// <summary>Gets the path to the system configuration file.</summary>
		/// <returns>The path to the system configuration file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x0009383C File Offset: 0x00091A3C
		public static string SystemConfigurationFile
		{
			get
			{
				string machineConfigPath = Environment.GetMachineConfigPath();
				if (SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, machineConfigPath).Demand();
				}
				return machineConfigPath;
			}
		}

		/// <summary>Tests whether the specified assembly is loaded in the global assembly cache (GAC).</summary>
		/// <returns>true if the assembly is loaded in the GAC; otherwise, false.</returns>
		/// <param name="a">The assembly to determine if it is loaded in the GAC. </param>
		// Token: 0x06002B66 RID: 11110 RVA: 0x00093868 File Offset: 0x00091A68
		public static bool FromGlobalAccessCache(Assembly a)
		{
			return a.GlobalAssemblyCache;
		}

		/// <summary>Gets the directory where the common language runtime is installed.</summary>
		/// <returns>A string containing the path to the directory where the common language runtime is installed.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002B67 RID: 11111 RVA: 0x00093870 File Offset: 0x00091A70
		public static string GetRuntimeDirectory()
		{
			return Path.GetDirectoryName(typeof(int).Assembly.Location);
		}

		/// <summary>Gets the version number of the common language runtime that is running the current process.</summary>
		/// <returns>A string containing the version number of the common language runtime.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B68 RID: 11112 RVA: 0x0009388C File Offset: 0x00091A8C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static string GetSystemVersion()
		{
			return string.Concat(new object[]
			{
				"v",
				Environment.Version.Major,
				".",
				Environment.Version.Minor,
				".",
				Environment.Version.Build
			});
		}
	}
}
