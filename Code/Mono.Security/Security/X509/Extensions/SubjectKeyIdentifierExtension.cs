using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000073 RID: 115
	public class SubjectKeyIdentifierExtension : X509Extension
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001AC9C File Offset: 0x00018E9C
		public SubjectKeyIdentifierExtension()
		{
			this.extnOid = "2.5.29.14";
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		public SubjectKeyIdentifierExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001ACBC File Offset: 0x00018EBC
		public SubjectKeyIdentifierExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001ACC8 File Offset: 0x00018EC8
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 4)
			{
				throw new ArgumentException("Invalid SubjectKeyIdentifier extension");
			}
			this.ski = asn.Value;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001AD0C File Offset: 0x00018F0C
		public override string Name
		{
			get
			{
				return "Subject Key Identifier";
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001AD14 File Offset: 0x00018F14
		public byte[] Identifier
		{
			get
			{
				if (this.ski == null)
				{
					return null;
				}
				return (byte[])this.ski.Clone();
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001AD34 File Offset: 0x00018F34
		public override string ToString()
		{
			if (this.ski == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.ski.Length; i++)
			{
				stringBuilder.Append(this.ski[i].ToString("X2", CultureInfo.InvariantCulture));
				if (i % 2 == 1)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001F0 RID: 496
		private byte[] ski;
	}
}
