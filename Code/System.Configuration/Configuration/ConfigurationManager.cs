using System;
using System.Collections.Specialized;
using System.Configuration.Internal;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Configuration
{
	/// <summary>Provides access to configuration files for client applications. This class cannot be inherited.</summary>
	// Token: 0x0200002D RID: 45
	public static class ConfigurationManager
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000065D0 File Offset: 0x000047D0
		[MonoTODO("Evidence and version still needs work")]
		private static string GetAssemblyInfo(Assembly a)
		{
			object[] customAttributes = a.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			string arg;
			if (customAttributes != null && customAttributes.Length > 0)
			{
				arg = ((AssemblyProductAttribute)customAttributes[0]).Product;
			}
			else
			{
				arg = AppDomain.CurrentDomain.FriendlyName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("evidencehere");
			string arg2 = stringBuilder.ToString();
			customAttributes = a.GetCustomAttributes(typeof(AssemblyVersionAttribute), false);
			string path;
			if (customAttributes != null && customAttributes.Length > 0)
			{
				path = ((AssemblyVersionAttribute)customAttributes[0]).Version;
			}
			else
			{
				path = "1.0.0.0";
			}
			return Path.Combine(string.Format("{0}_{1}", arg, arg2), path);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006684 File Offset: 0x00004884
		internal static Configuration OpenExeConfigurationInternal(ConfigurationUserLevel userLevel, Assembly calling_assembly, string exePath)
		{
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			if (userLevel != ConfigurationUserLevel.None)
			{
				if (userLevel != ConfigurationUserLevel.PerUserRoaming)
				{
					if (userLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						goto IL_104;
					}
				}
				else
				{
					exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ConfigurationManager.GetAssemblyInfo(calling_assembly));
					exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(exeConfigurationFileMap.RoamingUserConfigFilename, "user.config");
				}
				exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationManager.GetAssemblyInfo(calling_assembly));
				exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(exeConfigurationFileMap.LocalUserConfigFilename, "user.config");
			}
			else if (exePath == null || exePath.Length == 0)
			{
				exeConfigurationFileMap.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
			else
			{
				if (!Path.IsPathRooted(exePath))
				{
					exePath = Path.GetFullPath(exePath);
				}
				if (!File.Exists(exePath))
				{
					Exception inner = new ArgumentException("The specified path does not exist.", "exePath");
					throw new ConfigurationErrorsException("Error Initializing the configuration system:", inner);
				}
				exeConfigurationFileMap.ExeConfigFilename = exePath + ".config";
			}
			IL_104:
			return ConfigurationManager.ConfigurationFactory.Create(typeof(ExeConfigurationHost), new object[]
			{
				exeConfigurationFileMap,
				userLevel
			});
		}

		/// <summary>Opens the configuration file for the current application as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> for which you are opening the configuration.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x060001C4 RID: 452 RVA: 0x000067BC File Offset: 0x000049BC
		public static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
		{
			return ConfigurationManager.OpenExeConfigurationInternal(userLevel, Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly(), null);
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="exePath">The path of the executable (exe) file.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x060001C5 RID: 453 RVA: 0x000067D8 File Offset: 0x000049D8
		public static Configuration OpenExeConfiguration(string exePath)
		{
			return ConfigurationManager.OpenExeConfigurationInternal(ConfigurationUserLevel.None, Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly(), exePath);
		}

		/// <summary>Opens the specified client configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping and user level.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references configuration file to use instead of the application default configuration file.</param>
		/// <param name="userLevel">The <see cref="T:System.Configuration.ConfigurationUserLevel" /> object for which you are opening the configuration.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x060001C6 RID: 454 RVA: 0x000067F4 File Offset: 0x000049F4
		[MonoLimitation("ConfigurationUserLevel parameter is not supported.")]
		public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
		{
			return ConfigurationManager.ConfigurationFactory.Create(typeof(ExeConfigurationHost), new object[]
			{
				fileMap,
				userLevel
			});
		}

		/// <summary>Opens the machine configuration file on the current computer as a <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x060001C7 RID: 455 RVA: 0x00006828 File Offset: 0x00004A28
		public static Configuration OpenMachineConfiguration()
		{
			ConfigurationFileMap configurationFileMap = new ConfigurationFileMap();
			return ConfigurationManager.ConfigurationFactory.Create(typeof(MachineConfigurationHost), new object[]
			{
				configurationFileMap
			});
		}

		/// <summary>Opens the machine configuration file as a <see cref="T:System.Configuration.Configuration" /> object that uses the specified file mapping.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object that references configuration file to use instead of the application default configuration file.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x060001C8 RID: 456 RVA: 0x0000685C File Offset: 0x00004A5C
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return ConfigurationManager.ConfigurationFactory.Create(typeof(MachineConfigurationHost), new object[]
			{
				fileMap
			});
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000687C File Offset: 0x00004A7C
		internal static IInternalConfigConfigurationFactory ConfigurationFactory
		{
			get
			{
				return ConfigurationManager.configFactory;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006884 File Offset: 0x00004A84
		internal static IInternalConfigSystem ConfigurationSystem
		{
			get
			{
				return ConfigurationManager.configSystem;
			}
		}

		/// <summary>Retrieves a specified configuration section for the current application's default configuration.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object, or null if the section does not exist.</returns>
		/// <param name="sectionName">The configuration section path and name.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x060001CB RID: 459 RVA: 0x0000688C File Offset: 0x00004A8C
		public static object GetSection(string sectionName)
		{
			object section = ConfigurationManager.ConfigurationSystem.GetSection(sectionName);
			if (section is ConfigurationSection)
			{
				return ((ConfigurationSection)section).GetRuntimeObject();
			}
			return section;
		}

		/// <summary>Refreshes the named section so the next time that it is retrieved it will be re-read from disk.</summary>
		/// <param name="sectionName">The configuration section name or the configuration path and section name of the section to refresh.</param>
		// Token: 0x060001CC RID: 460 RVA: 0x000068C0 File Offset: 0x00004AC0
		public static void RefreshSection(string sectionName)
		{
			ConfigurationManager.ConfigurationSystem.RefreshConfig(sectionName);
		}

		/// <summary>Gets the <see cref="T:System.Configuration.AppSettingsSection" /> data for the current application's default configuration.</summary>
		/// <returns>Returns a <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the contents of the <see cref="T:System.Configuration.AppSettingsSection" /> object for the current application's default configuration. </returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Collections.Specialized.NameValueCollection" /> object with the application settings data.</exception>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000068D0 File Offset: 0x00004AD0
		public static NameValueCollection AppSettings
		{
			get
			{
				return (NameValueCollection)ConfigurationManager.GetSection("appSettings");
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConnectionStringsSection" /> data for the current application's default configuration.</summary>
		/// <returns>Returns a <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object that contains the contents of the <see cref="T:System.Configuration.ConnectionStringsSection" /> object for the current application's default configuration. </returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object.</exception>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000068E4 File Offset: 0x00004AE4
		[MonoTODO]
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)ConfigurationManager.GetSection("connectionStrings");
				return connectionStringsSection.ConnectionStrings;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006908 File Offset: 0x00004B08
		internal static IInternalConfigSystem ChangeConfigurationSystem(IInternalConfigSystem newSystem)
		{
			if (newSystem == null)
			{
				throw new ArgumentNullException("newSystem");
			}
			object obj = ConfigurationManager.lockobj;
			IInternalConfigSystem result;
			lock (obj)
			{
				string strA = newSystem.GetType().ToString();
				if (string.Compare(strA, "System.Web.Configuration.HttpConfigurationSystem", StringComparison.OrdinalIgnoreCase) == 0)
				{
					ConfigurationManager.systemWebInUse = true;
				}
				else
				{
					ConfigurationManager.systemWebInUse = false;
				}
				IInternalConfigSystem internalConfigSystem = ConfigurationManager.configSystem;
				ConfigurationManager.configSystem = newSystem;
				result = internalConfigSystem;
			}
			return result;
		}

		// Token: 0x0400008E RID: 142
		private static bool systemWebInUse;

		// Token: 0x0400008F RID: 143
		private static InternalConfigurationFactory configFactory = new InternalConfigurationFactory();

		// Token: 0x04000090 RID: 144
		private static IInternalConfigSystem configSystem = new ClientConfigurationSystem();

		// Token: 0x04000091 RID: 145
		private static object lockobj = new object();
	}
}
