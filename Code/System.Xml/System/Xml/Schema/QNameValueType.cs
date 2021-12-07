using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EF RID: 495
	internal struct QNameValueType
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x00053318 File Offset: 0x00051518
		public QNameValueType(XmlQualifiedName value)
		{
			this.value = value;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00053324 File Offset: 0x00051524
		public XmlQualifiedName Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0005332C File Offset: 0x0005152C
		public override bool Equals(object obj)
		{
			return obj is QNameValueType && (QNameValueType)obj == this;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0005334C File Offset: 0x0005154C
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0005335C File Offset: 0x0005155C
		public static bool operator ==(QNameValueType v1, QNameValueType v2)
		{
			return v1.Value == v2.Value;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00053374 File Offset: 0x00051574
		public static bool operator !=(QNameValueType v1, QNameValueType v2)
		{
			return v1.Value != v2.Value;
		}

		// Token: 0x0400077A RID: 1914
		private XmlQualifiedName value;
	}
}
