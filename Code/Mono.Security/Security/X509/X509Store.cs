using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x0200004D RID: 77
	public class X509Store
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00017ED8 File Offset: 0x000160D8
		internal X509Store(string path, bool crl)
		{
			this._storePath = path;
			this._crl = crl;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00017EF0 File Offset: 0x000160F0
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00017F18 File Offset: 0x00016118
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00017F54 File Offset: 0x00016154
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

		// Token: 0x06000388 RID: 904 RVA: 0x00017F98 File Offset: 0x00016198
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

		// Token: 0x06000389 RID: 905 RVA: 0x00017FE0 File Offset: 0x000161E0
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

		// Token: 0x0600038A RID: 906 RVA: 0x00018068 File Offset: 0x00016268
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

		// Token: 0x0600038B RID: 907 RVA: 0x000180E8 File Offset: 0x000162E8
		public void Remove(X509Certificate certificate)
		{
			string path = Path.Combine(this._storePath, this.GetUniqueName(certificate));
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001811C File Offset: 0x0001631C
		public void Remove(X509Crl crl)
		{
			string path = Path.Combine(this._storePath, this.GetUniqueName(crl));
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00018150 File Offset: 0x00016350
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

		// Token: 0x0600038E RID: 910 RVA: 0x00018198 File Offset: 0x00016398
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

		// Token: 0x0600038F RID: 911 RVA: 0x000181E0 File Offset: 0x000163E0
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

		// Token: 0x06000390 RID: 912 RVA: 0x00018210 File Offset: 0x00016410
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

		// Token: 0x06000391 RID: 913 RVA: 0x00018270 File Offset: 0x00016470
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

		// Token: 0x06000392 RID: 914 RVA: 0x000182D4 File Offset: 0x000164D4
		private X509Certificate LoadCertificate(string filename)
		{
			byte[] data = this.Load(filename);
			return new X509Certificate(data);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000182F4 File Offset: 0x000164F4
		private X509Crl LoadCrl(string filename)
		{
			byte[] crl = this.Load(filename);
			return new X509Crl(crl);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00018314 File Offset: 0x00016514
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

		// Token: 0x06000395 RID: 917 RVA: 0x00018380 File Offset: 0x00016580
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

		// Token: 0x06000396 RID: 918 RVA: 0x00018428 File Offset: 0x00016628
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

		// Token: 0x040001A3 RID: 419
		private string _storePath;

		// Token: 0x040001A4 RID: 420
		private X509CertificateCollection _certificates;

		// Token: 0x040001A5 RID: 421
		private ArrayList _crls;

		// Token: 0x040001A6 RID: 422
		private bool _crl;

		// Token: 0x040001A7 RID: 423
		private string _name;
	}
}
