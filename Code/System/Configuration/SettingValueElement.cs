using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Contains the XML representing the serialized value of the setting. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FF RID: 511
	public sealed class SettingValueElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingValueElement" /> class. </summary>
		// Token: 0x0600116C RID: 4460 RVA: 0x0002E948 File Offset: 0x0002CB48
		[MonoTODO]
		public SettingValueElement()
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0002E950 File Offset: 0x0002CB50
		[MonoTODO]
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		/// <summary>Gets or sets the value of a <see cref="T:System.Configuration.SettingValueElement" /> object by using an <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> object containing the value of a <see cref="T:System.Configuration.SettingElement" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0002E958 File Offset: 0x0002CB58
		// (set) Token: 0x0600116F RID: 4463 RVA: 0x0002E960 File Offset: 0x0002CB60
		public XmlNode ValueXml
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0002E96C File Offset: 0x0002CB6C
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.node = new XmlDocument().ReadNode(reader);
		}

		/// <summary>Compares the current <see cref="T:System.Configuration.SettingValueElement" /> instance to the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingValueElement" /> instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="settingValue">The object to compare.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001171 RID: 4465 RVA: 0x0002E980 File Offset: 0x0002CB80
		public override bool Equals(object settingValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.SettingValueElement" /> current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001172 RID: 4466 RVA: 0x0002E988 File Offset: 0x0002CB88
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0002E990 File Offset: 0x0002CB90
		protected override bool IsModified()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0002E998 File Offset: 0x0002CB98
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.node = null;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
		protected override void ResetModified()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0002E9AC File Offset: 0x0002CBAC
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			if (this.node == null)
			{
				return false;
			}
			this.node.WriteTo(writer);
			return true;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0002E9C8 File Offset: 0x0002CBC8
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040004FB RID: 1275
		private XmlNode node;
	}
}
