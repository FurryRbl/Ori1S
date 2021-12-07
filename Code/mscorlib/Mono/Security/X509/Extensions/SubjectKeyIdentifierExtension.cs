using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x020000EB RID: 235
	internal class SubjectKeyIdentifierExtension : X509Extension
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x000373E0 File Offset: 0x000355E0
		public SubjectKeyIdentifierExtension()
		{
			this.extnOid = "2.5.29.14";
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000373F4 File Offset: 0x000355F4
		public SubjectKeyIdentifierExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00037400 File Offset: 0x00035600
		public SubjectKeyIdentifierExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0003740C File Offset: 0x0003560C
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 4)
			{
				throw new ArgumentException("Invalid SubjectKeyIdentifier extension");
			}
			this.ski = asn.Value;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00037450 File Offset: 0x00035650
		public override string Name
		{
			get
			{
				return "Subject Key Identifier";
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00037458 File Offset: 0x00035658
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

		// Token: 0x06000C15 RID: 3093 RVA: 0x00037478 File Offset: 0x00035678
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

		// Token: 0x04000347 RID: 839
		private byte[] ski;
	}
}
