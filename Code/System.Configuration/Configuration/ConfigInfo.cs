using System;
using System.Configuration.Internal;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200001B RID: 27
	internal abstract class ConfigInfo
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00002D80 File Offset: 0x00000F80
		public virtual object CreateInstance()
		{
			if (this.Type == null)
			{
				this.Type = this.ConfigHost.GetConfigType(this.TypeName, true);
			}
			return Activator.CreateInstance(this.Type, true);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public string XPath
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.Name);
				for (ConfigInfo parent = this.Parent; parent != null; parent = parent.Parent)
				{
					stringBuilder.Insert(0, parent.Name + "/");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002E04 File Offset: 0x00001004
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002E0C File Offset: 0x0000100C
		public string StreamName
		{
			get
			{
				return this.streamName;
			}
			set
			{
				this.streamName = value;
			}
		}

		// Token: 0x060000D4 RID: 212
		public abstract bool HasConfigContent(Configuration cfg);

		// Token: 0x060000D5 RID: 213
		public abstract bool HasDataContent(Configuration cfg);

		// Token: 0x060000D6 RID: 214 RVA: 0x00002E18 File Offset: 0x00001018
		protected void ThrowException(string text, XmlReader reader)
		{
			throw new ConfigurationErrorsException(text, reader);
		}

		// Token: 0x060000D7 RID: 215
		public abstract void ReadConfig(Configuration cfg, string streamName, XmlReader reader);

		// Token: 0x060000D8 RID: 216
		public abstract void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode);

		// Token: 0x060000D9 RID: 217
		public abstract void ReadData(Configuration config, XmlReader reader, bool overrideAllowed);

		// Token: 0x060000DA RID: 218
		public abstract void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode);

		// Token: 0x060000DB RID: 219
		internal abstract void Merge(ConfigInfo data);

		// Token: 0x04000032 RID: 50
		public string Name;

		// Token: 0x04000033 RID: 51
		public string TypeName;

		// Token: 0x04000034 RID: 52
		protected Type Type;

		// Token: 0x04000035 RID: 53
		private string streamName;

		// Token: 0x04000036 RID: 54
		public ConfigInfo Parent;

		// Token: 0x04000037 RID: 55
		public IInternalConfigHost ConfigHost;
	}
}
