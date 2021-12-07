using System;
using System.Globalization;
using System.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000042 RID: 66
	public abstract class X509Builder
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x000150A0 File Offset: 0x000132A0
		protected X509Builder()
		{
			this.hashName = "SHA1";
		}

		// Token: 0x060002D4 RID: 724
		protected abstract ASN1 ToBeSigned(string hashName);

		// Token: 0x060002D5 RID: 725 RVA: 0x000150B4 File Offset: 0x000132B4
		protected string GetOid(string hashName)
		{
			string text = hashName.ToLower(CultureInfo.InvariantCulture);
			switch (text)
			{
			case "md2":
				return "1.2.840.113549.1.1.2";
			case "md4":
				return "1.2.840.113549.1.1.3";
			case "md5":
				return "1.2.840.113549.1.1.4";
			case "sha1":
				return "1.2.840.113549.1.1.5";
			case "sha256":
				return "1.2.840.113549.1.1.11";
			case "sha384":
				return "1.2.840.113549.1.1.12";
			case "sha512":
				return "1.2.840.113549.1.1.13";
			}
			throw new NotSupportedException("Unknown hash algorithm " + hashName);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000151B4 File Offset: 0x000133B4
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000151BC File Offset: 0x000133BC
		public string Hash
		{
			get
			{
				return this.hashName;
			}
			set
			{
				if (this.hashName == null)
				{
					this.hashName = "SHA1";
				}
				else
				{
					this.hashName = value;
				}
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000151EC File Offset: 0x000133EC
		public virtual byte[] Sign(AsymmetricAlgorithm aa)
		{
			if (aa is RSA)
			{
				return this.Sign(aa as RSA);
			}
			if (aa is DSA)
			{
				return this.Sign(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00015240 File Offset: 0x00013440
		private byte[] Build(ASN1 tbs, string hashoid, byte[] signature)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(tbs);
			asn.Add(PKCS7.AlgorithmIdentifier(hashoid));
			byte[] array = new byte[signature.Length + 1];
			Buffer.BlockCopy(signature, 0, array, 1, signature.Length);
			asn.Add(new ASN1(3, array));
			return asn.GetBytes();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00015298 File Offset: 0x00013498
		public virtual byte[] Sign(RSA key)
		{
			string oid = this.GetOid(this.hashName);
			ASN1 asn = this.ToBeSigned(oid);
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashName);
			byte[] rgbHash = hashAlgorithm.ComputeHash(asn.GetBytes());
			RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(key);
			rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashName);
			byte[] signature = rsapkcs1SignatureFormatter.CreateSignature(rgbHash);
			return this.Build(asn, oid, signature);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000152FC File Offset: 0x000134FC
		public virtual byte[] Sign(DSA key)
		{
			string hashoid = "1.2.840.10040.4.3";
			ASN1 asn = this.ToBeSigned(hashoid);
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashName);
			if (!(hashAlgorithm is SHA1))
			{
				throw new NotSupportedException("Only SHA-1 is supported for DSA");
			}
			byte[] rgbHash = hashAlgorithm.ComputeHash(asn.GetBytes());
			DSASignatureFormatter dsasignatureFormatter = new DSASignatureFormatter(key);
			dsasignatureFormatter.SetHashAlgorithm(this.hashName);
			byte[] src = dsasignatureFormatter.CreateSignature(rgbHash);
			byte[] array = new byte[20];
			Buffer.BlockCopy(src, 0, array, 0, 20);
			byte[] array2 = new byte[20];
			Buffer.BlockCopy(src, 20, array2, 0, 20);
			ASN1 asn2 = new ASN1(48);
			asn2.Add(new ASN1(2, array));
			asn2.Add(new ASN1(2, array2));
			return this.Build(asn, hashoid, asn2.GetBytes());
		}

		// Token: 0x0400015B RID: 347
		private const string defaultHash = "SHA1";

		// Token: 0x0400015C RID: 348
		private string hashName;
	}
}
