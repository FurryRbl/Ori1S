using System;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for simple type content classes.</summary>
	// Token: 0x0200023E RID: 574
	public abstract class XmlSchemaSimpleTypeContent : XmlSchemaAnnotated
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00071404 File Offset: 0x0006F604
		internal object ActualBaseSchemaType
		{
			get
			{
				return this.OwnerType.BaseSchemaType;
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00071414 File Offset: 0x0006F614
		internal virtual string Normalize(string s, XmlNameTable nt, XmlNamespaceManager nsmgr)
		{
			return s;
		}

		// Token: 0x04000952 RID: 2386
		internal XmlSchemaSimpleType OwnerType;
	}
}
