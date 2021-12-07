using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x020000D1 RID: 209
	internal class X509Store
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x00036074 File Offset: 0x00034274
		internal X509Store(string path, bool crl)
		{
			this._storePath = path;
			this._crl = crl;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0003608C File Offset: 0x0003428C
		public X509CertificateCollection Certificates
		{
			get
			{
				if (this._certificates == null)
				{
					this._certificates = this.BuildCertificatesCollection(this._storePath);
				}
				return this._certificates;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x000360B4 File Offset: 0x000342B4
		public ArrayList Crls
		{
			get
			{
				if (!this._crl)
				{
					this._crls = new ArrayList();
				}
				if (this._crls == null)
				{
					this._crls = this.BuildCrlsCollection(this._storePath);
				}
				return this._crls;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000360F0 File Offset: 0x000342F0
		public string Name
		{
			get
			{
				if (this._name == null)
				{
					int num = this._storePath.LastIndexOf(Path.DirectorySeparatorChar);
					this._name = this._storePath.Substring(num + 1);
				}
				return this._name;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00036134 File Offset: 0x00034334
		public void Clear()
		{
			if (this._certificates != null)
			{
				this._certificates.Clear();
			}
			this._certificates = null;
			if (this._crls != null)
			{
				this._crls.Clear();
			}
			this._crls = null;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003617C File Offset: 0x0003437C
		public void Import(X509Certificate certificate)
		{
			this.CheckStore(this._storePath, true);
			string path = Path.Combine(this._storePath, this.GetUniqueName(certificate));
			if (!File.Exists(path))
			{
				using (FileStream fileStream = File.Create(path))
				{
					byte[] rawData = certificate.RawData;
					fileStream.Write(rawData, 0, rawData.Length);
					fileStream.Close();
				}
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00036204 File Offset: 0x00034404
		public void Import(X509Crl crl)
		{
			this.CheckStore(this._storePath, true);
			string path = Path.Combine(this._storePath, this.GetUniqueName(crl));
			if (!File.Exists(path))
			{
				using (FileStream fileStream = File.Create(path))
				{
					byte[] rawData = crl.RawData;
					fileStream.Write(rawData, 0, rawData.Length);
				}
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00036284 File Offset: 0x00034484
		public void Remove(X509Certificate certificate)
		{
			string path = Path.Combine(this._storePath, this.GetUniqueName(certificate));
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000362B8 File Offset: 0x000344B8
		public void Remove(X509Crl crl)
		{
			string path = Path.Combine(this._storePath, this.GetUniqueName(crl));
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000362EC File Offset: 0x000344EC
		private string GetUniqueName(X509Certificate certificate)
		{
			byte[] array = this.GetUniqueName(certificate.Extensions);
			string method;
			if (array == null)
			{
				method = "tbp";
				array = certificate.Hash;
			}
			else
			{
				method = "ski";
			}
			return this.GetUniqueName(method, array, ".cer");
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00036334 File Offset: 0x00034534
		private string GetUniqueName(X509Crl crl)
		{
			byte[] array = this.GetUniqueName(crl.Extensions);
			string method;
			if (array == null)
			{
				method = "tbp";
				array = crl.Hash;
			}
			else
			{
				method = "ski";
			}
			return this.GetUniqueName(method, array, ".crl");
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003637C File Offset: 0x0003457C
		private byte[] GetUniqueName(X509ExtensionCollection extensions)
		{
			X509Extension x509Extension = extensions["2.5.29.14"];
			if (x509Extension == null)
			{
				return null;
			}
			SubjectKeyIdentifierExtension subjectKeyIdentifierExtension = new SubjectKeyIdentifierExtension(x509Extension);
			return subjectKeyIdentifierExtension.Identifier;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000363AC File Offset: 0x000345AC
		private string GetUniqueName(string method, byte[] name, string fileExtension)
		{
			StringBuilder stringBuilder = new StringBuilder(method);
			stringBuilder.Append("-");
			foreach (byte b in name)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(fileExtension);
			return stringBuilder.ToString();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003640C File Offset: 0x0003460C
		private byte[] Load(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(filename))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return array;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00036470 File Offset: 0x00034670
		private X509Certificate LoadCertificate(string filename)
		{
			byte[] data = this.Load(filename);
			return new X509Certificate(data);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00036490 File Offset: 0x00034690
		private X509Crl LoadCrl(string filename)
		{
			byte[] crl = this.Load(filename);
			return new X509Crl(crl);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000364B0 File Offset: 0x000346B0
		private bool CheckStore(string path, bool throwException)
		{
			bool result;
			try
			{
				if (Directory.Exists(path))
				{
					result = true;
				}
				else
				{
					Directory.CreateDirectory(path);
					result = Directory.Exists(path);
				}
			}
			catch
			{
				if (throwException)
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0003651C File Offset: 0x0003471C
		private X509CertificateCollection BuildCertificatesCollection(string storeName)
		{
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			string path = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(path, false))
			{
				return x509CertificateCollection;
			}
			string[] files = Directory.GetFiles(path, "*.cer");
			if (files != null && files.Length > 0)
			{
				foreach (string filename in files)
				{
					try
					{
						X509Certificate value = this.LoadCertificate(filename);
						x509CertificateCollection.Add(value);
					}
					catch
					{
					}
				}
			}
			return x509CertificateCollection;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000365C4 File Offset: 0x000347C4
		private ArrayList BuildCrlsCollection(string storeName)
		{
			ArrayList arrayList = new ArrayList();
			string path = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(path, false))
			{
				return arrayList;
			}
			string[] files = Directory.GetFiles(path, "*.crl");
			if (files != null && files.Length > 0)
			{
				foreach (string filename in files)
				{
					try
					{
						X509Crl value = this.LoadCrl(filename);
						arrayList.Add(value);
					}
					catch
					{
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04000322 RID: 802
		private string _storePath;

		// Token: 0x04000323 RID: 803
		private X509CertificateCollection _certificates;

		// Token: 0x04000324 RID: 804
		private ArrayList _crls;

		// Token: 0x04000325 RID: 805
		private bool _crl;

		// Token: 0x04000326 RID: 806
		private string _name;
	}
}
