using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000064 RID: 100
	public class AuthorityKeyIdentifierExtension : X509Extension
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00018C18 File Offset: 0x00016E18
		public AuthorityKeyIdentifierExtension()
		{
			this.extnOid = "2.5.29.35";
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00018C2C File Offset: 0x00016E2C
		public AuthorityKeyIdentifierExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00018C38 File Offset: 0x00016E38
		public AuthorityKeyIdentifierExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00018C44 File Offset: 0x00016E44
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid AuthorityKeyIdentifier extension");
			}
			for (int i = 0; i < asn.Count; i++)
			{
				ASN1 asn2 = asn[i];
				byte tag = asn2.Tag;
				if (tag == 128)
				{
					this.aki = asn2.Value;
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00018CC8 File Offset: 0x00016EC8
		public override string Name
		{
			get
			{
				return "Authority Key Identifier";
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00018CD0 File Offset: 0x00016ED0
		public byte[] Identifier
		{
			get
			{
				if (this.aki == null)
				{
					return null;
				}
				return (byte[])this.aki.Clone();
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00018CF0 File Offset: 0x00016EF0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.aki != null)
			{
				int i = 0;
				stringBuilder.Append("KeyID=");
				while (i < this.aki.Length)
				{
					stringBuilder.Append(this.aki[i].ToString("X2", CultureInfo.InvariantCulture));
					if (i % 2 == 1)
					{
						stringBuilder.Append(" ");
					}
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001B9 RID: 441
		private byte[] aki;
	}
}
