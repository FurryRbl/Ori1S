using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Provides runtime versions 1.0 and 1.1 support for reading configuration sections and common configuration settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CF RID: 463
	public sealed class ConfigurationSettings
	{
		// Token: 0x0600102D RID: 4141 RVA: 0x0002ADCC File Offset: 0x00028FCC
		private ConfigurationSettings()
		{
		}

		/// <summary>Returns the <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.Note:The <see cref="T:System.Configuration.ConfigurationSettings" /> class provides backward compatibility only. You should use the <see cref="T:System.Configuration.ConfigurationManager" /> class or <see cref="T:System.Web.Configuration.WebConfigurationManager" /> class instead.</returns>
		/// <param name="sectionName">A configuration name and path, such as "system.net/settings".</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">Unable to retrieve the requested section.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600102F RID: 4143 RVA: 0x0002ADEC File Offset: 0x00028FEC
		[Obsolete("This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.GetSection")]
		public static object GetConfig(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}

		/// <summary>Gets a read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section of the configuration file.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the application settings section from the configuration file.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0002ADF4 File Offset: 0x00028FF4
		[Obsolete("This property is obsolete.  Please use System.Configuration.ConfigurationManager.AppSettings")]
		public static System.Collections.Specialized.NameValueCollection AppSettings
		{
			get
			{
				object obj = ConfigurationManager.GetSection("appSettings");
				if (obj == null)
				{
					obj = new System.Collections.Specialized.NameValueCollection();
				}
				return (System.Collections.Specialized.NameValueCollection)obj;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002AE20 File Offset: 0x00029020
		internal static IConfigurationSystem ChangeConfigurationSystem(IConfigurationSystem newSystem)
		{
			if (newSystem == null)
			{
				throw new ArgumentNullException("newSystem");
			}
			object obj = ConfigurationSettings.lockobj;
			IConfigurationSystem result;
			lock (obj)
			{
				IConfigurationSystem configurationSystem = ConfigurationSettings.config;
				ConfigurationSettings.config = newSystem;
				result = configurationSystem;
			}
			return result;
		}

		// Token: 0x04000480 RID: 1152
		private static IConfigurationSystem config = DefaultConfig.GetInstance();

		// Token: 0x04000481 RID: 1153
		private static object lockobj = new object();
	}
}
