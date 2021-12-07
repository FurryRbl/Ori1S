using System;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000072 RID: 114
	public class SubjectAltNameExtension : X509Extension
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0001AB80 File Offset: 0x00018D80
		public SubjectAltNameExtension()
		{
			this.extnOid = "2.5.29.17";
			this._names = new GeneralNames();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		public SubjectAltNameExtension(ASN1 asn1) : base(asn1)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001ABAC File Offset: 0x00018DAC
		public SubjectAltNameExtension(X509Extension extension) : base(extension)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001ABB8 File Offset: 0x00018DB8
		public SubjectAltNameExtension(string[] rfc822, string[] dnsNames, string[] ipAddresses, string[] uris)
		{
			this._names = new GeneralNames(rfc822, dnsNames, ipAddresses, uris);
			this.extnValue = new ASN1(4, this._names.GetBytes());
			this.extnOid = "2.5.29.17";
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001AC00 File Offset: 0x00018E00
		protected override void Decode()
		{
			ASN1 asn = new ASN1(this.extnValue.Value);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("Invalid SubjectAltName extension");
			}
			this._names = new GeneralNames(asn);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001AC44 File Offset: 0x00018E44
		public override string Name
		{
			get
			{
				return "Subject Alternative Name";
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001AC4C File Offset: 0x00018E4C
		public string[] RFC822
		{
			get
			{
				return this._names.RFC822;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001AC5C File Offset: 0x00018E5C
		public string[] DNSNames
		{
			get
			{
				return this._names.DNSNames;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001AC6C File Offset: 0x00018E6C
		public string[] IPAddresses
		{
			get
			{
				return this._names.IPAddresses;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001AC7C File Offset: 0x00018E7C
		public string[] UniformResourceIdentifiers
		{
			get
			{
				return this._names.UniformResourceIdentifiers;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001AC8C File Offset: 0x00018E8C
		public override string ToString()
		{
			return this._names.ToString();
		}

		// Token: 0x040001EF RID: 495
		private GeneralNames _names;
	}
}
