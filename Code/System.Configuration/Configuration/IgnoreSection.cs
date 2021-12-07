using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a wrapper type definition for configuration sections that are not handled by the <see cref="N:System.Configuration" /> types.</summary>
	// Token: 0x0200004A RID: 74
	public sealed class IgnoreSection : ConfigurationSection
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x000084BC File Offset: 0x000066BC
		protected internal override bool IsModified()
		{
			return false;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000084C0 File Offset: 0x000066C0
		protected internal override void DeserializeSection(XmlReader reader)
		{
			this.xml = reader.ReadOuterXml();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000084D0 File Offset: 0x000066D0
		[MonoTODO]
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000084DC File Offset: 0x000066DC
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000084E4 File Offset: 0x000066E4
		protected internal override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			return this.xml;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000084EC File Offset: 0x000066EC
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IgnoreSection.properties;
			}
		}

		// Token: 0x040000DA RID: 218
		private string xml;

		// Token: 0x040000DB RID: 219
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
