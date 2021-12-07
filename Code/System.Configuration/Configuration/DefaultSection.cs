using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a basic configuration-section handler that exposes the configuration section's XML for both read and write access.</summary>
	// Token: 0x02000042 RID: 66
	public sealed class DefaultSection : ConfigurationSection
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000809C File Offset: 0x0000629C
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			if (base.RawXml == null)
			{
				base.RawXml = xmlReader.ReadOuterXml();
			}
			else
			{
				xmlReader.Skip();
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000080CC File Offset: 0x000062CC
		[MonoTODO]
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000080D4 File Offset: 0x000062D4
		[MonoTODO]
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			base.Reset(parentSection);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000080E0 File Offset: 0x000062E0
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000080E8 File Offset: 0x000062E8
		[MonoTODO]
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return base.SerializeSection(parentSection, name, saveMode);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000080F4 File Offset: 0x000062F4
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultSection.properties;
			}
		}

		// Token: 0x040000CE RID: 206
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
