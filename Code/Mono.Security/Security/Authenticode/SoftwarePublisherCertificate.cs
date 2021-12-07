using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000021 RID: 33
	public class SoftwarePublisherCertificate
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000097FC File Offset: 0x000079FC
		public SoftwarePublisherCertificate()
		{
			this.pkcs7 = new PKCS7.SignedData();
			this.pkcs7.ContentInfo.ContentType = "1.2.840.113549.1.7.1";
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00009830 File Offset: 0x00007A30
		public SoftwarePublisherCertificate(byte[] data) : this()
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(data);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				throw new ArgumentException(Locale.GetText("Unsupported ContentType"));
			}
			this.pkcs7 = new PKCS7.SignedData(contentInfo.Content);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00009894 File Offset: 0x00007A94
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.pkcs7.Certificates;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000098A4 File Offset: 0x00007AA4
		public ArrayList Crls
		{
			get
			{
				return this.pkcs7.Crls;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000098B4 File Offset: 0x00007AB4
		public byte[] GetBytes()
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
			contentInfo.Content.Add(this.pkcs7.ASN1);
			return contentInfo.GetBytes();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000098EC File Offset: 0x00007AEC
		public static SoftwarePublisherCertificate CreateFromFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			if (array.Length < 2)
			{
				return null;
			}
			if (array[0] != 48)
			{
				try
				{
					array = SoftwarePublisherCertificate.PEM(array);
				}
				catch (Exception inner)
				{
					throw new CryptographicException("Invalid encoding", inner);
				}
			}
			return new SoftwarePublisherCertificate(array);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000099B4 File Offset: 0x00007BB4
		private static byte[] PEM(byte[] data)
		{
			string text = (data[1] != 0) ? Encoding.ASCII.GetString(data) : Encoding.Unicode.GetString(data);
			int num = text.IndexOf("-----BEGIN PKCS7-----") + "-----BEGIN PKCS7-----".Length;
			int num2 = text.IndexOf("-----END PKCS7-----", num);
			string s = (num != -1 && num2 != -1) ? text.Substring(num, num2 - num) : text;
			return Convert.FromBase64String(s);
		}

		// Token: 0x040000A1 RID: 161
		private const string header = "-----BEGIN PKCS7-----";

		// Token: 0x040000A2 RID: 162
		private const string footer = "-----END PKCS7-----";

		// Token: 0x040000A3 RID: 163
		private PKCS7.SignedData pkcs7;
	}
}
