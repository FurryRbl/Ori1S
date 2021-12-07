using System;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x02000269 RID: 617
	internal class SoapTypeSerializationSource : SerializationSource
	{
		// Token: 0x0600190A RID: 6410 RVA: 0x00084598 File Offset: 0x00082798
		public SoapTypeSerializationSource(Type type, SoapAttributeOverrides attributeOverrides, string namspace, Type[] includedTypes) : base(namspace, includedTypes)
		{
			if (attributeOverrides != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				attributeOverrides.AddKeyHash(stringBuilder);
				this.attributeOverridesHash = stringBuilder.ToString();
			}
			this.type = type;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000845D4 File Offset: 0x000827D4
		public override bool Equals(object o)
		{
			SoapTypeSerializationSource soapTypeSerializationSource = o as SoapTypeSerializationSource;
			return soapTypeSerializationSource != null && this.type.Equals(soapTypeSerializationSource.type) && !(this.attributeOverridesHash != soapTypeSerializationSource.attributeOverridesHash) && base.BaseEquals(soapTypeSerializationSource);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00084628 File Offset: 0x00082828
		public override int GetHashCode()
		{
			return this.type.GetHashCode();
		}

		// Token: 0x04000A6F RID: 2671
		private string attributeOverridesHash;

		// Token: 0x04000A70 RID: 2672
		private Type type;
	}
}
