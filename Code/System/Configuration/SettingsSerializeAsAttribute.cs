using System;

namespace System.Configuration
{
	/// <summary>Specifies the serialization mechanism that the settings provider should use. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001FD RID: 509
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsSerializeAsAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsSerializeAsAttribute" /> class.</summary>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</param>
		// Token: 0x0600116A RID: 4458 RVA: 0x0002E930 File Offset: 0x0002CB30
		public SettingsSerializeAsAttribute(SettingsSerializeAs serializeAs)
		{
			this.serializeAs = serializeAs;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.SettingsSerializeAs" /> enumeration value that specifies the serialization scheme.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> enumerated value that specifies the serialization scheme.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0002E940 File Offset: 0x0002CB40
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
		}

		// Token: 0x040004F5 RID: 1269
		private SettingsSerializeAs serializeAs;
	}
}
