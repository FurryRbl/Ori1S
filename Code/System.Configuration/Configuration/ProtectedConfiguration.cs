using System;

namespace System.Configuration
{
	/// <summary>Provides access to the protected-configuration providers for the current application's configuration file. </summary>
	// Token: 0x02000062 RID: 98
	public static class ProtectedConfiguration
	{
		/// <summary>Gets the name of the default protected-configuration provider.</summary>
		/// <returns>The name of the default protected-configuration provider.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00009810 File Offset: 0x00007A10
		public static string DefaultProvider
		{
			get
			{
				return ProtectedConfiguration.Section.DefaultProvider;
			}
		}

		/// <summary>Gets a collection of the installed protected-configuration providers.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProtectedConfigurationProviderCollection" /> collection of installed <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000981C File Offset: 0x00007A1C
		public static ProtectedConfigurationProviderCollection Providers
		{
			get
			{
				return ProtectedConfiguration.Section.GetAllProviders();
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00009828 File Offset: 0x00007A28
		internal static ProtectedConfigurationSection Section
		{
			get
			{
				return (ProtectedConfigurationSection)ConfigurationManager.GetSection("configProtectedData");
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000983C File Offset: 0x00007A3C
		internal static ProtectedConfigurationProvider GetProvider(string name, bool throwOnError)
		{
			ProtectedConfigurationProvider protectedConfigurationProvider = ProtectedConfiguration.Providers[name];
			if (protectedConfigurationProvider == null && throwOnError)
			{
				throw new Exception(string.Format("The protection provider '{0}' was not found.", name));
			}
			return protectedConfigurationProvider;
		}

		/// <summary>The name of the data protection provider.</summary>
		// Token: 0x0400010E RID: 270
		public const string DataProtectionProviderName = "DataProtectionConfigurationProvider";

		/// <summary>The name of the protected data section.</summary>
		// Token: 0x0400010F RID: 271
		public const string ProtectedDataSectionName = "configProtectedData";

		/// <summary>The name of the RSA provider.</summary>
		// Token: 0x04000110 RID: 272
		public const string RsaProviderName = "RsaProtectedConfigurationProvider";
	}
}
