using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x020000CF RID: 207
	internal class X509Extension
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x0003584C File Offset: 0x00033A4C
		protected X509Extension()
		{
			this.extnCritical = false;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003585C File Offset: 0x00033A5C
		public X509Extension(ASN1 asn1)
		{
			if (asn1.Tag != 48 || asn1.Count < 2)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			if (asn1[0].Tag != 6)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = ASN1Convert.ToOid(asn1[0]);
			this.extnCritical = (asn1[1].Tag == 1 && asn1[1].Value[0] == byte.MaxValue);
			this.extnValue = asn1[asn1.Count - 1];
			if (this.extnValue.Tag == 4 && this.extnValue.Length > 0 && this.extnValue.Count == 0)
			{
				try
				{
					ASN1 asn2 = new ASN1(this.extnValue.Value);
					this.extnValue.Value = null;
					this.extnValue.Add(asn2);
				}
				catch
				{
				}
			}
			this.Decode();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0003599C File Offset: 0x00033B9C
		public X509Extension(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (extension.Value == null || extension.Value.Tag != 4 || extension.Value.Count != 1)
			{
				throw new ArgumentException(Locale.GetText("Invalid X.509 extension."));
			}
			this.extnOid = extension.Oid;
			this.extnCritical = extension.Critical;
			this.extnValue = extension.Value;
			this.Decode();
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00035A28 File Offset: 0x00033C28
		protected virtual void Decode()
		{
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00035A2C File Offset: 0x00033C2C
		protected virtual void Encode()
		{
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00035A30 File Offset: 0x00033C30
		public ASN1 ASN1
		{
			get
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.extnOid));
				if (this.extnCritical)
				{
					asn.Add(new ASN1(1, new byte[]
					{
						byte.MaxValue
					}));
				}
				this.Encode();
				asn.Add(this.extnValue);
				return asn;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00035A94 File Offset: 0x00033C94
		public string Oid
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00035A9C File Offset: 0x00033C9C
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x00035AA4 File Offset: 0x00033CA4
		public bool Critical
		{
			get
			{
				return this.extnCritical;
			}
			set
			{
				this.extnCritical = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00035AB0 File Offset: 0x00033CB0
		public virtual string Name
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00035AB8 File Offset: 0x00033CB8
		public ASN1 Value
		{
			get
			{
				if (this.extnValue == null)
				{
					this.Encode();
				}
				return this.extnValue;
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00035AD4 File Offset: 0x00033CD4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			X509Extension x509Extension = obj as X509Extension;
			if (x509Extension == null)
			{
				return false;
			}
			if (this.extnCritical != x509Extension.extnCritical)
			{
				return false;
			}
			if (this.extnOid != x509Extension.extnOid)
			{
				return false;
			}
			if (this.extnValue.Length != x509Extension.extnValue.Length)
			{
				return false;
			}
			for (int i = 0; i < this.extnValue.Length; i++)
			{
				if (this.extnValue[i] != x509Extension.extnValue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00035B7C File Offset: 0x00033D7C
		public byte[] GetBytes()
		{
			return this.ASN1.GetBytes();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00035B8C File Offset: 0x00033D8C
		public override int GetHashCode()
		{
			return this.extnOid.GetHashCode();
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00035B9C File Offset: 0x00033D9C
		private void WriteLine(StringBuilder sb, int n, int pos)
		{
			byte[] value = this.extnValue.Value;
			int num = pos;
			for (int i = 0; i < 8; i++)
			{
				if (i < n)
				{
					sb.Append(value[num++].ToString("X2", CultureInfo.InvariantCulture));
					sb.Append(" ");
				}
				else
				{
					sb.Append("   ");
				}
			}
			sb.Append("  ");
			num = pos;
			for (int j = 0; j < n; j++)
			{
				byte b = value[num++];
				if (b < 32)
				{
					sb.Append(".");
				}
				else
				{
					sb.Append(Convert.ToChar(b));
				}
			}
			sb.Append(Environment.NewLine);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00035C6C File Offset: 0x00033E6C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.extnValue.Length >> 3;
			int n = this.extnValue.Length - (num << 3);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				this.WriteLine(stringBuilder, 8, num2);
				num2 += 8;
			}
			this.WriteLine(stringBuilder, n, num2);
			return stringBuilder.ToString();
		}

		// Token: 0x0400031E RID: 798
		protected string extnOid;

		// Token: 0x0400031F RID: 799
		protected bool extnCritical;

		// Token: 0x04000320 RID: 800
		protected ASN1 extnValue;
	}
}
