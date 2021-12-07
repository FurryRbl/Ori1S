using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using Mono.Security.Authenticode;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides methods that help you use X.509 v.3 certificates.</summary>
	// Token: 0x020005EC RID: 1516
	[MonoTODO("X509ContentType.SerializedCert isn't supported (anywhere in the class)")]
	[ComVisible(true)]
	[Serializable]
	public class X509Certificate : ISerializable, IDeserializationCallback
	{
		// Token: 0x060039D0 RID: 14800 RVA: 0x000C6518 File Offset: 0x000C4718
		internal X509Certificate(byte[] data, bool dates)
		{
			if (data != null)
			{
				this.Import(data, null, X509KeyStorageFlags.DefaultKeySet);
				this.hideDates = !dates;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class defined from a sequence of bytes representing an X.509v3 certificate.</summary>
		/// <param name="data">A byte array containing data from an X.509 certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060039D1 RID: 14801 RVA: 0x000C6544 File Offset: 0x000C4744
		public X509Certificate(byte[] data) : this(data, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a handle to an unmanaged PCCERT_CONTEXT structure.</summary>
		/// <param name="handle">A handle to an unmanaged PCCERT_CONTEXT structure.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The handle parameter does not represent a valid PCCERT_CONTEXT structure.</exception>
		// Token: 0x060039D2 RID: 14802 RVA: 0x000C6550 File Offset: 0x000C4750
		public X509Certificate(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("Invalid handle.");
			}
			this.InitFromHandle(handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class.</summary>
		/// <param name="cert">A <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class from which to initialize this class. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="cert" /> parameter is null.</exception>
		// Token: 0x060039D3 RID: 14803 RVA: 0x000C6588 File Offset: 0x000C4788
		public X509Certificate(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			if (cert != null)
			{
				byte[] rawCertData = cert.GetRawCertData();
				if (rawCertData != null)
				{
					this.x509 = new X509Certificate(rawCertData);
				}
				this.hideDates = false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class. </summary>
		// Token: 0x060039D4 RID: 14804 RVA: 0x000C65D4 File Offset: 0x000C47D4
		public X509Certificate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060039D5 RID: 14805 RVA: 0x000C65DC File Offset: 0x000C47DC
		public X509Certificate(byte[] rawData, string password)
		{
			this.Import(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060039D6 RID: 14806 RVA: 0x000C65F0 File Offset: 0x000C47F0
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(byte[] rawData, SecureString password)
		{
			this.Import(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060039D7 RID: 14807 RVA: 0x000C6604 File Offset: 0x000C4804
		public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values that controls where and how to import the private key. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x060039D8 RID: 14808 RVA: 0x000C6618 File Offset: 0x000C4818
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file. </summary>
		/// <param name="fileName">The name of a PKCS7 signed file.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060039D9 RID: 14809 RVA: 0x000C662C File Offset: 0x000C482C
		public X509Certificate(string fileName)
		{
			this.Import(fileName, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file and a password to access the certificate.</summary>
		/// <param name="fileName">The name of a PKCS7 signed file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060039DA RID: 14810 RVA: 0x000C6640 File Offset: 0x000C4840
		public X509Certificate(string fileName, string password)
		{
			this.Import(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a certificate file name and a password.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060039DB RID: 14811 RVA: 0x000C6654 File Offset: 0x000C4854
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(string fileName, SecureString password)
		{
			this.Import(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using the name of a PKCS7 signed file, a password to access the certificate, and a key storage flag. </summary>
		/// <param name="fileName">The name of a PKCS7 signed file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060039DC RID: 14812 RVA: 0x000C6668 File Offset: 0x000C4868
		public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a certificate file name, a password, and a key storage flag. </summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values that controls where and how to import the private key. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x060039DD RID: 14813 RVA: 0x000C667C File Offset: 0x000C487C
		[MonoTODO("SecureString support is incomplete")]
		public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class using a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object and a <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes serialization information.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that describes how serialization should be performed.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x060039DE RID: 14814 RVA: 0x000C6690 File Offset: 0x000C4890
		public X509Certificate(SerializationInfo info, StreamingContext context)
		{
			byte[] rawData = (byte[])info.GetValue("RawData", typeof(byte[]));
			this.Import(rawData, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.  </summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060039DF RID: 14815 RVA: 0x000C66C8 File Offset: 0x000C48C8
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Gets serialization information with all the data needed to recreate an instance of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x060039E0 RID: 14816 RVA: 0x000C66CC File Offset: 0x000C48CC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("RawData", this.x509.RawData);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000C66E4 File Offset: 0x000C48E4
		private string tostr(byte[] data)
		{
			if (data != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					stringBuilder.Append(data[i].ToString("X2"));
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		/// <summary>Creates an X.509v3 certificate using the name of a PKCS7 signed file.</summary>
		/// <returns>The newly created X.509 certificate.</returns>
		/// <param name="filename">The path of the PKCS7 signed file from which to create the X.509 certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="filename" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060039E2 RID: 14818 RVA: 0x000C6734 File Offset: 0x000C4934
		public static X509Certificate CreateFromCertFile(string filename)
		{
			byte[] data = X509Certificate.Load(filename);
			return new X509Certificate(data);
		}

		/// <summary>Creates an X.509v3 certificate from the specified signed file.</summary>
		/// <returns>The newly created X.509 certificate.</returns>
		/// <param name="filename">The path of the signed file from which to create the X.509 certificate. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060039E3 RID: 14819 RVA: 0x000C6750 File Offset: 0x000C4950
		[MonoTODO("Incomplete - minimal validation in this version")]
		public static X509Certificate CreateFromSignedFile(string filename)
		{
			try
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(filename);
				if (authenticodeDeformatter.SigningCertificate != null)
				{
					return new X509Certificate(authenticodeDeformatter.SigningCertificate.RawData);
				}
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception inner)
			{
				string text = Locale.GetText("Couldn't extract digital signature from {0}.", new object[]
				{
					filename
				});
				throw new COMException(text, inner);
			}
			throw new CryptographicException(Locale.GetText("{0} isn't signed.", new object[]
			{
				filename
			}));
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000C6804 File Offset: 0x000C4A04
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		private void InitFromHandle(IntPtr handle)
		{
			if (handle != IntPtr.Zero)
			{
				X509Certificate.CertificateContext certificateContext = (X509Certificate.CertificateContext)Marshal.PtrToStructure(handle, typeof(X509Certificate.CertificateContext));
				byte[] array = new byte[certificateContext.cbCertEncoded];
				Marshal.Copy(certificateContext.pbCertEncoded, array, 0, (int)certificateContext.cbCertEncoded);
				this.x509 = new X509Certificate(array);
			}
		}

		/// <summary>Compares two <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects for equality.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object is equal to the object specified by the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to compare to the current object.</param>
		// Token: 0x060039E5 RID: 14821 RVA: 0x000C6868 File Offset: 0x000C4A68
		public virtual bool Equals(X509Certificate other)
		{
			if (other == null)
			{
				return false;
			}
			if (other.x509 == null)
			{
				if (this.x509 == null)
				{
					return true;
				}
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			else
			{
				byte[] rawData = other.x509.RawData;
				if (rawData == null)
				{
					return this.x509 == null || this.x509.RawData == null;
				}
				if (this.x509 == null)
				{
					return false;
				}
				if (this.x509.RawData == null)
				{
					return false;
				}
				if (rawData.Length == this.x509.RawData.Length)
				{
					for (int i = 0; i < rawData.Length; i++)
					{
						if (rawData[i] != this.x509.RawData[i])
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		/// <summary>Returns the hash value for the X.509v3 certificate as an array of bytes.</summary>
		/// <returns>The hash value for the X.509 certificate.</returns>
		// Token: 0x060039E6 RID: 14822 RVA: 0x000C6938 File Offset: 0x000C4B38
		public virtual byte[] GetCertHash()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			if (this.cachedCertificateHash == null && this.x509 != null)
			{
				SHA1 sha = SHA1.Create();
				this.cachedCertificateHash = sha.ComputeHash(this.x509.RawData);
			}
			return this.cachedCertificateHash;
		}

		/// <summary>Returns the hash value for the X.509v3 certificate as a hexadecimal string.</summary>
		/// <returns>The hexadecimal string representation of the X.509 certificate hash value.</returns>
		// Token: 0x060039E7 RID: 14823 RVA: 0x000C699C File Offset: 0x000C4B9C
		public virtual string GetCertHashString()
		{
			return this.tostr(this.GetCertHash());
		}

		/// <summary>Returns the effective date of this X.509v3 certificate.</summary>
		/// <returns>The effective date for this X.509 certificate.</returns>
		// Token: 0x060039E8 RID: 14824 RVA: 0x000C69AC File Offset: 0x000C4BAC
		public virtual string GetEffectiveDateString()
		{
			if (this.hideDates)
			{
				return null;
			}
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.ValidFrom.ToLocalTime().ToString();
		}

		/// <summary>Returns the expiration date of this X.509v3 certificate.</summary>
		/// <returns>The expiration date for this X.509 certificate.</returns>
		// Token: 0x060039E9 RID: 14825 RVA: 0x000C69FC File Offset: 0x000C4BFC
		public virtual string GetExpirationDateString()
		{
			if (this.hideDates)
			{
				return null;
			}
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.ValidUntil.ToLocalTime().ToString();
		}

		/// <summary>Returns the name of the format of this X.509v3 certificate.</summary>
		/// <returns>The format of this X.509 certificate.</returns>
		// Token: 0x060039EA RID: 14826 RVA: 0x000C6A4C File Offset: 0x000C4C4C
		public virtual string GetFormat()
		{
			return "X509";
		}

		/// <summary>Returns the hash code for the X.509v3 certificate as an integer.</summary>
		/// <returns>The hash code for the X.509 certificate as an integer.</returns>
		// Token: 0x060039EB RID: 14827 RVA: 0x000C6A54 File Offset: 0x000C4C54
		public override int GetHashCode()
		{
			if (this.x509 == null)
			{
				return 0;
			}
			if (this.cachedCertificateHash == null)
			{
				this.GetCertHash();
			}
			if (this.cachedCertificateHash != null && this.cachedCertificateHash.Length >= 4)
			{
				return (int)this.cachedCertificateHash[0] << 24 | (int)this.cachedCertificateHash[1] << 16 | (int)this.cachedCertificateHash[2] << 8 | (int)this.cachedCertificateHash[3];
			}
			return 0;
		}

		/// <summary>Returns the name of the certification authority that issued the X.509v3 certificate.</summary>
		/// <returns>The name of the certification authority that issued the X.509 certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:The certificate file does not exist.The certificate is invalid.The certificate's password is incorrect.</exception>
		// Token: 0x060039EC RID: 14828 RVA: 0x000C6AC8 File Offset: 0x000C4CC8
		[Obsolete("Use the Issuer property.")]
		public virtual string GetIssuerName()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.IssuerName;
		}

		/// <summary>Returns the key algorithm information for this X.509v3 certificate.</summary>
		/// <returns>The key algorithm information for this X.509 certificate as a string.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060039ED RID: 14829 RVA: 0x000C6AFC File Offset: 0x000C4CFC
		public virtual string GetKeyAlgorithm()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.KeyAlgorithm;
		}

		/// <summary>Returns the key algorithm parameters for the X.509v3 certificate.</summary>
		/// <returns>The key algorithm parameters for the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060039EE RID: 14830 RVA: 0x000C6B30 File Offset: 0x000C4D30
		public virtual byte[] GetKeyAlgorithmParameters()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			byte[] keyAlgorithmParameters = this.x509.KeyAlgorithmParameters;
			if (keyAlgorithmParameters == null)
			{
				throw new CryptographicException(Locale.GetText("Parameters not part of the certificate"));
			}
			return keyAlgorithmParameters;
		}

		/// <summary>Returns the key algorithm parameters for the X.509v3 certificate.</summary>
		/// <returns>The key algorithm parameters for the X.509 certificate as a hexadecimal string.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060039EF RID: 14831 RVA: 0x000C6B7C File Offset: 0x000C4D7C
		public virtual string GetKeyAlgorithmParametersString()
		{
			return this.tostr(this.GetKeyAlgorithmParameters());
		}

		/// <summary>Returns the name of the principal to which the certificate was issued.</summary>
		/// <returns>The name of the principal to which the certificate was issued.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060039F0 RID: 14832 RVA: 0x000C6B8C File Offset: 0x000C4D8C
		[Obsolete("Use the Subject property.")]
		public virtual string GetName()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.SubjectName;
		}

		/// <summary>Returns the public key for the X.509v3 certificate.</summary>
		/// <returns>The public key for the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060039F1 RID: 14833 RVA: 0x000C6BC0 File Offset: 0x000C4DC0
		public virtual byte[] GetPublicKey()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.PublicKey;
		}

		/// <summary>Returns the public key for the X.509v3 certificate.</summary>
		/// <returns>The public key for the X.509 certificate as a hexadecimal string.</returns>
		// Token: 0x060039F2 RID: 14834 RVA: 0x000C6BF4 File Offset: 0x000C4DF4
		public virtual string GetPublicKeyString()
		{
			return this.tostr(this.GetPublicKey());
		}

		/// <summary>Returns the raw data for the entire X.509v3 certificate.</summary>
		/// <returns>A byte array containing the X.509 certificate data.</returns>
		// Token: 0x060039F3 RID: 14835 RVA: 0x000C6C04 File Offset: 0x000C4E04
		public virtual byte[] GetRawCertData()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.RawData;
		}

		/// <summary>Returns the raw data for the entire X.509v3 certificate.</summary>
		/// <returns>The X.509 certificate data as a hexadecimal string.</returns>
		// Token: 0x060039F4 RID: 14836 RVA: 0x000C6C38 File Offset: 0x000C4E38
		public virtual string GetRawCertDataString()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.tostr(this.x509.RawData);
		}

		/// <summary>Returns the serial number of the X.509v3 certificate.</summary>
		/// <returns>The serial number of the X.509 certificate as an array of bytes.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x060039F5 RID: 14837 RVA: 0x000C6C74 File Offset: 0x000C4E74
		public virtual byte[] GetSerialNumber()
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			return this.x509.SerialNumber;
		}

		/// <summary>Returns the serial number of the X.509v3 certificate.</summary>
		/// <returns>The serial number of the X.509 certificate as a hexadecimal string.</returns>
		// Token: 0x060039F6 RID: 14838 RVA: 0x000C6CA8 File Offset: 0x000C4EA8
		public virtual string GetSerialNumberString()
		{
			byte[] serialNumber = this.GetSerialNumber();
			Array.Reverse(serialNumber);
			return this.tostr(serialNumber);
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		// Token: 0x060039F7 RID: 14839 RVA: 0x000C6CCC File Offset: 0x000C4ECC
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object, with extra information, if specified.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="fVerbose">true to produce the verbose form of the string representation; otherwise, false. </param>
		// Token: 0x060039F8 RID: 14840 RVA: 0x000C6CD4 File Offset: 0x000C4ED4
		public virtual string ToString(bool fVerbose)
		{
			if (!fVerbose || this.x509 == null)
			{
				return base.ToString();
			}
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[Subject]{0}  {1}{0}{0}", newLine, this.Subject);
			stringBuilder.AppendFormat("[Issuer]{0}  {1}{0}{0}", newLine, this.Issuer);
			stringBuilder.AppendFormat("[Not Before]{0}  {1}{0}{0}", newLine, this.GetEffectiveDateString());
			stringBuilder.AppendFormat("[Not After]{0}  {1}{0}{0}", newLine, this.GetExpirationDateString());
			stringBuilder.AppendFormat("[Thumbprint]{0}  {1}{0}", newLine, this.GetCertHashString());
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000C6D74 File Offset: 0x000C4F74
		private static byte[] Load(string fileName)
		{
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return array;
		}

		/// <summary>Gets the name of the certificate authority that issued the X.509v3 certificate.</summary>
		/// <returns>The name of the certificate authority that issued the X.509v3 certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate handle is invalid.</exception>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x000C6DD8 File Offset: 0x000C4FD8
		public string Issuer
		{
			get
			{
				if (this.x509 == null)
				{
					throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
				}
				if (this.issuer_name == null)
				{
					this.issuer_name = X501.ToString(this.x509.GetIssuerName(), true, ", ", true);
				}
				return this.issuer_name;
			}
		}

		/// <summary>Gets the subject distinguished name from the certificate.</summary>
		/// <returns>The subject distinguished name from the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate handle is invalid.</exception>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000C6E30 File Offset: 0x000C5030
		public string Subject
		{
			get
			{
				if (this.x509 == null)
				{
					throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
				}
				if (this.subject_name == null)
				{
					this.subject_name = X501.ToString(this.x509.GetSubjectName(), true, ", ", true);
				}
				return this.subject_name;
			}
		}

		/// <summary>Gets a handle to a Microsoft Cryptographic API certificate context described by an unmanaged PCCERT_CONTEXT structure. </summary>
		/// <returns>An <see cref="T:System.IntPtr" /> structure that represents an unmanaged PCCERT_CONTEXT structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000C6E88 File Offset: 0x000C5088
		[ComVisible(false)]
		public IntPtr Handle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		/// <summary>Compares two <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects for equality.</summary>
		/// <returns>true if the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object is equal to the object specified by the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to compare to the current object. </param>
		// Token: 0x060039FD RID: 14845 RVA: 0x000C6E90 File Offset: 0x000C5090
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			X509Certificate x509Certificate = obj as X509Certificate;
			return x509Certificate != null && this.Equals(x509Certificate);
		}

		/// <summary>Exports the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to a byte array in a format described by one of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values. </summary>
		/// <returns>An array of bytes that represents the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="contentType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values that describes how to format the output data. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A value other than <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, or <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> was passed to the <paramref name="contentType" /> parameter.-or-The certificate could not be exported.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Open, Export" />
		/// </PermissionSet>
		// Token: 0x060039FE RID: 14846 RVA: 0x000C6EB4 File Offset: 0x000C50B4
		[MonoTODO("X509ContentType.Pfx/Pkcs12 and SerializedCert are not supported")]
		[ComVisible(false)]
		public virtual byte[] Export(X509ContentType contentType)
		{
			return this.Export(contentType, null);
		}

		/// <summary>Exports the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to a byte array in a format described by one of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values, and using the specified password.</summary>
		/// <returns>An array of bytes that represents the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="contentType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values that describes how to format the output data.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A value other than <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, or <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> was passed to the <paramref name="contentType" /> parameter.-or-The certificate could not be exported.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Open, Export" />
		/// </PermissionSet>
		// Token: 0x060039FF RID: 14847 RVA: 0x000C6EC0 File Offset: 0x000C50C0
		[MonoTODO("X509ContentType.Pfx/Pkcs12 and SerializedCert are not supported")]
		[ComVisible(false)]
		public virtual byte[] Export(X509ContentType contentType, string password)
		{
			byte[] password2 = (password != null) ? Encoding.UTF8.GetBytes(password) : null;
			return this.Export(contentType, password2);
		}

		/// <summary>Exports the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to a byte array using the specified format and a password.</summary>
		/// <returns>A byte array that represents the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</returns>
		/// <param name="contentType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> values that describes how to format the output data.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A value other than <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Cert" />, <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.SerializedCert" />, or <see cref="F:System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12" /> was passed to the <paramref name="contentType" /> parameter.-or-The certificate could not be exported.</exception>
		// Token: 0x06003A00 RID: 14848 RVA: 0x000C6EF0 File Offset: 0x000C50F0
		[MonoTODO("X509ContentType.Pfx/Pkcs12 and SerializedCert are not supported. SecureString support is incomplete.")]
		public virtual byte[] Export(X509ContentType contentType, SecureString password)
		{
			byte[] password2 = (password != null) ? password.GetBuffer() : null;
			return this.Export(contentType, password2);
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000C6F18 File Offset: 0x000C5118
		internal byte[] Export(X509ContentType contentType, byte[] password)
		{
			if (this.x509 == null)
			{
				throw new CryptographicException(Locale.GetText("Certificate instance is empty."));
			}
			byte[] rawData;
			try
			{
				switch (contentType)
				{
				case X509ContentType.Cert:
					rawData = this.x509.RawData;
					break;
				case X509ContentType.SerializedCert:
					throw new NotSupportedException();
				case X509ContentType.Pfx:
					throw new NotSupportedException();
				default:
				{
					string text = Locale.GetText("This certificate format '{0}' cannot be exported.", new object[]
					{
						contentType
					});
					throw new CryptographicException(text);
				}
				}
			}
			finally
			{
				if (password != null)
				{
					Array.Clear(password, 0, password.Length);
				}
			}
			return rawData;
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with data from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003A02 RID: 14850 RVA: 0x000C6FCC File Offset: 0x000C51CC
		[ComVisible(false)]
		public virtual void Import(byte[] rawData)
		{
			this.Import(rawData, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object using data from a byte array, a password, and flags for determining how the private key is imported.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values that controls where and how the private key is imported. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003A03 RID: 14851 RVA: 0x000C6FD8 File Offset: 0x000C51D8
		[MonoTODO("missing KeyStorageFlags support")]
		[ComVisible(false)]
		public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			if (password == null)
			{
				try
				{
					this.x509 = new X509Certificate(rawData);
				}
				catch (Exception inner)
				{
					try
					{
						PKCS12 pkcs = new PKCS12(rawData);
						if (pkcs.Certificates.Count > 0)
						{
							this.x509 = pkcs.Certificates[0];
						}
						else
						{
							this.x509 = null;
						}
					}
					catch
					{
						string text = Locale.GetText("Unable to decode certificate.");
						throw new CryptographicException(text, inner);
					}
				}
			}
			else
			{
				try
				{
					PKCS12 pkcs2 = new PKCS12(rawData, password);
					if (pkcs2.Certificates.Count > 0)
					{
						this.x509 = pkcs2.Certificates[0];
					}
					else
					{
						this.x509 = null;
					}
				}
				catch
				{
					this.x509 = new X509Certificate(rawData);
				}
			}
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object using data from a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values that controls where and how to import the private key. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="rawData" /> parameter is null.-or-The length of the <paramref name="rawData" /> parameter is 0.</exception>
		// Token: 0x06003A04 RID: 14852 RVA: 0x000C70FC File Offset: 0x000C52FC
		[MonoTODO("SecureString support is incomplete")]
		public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Import(rawData, null, keyStorageFlags);
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with information from a certificate file.</summary>
		/// <param name="fileName">The name of a certificate file represented as a string. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003A05 RID: 14853 RVA: 0x000C7108 File Offset: 0x000C5308
		[ComVisible(false)]
		public virtual void Import(string fileName)
		{
			byte[] rawData = X509Certificate.Load(fileName);
			this.Import(rawData, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Populates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with information from a certificate file, a password, and a <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> value.</summary>
		/// <param name="fileName">The name of a certificate file represented as a string. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values that controls where and how the private key is imported. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003A06 RID: 14854 RVA: 0x000C7128 File Offset: 0x000C5328
		[ComVisible(false)]
		[MonoTODO("missing KeyStorageFlags support")]
		public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			byte[] rawData = X509Certificate.Load(fileName);
			this.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object with information from a certificate file, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file. </param>
		/// <param name="password">The password required to access the X.509 certificate data. </param>
		/// <param name="keyStorageFlags">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> values that controls where and how to import the private key. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="fileName" /> parameter is null.</exception>
		// Token: 0x06003A07 RID: 14855 RVA: 0x000C7148 File Offset: 0x000C5348
		[MonoTODO("SecureString support is incomplete, missing KeyStorageFlags support")]
		public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			byte[] rawData = X509Certificate.Load(fileName);
			this.Import(rawData, null, keyStorageFlags);
		}

		/// <summary>Resets the state of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		// Token: 0x06003A08 RID: 14856 RVA: 0x000C7168 File Offset: 0x000C5368
		[ComVisible(false)]
		public virtual void Reset()
		{
			this.x509 = null;
			this.issuer_name = null;
			this.subject_name = null;
			this.hideDates = false;
			this.cachedCertificateHash = null;
		}

		// Token: 0x04001913 RID: 6419
		private X509Certificate x509;

		// Token: 0x04001914 RID: 6420
		private bool hideDates;

		// Token: 0x04001915 RID: 6421
		private byte[] cachedCertificateHash;

		// Token: 0x04001916 RID: 6422
		private string issuer_name;

		// Token: 0x04001917 RID: 6423
		private string subject_name;

		// Token: 0x020005ED RID: 1517
		internal struct CertificateContext
		{
			// Token: 0x04001918 RID: 6424
			public uint dwCertEncodingType;

			// Token: 0x04001919 RID: 6425
			public IntPtr pbCertEncoded;

			// Token: 0x0400191A RID: 6426
			public uint cbCertEncoded;

			// Token: 0x0400191B RID: 6427
			public IntPtr pCertInfo;

			// Token: 0x0400191C RID: 6428
			public IntPtr hCertStore;
		}
	}
}
