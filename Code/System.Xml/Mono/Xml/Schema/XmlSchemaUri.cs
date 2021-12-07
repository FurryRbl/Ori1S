using System;

namespace Mono.Xml.Schema
{
	// Token: 0x020001E2 RID: 482
	internal class XmlSchemaUri : Uri
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x00052AE0 File Offset: 0x00050CE0
		public XmlSchemaUri(string src) : this(src, XmlSchemaUri.HasValidScheme(src))
		{
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00052AF0 File Offset: 0x00050CF0
		private XmlSchemaUri(string src, bool formal) : base((!formal) ? ("anyuri:" + src) : src, !formal)
		{
			this.value = src;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00052B28 File Offset: 0x00050D28
		private static bool HasValidScheme(string src)
		{
			int num = src.IndexOf(':');
			if (num < 0)
			{
				return false;
			}
			int i = 0;
			while (i < num)
			{
				switch (src[i])
				{
				case '+':
				case '-':
				case '.':
					break;
				case ',':
					goto IL_44;
				default:
					goto IL_44;
				}
				IL_5C:
				i++;
				continue;
				IL_44:
				if (char.IsLetterOrDigit(src[i]))
				{
					goto IL_5C;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00052BA0 File Offset: 0x00050DA0
		public override bool Equals(object obj)
		{
			return obj is XmlSchemaUri && (XmlSchemaUri)obj == this;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00052BBC File Offset: 0x00050DBC
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00052BCC File Offset: 0x00050DCC
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00052BD4 File Offset: 0x00050DD4
		public static bool operator ==(XmlSchemaUri v1, XmlSchemaUri v2)
		{
			return v1.value == v2.value;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00052BE8 File Offset: 0x00050DE8
		public static bool operator !=(XmlSchemaUri v1, XmlSchemaUri v2)
		{
			return v1.value != v2.value;
		}

		// Token: 0x04000778 RID: 1912
		public string value;
	}
}
