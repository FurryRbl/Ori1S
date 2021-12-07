using System;

namespace System.Xml.Schema
{
	// Token: 0x020001F2 RID: 498
	internal struct StringArrayValueType
	{
		// Token: 0x06001386 RID: 4998 RVA: 0x00053484 File Offset: 0x00051684
		public StringArrayValueType(string[] value)
		{
			this.value = value;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x00053490 File Offset: 0x00051690
		public string[] Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00053498 File Offset: 0x00051698
		public override bool Equals(object obj)
		{
			return obj is StringArrayValueType && (StringArrayValueType)obj == this;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000534B8 File Offset: 0x000516B8
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000534C8 File Offset: 0x000516C8
		public static bool operator ==(StringArrayValueType v1, StringArrayValueType v2)
		{
			return v1.Value == v2.Value;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000534DC File Offset: 0x000516DC
		public static bool operator !=(StringArrayValueType v1, StringArrayValueType v2)
		{
			return v1.Value != v2.Value;
		}

		// Token: 0x0400077D RID: 1917
		private string[] value;
	}
}
