using System;
using Mono.Xml.Schema;

namespace System.Xml.Schema
{
	// Token: 0x020001F1 RID: 497
	internal struct UriValueType
	{
		// Token: 0x0600137F RID: 4991 RVA: 0x00053400 File Offset: 0x00051600
		public UriValueType(XmlSchemaUri value)
		{
			this.value = value;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0005340C File Offset: 0x0005160C
		public XmlSchemaUri Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00053414 File Offset: 0x00051614
		public override bool Equals(object obj)
		{
			return obj is UriValueType && (UriValueType)obj == this;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00053434 File Offset: 0x00051634
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00053444 File Offset: 0x00051644
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00053454 File Offset: 0x00051654
		public static bool operator ==(UriValueType v1, UriValueType v2)
		{
			return v1.Value == v2.Value;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0005346C File Offset: 0x0005166C
		public static bool operator !=(UriValueType v1, UriValueType v2)
		{
			return v1.Value != v2.Value;
		}

		// Token: 0x0400077C RID: 1916
		private XmlSchemaUri value;
	}
}
