using System;

namespace System.Configuration
{
	/// <summary>Specifies the settings provider used to provide storage for the current application settings class or property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001FA RID: 506
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsProviderAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerTypeName">A <see cref="T:System.String" /> containing the name of the settings provider.</param>
		// Token: 0x0600115F RID: 4447 RVA: 0x0002E878 File Offset: 0x0002CA78
		public SettingsProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this.providerTypeName = providerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class. </summary>
		/// <param name="providerType">A <see cref="T:System.Type" /> containing the settings provider type.</param>
		// Token: 0x06001160 RID: 4448 RVA: 0x0002E898 File Offset: 0x0002CA98
		public SettingsProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this.providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name of the settings provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the settings provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		public string ProviderTypeName
		{
			get
			{
				return this.providerTypeName;
			}
		}

		// Token: 0x040004F4 RID: 1268
		private string providerTypeName;
	}
}
