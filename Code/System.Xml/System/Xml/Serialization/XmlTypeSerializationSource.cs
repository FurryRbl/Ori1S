using System;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x02000268 RID: 616
	internal class XmlTypeSerializationSource : SerializationSource
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x000844C0 File Offset: 0x000826C0
		public XmlTypeSerializationSource(Type type, XmlRootAttribute root, XmlAttributeOverrides attributeOverrides, string namspace, Type[] includedTypes) : base(namspace, includedTypes)
		{
			if (attributeOverrides != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				attributeOverrides.AddKeyHash(stringBuilder);
				this.attributeOverridesHash = stringBuilder.ToString();
			}
			if (root != null)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				root.AddKeyHash(stringBuilder2);
				this.rootHash = stringBuilder2.ToString();
			}
			this.type = type;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0008451C File Offset: 0x0008271C
		public override bool Equals(object o)
		{
			XmlTypeSerializationSource xmlTypeSerializationSource = o as XmlTypeSerializationSource;
			return xmlTypeSerializationSource != null && this.type.Equals(xmlTypeSerializationSource.type) && !(this.rootHash != xmlTypeSerializationSource.rootHash) && !(this.attributeOverridesHash != xmlTypeSerializationSource.attributeOverridesHash) && base.BaseEquals(xmlTypeSerializationSource);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00084588 File Offset: 0x00082788
		public override int GetHashCode()
		{
			return this.type.GetHashCode();
		}

		// Token: 0x04000A6C RID: 2668
		private string attributeOverridesHash;

		// Token: 0x04000A6D RID: 2669
		private Type type;

		// Token: 0x04000A6E RID: 2670
		private string rootHash;
	}
}
