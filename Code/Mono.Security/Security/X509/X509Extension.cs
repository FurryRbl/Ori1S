using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.X509
{
	// Token: 0x0200004B RID: 75
	public class X509Extension
	{
		// Token: 0x06000363 RID: 867 RVA: 0x000176B0 File Offset: 0x000158B0
		protected X509Extension()
		{
			this.extnCritical = false;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000176C0 File Offset: 0x000158C0
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

		// Token: 0x06000365 RID: 869 RVA: 0x00017800 File Offset: 0x00015A00
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

		// Token: 0x06000366 RID: 870 RVA: 0x0001788C File Offset: 0x00015A8C
		protected virtual void Decode()
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00017890 File Offset: 0x00015A90
		protected virtual void Encode()
		{
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00017894 File Offset: 0x00015A94
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000178F8 File Offset: 0x00015AF8
		public string Oid
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00017900 File Offset: 0x00015B00
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00017908 File Offset: 0x00015B08
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00017914 File Offset: 0x00015B14
		public virtual string Name
		{
			get
			{
				return this.extnOid;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0001791C File Offset: 0x00015B1C
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

		// Token: 0x0600036E RID: 878 RVA: 0x00017938 File Offset: 0x00015B38
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

		// Token: 0x0600036F RID: 879 RVA: 0x000179E0 File Offset: 0x00015BE0
		public byte[] GetBytes()
		{
			return this.ASN1.GetBytes();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000179F0 File Offset: 0x00015BF0
		public override int GetHashCode()
		{
			return this.extnOid.GetHashCode();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00017A00 File Offset: 0x00015C00
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

		// Token: 0x06000372 RID: 882 RVA: 0x00017AD0 File Offset: 0x00015CD0
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

		// Token: 0x0400019F RID: 415
		protected string extnOid;

		// Token: 0x040001A0 RID: 416
		protected bool extnCritical;

		// Token: 0x040001A1 RID: 417
		protected ASN1 extnValue;
	}
}
