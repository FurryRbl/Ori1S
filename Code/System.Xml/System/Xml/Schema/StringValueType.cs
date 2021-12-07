using System;

namespace System.Xml.Schema
{
	// Token: 0x020001F0 RID: 496
	internal struct StringValueType
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x0005338C File Offset: 0x0005158C
		public StringValueType(string value)
		{
			this.value = value;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00053398 File Offset: 0x00051598
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000533A0 File Offset: 0x000515A0
		public override bool Equals(object obj)
		{
			return obj is StringValueType && (StringValueType)obj == this;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000533C0 File Offset: 0x000515C0
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000533D0 File Offset: 0x000515D0
		public static bool operator ==(StringValueType v1, StringValueType v2)
		{
			return v1.Value == v2.Value;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000533E8 File Offset: 0x000515E8
		public static bool operator !=(StringValueType v1, StringValueType v2)
		{
			return v1.Value != v2.Value;
		}

		// Token: 0x0400077B RID: 1915
		private string value;
	}
}
