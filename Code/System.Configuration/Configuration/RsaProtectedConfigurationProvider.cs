using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> instance that uses RSA encryption to encrypt and decrypt configuration data.</summary>
	// Token: 0x0200006B RID: 107
	public sealed class RsaProtectedConfigurationProvider : ProtectedConfigurationProvider
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00009DDC File Offset: 0x00007FDC
		private RSACryptoServiceProvider GetProvider()
		{
			if (this.rsa == null)
			{
				CspParameters cspParameters = new CspParameters();
				cspParameters.ProviderName = this.cspProviderName;
				cspParameters.KeyContainerName = this.keyContainerName;
				if (this.useMachineContainer)
				{
					cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
				}
				this.rsa = new RSACryptoServiceProvider(cspParameters);
			}
			return this.rsa;
		}

		/// <summary>Decrypts the XML node passed to it.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing decrypted data.</returns>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> to decrypt.</param>
		// Token: 0x0600039D RID: 925 RVA: 0x00009E40 File Offset: 0x00008040
		[MonoTODO]
		public override XmlNode Decrypt(XmlNode encrypted_node)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(new StringReader(encrypted_node.OuterXml));
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			encryptedXml.AddKeyNameMapping("Rsa Key", this.GetProvider());
			encryptedXml.DecryptDocument();
			return xmlDocument.DocumentElement;
		}

		/// <summary>Encrypts the XML node passed to it.</summary>
		/// <returns>An encrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> to encrypt.</param>
		// Token: 0x0600039E RID: 926 RVA: 0x00009E88 File Offset: 0x00008088
		[MonoTODO]
		public override XmlNode Encrypt(XmlNode node)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(new StringReader(node.OuterXml));
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			encryptedXml.AddKeyNameMapping("Rsa Key", this.GetProvider());
			EncryptedData encryptedData = encryptedXml.Encrypt(xmlDocument.DocumentElement, "Rsa Key");
			return encryptedData.GetXml();
		}

		/// <summary>Initializes the provider with default settings.</summary>
		/// <param name="name">The provider name to use for the object.</param>
		/// <param name="configurationValues">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection of values to use when initializing the object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="configurationValues" /> includes one or more unrecognized values.</exception>
		// Token: 0x0600039F RID: 927 RVA: 0x00009EDC File Offset: 0x000080DC
		[MonoTODO]
		public override void Initialize(string name, NameValueCollection configurationValues)
		{
			base.Initialize(name, configurationValues);
			this.keyContainerName = configurationValues["keyContainerName"];
			this.cspProviderName = configurationValues["cspProviderName"];
			string text = configurationValues["useMachineContainer"];
			if (text != null && text.ToLower() == "true")
			{
				this.useMachineContainer = true;
			}
			text = configurationValues["useOAEP"];
			if (text != null && text.ToLower() == "true")
			{
				this.useOAEP = true;
			}
		}

		/// <summary>Adds a key to the RSA key container.</summary>
		/// <param name="keySize">The size of the key to add.</param>
		/// <param name="exportable">true to indicate that the key is exportable; otherwise, false.</param>
		// Token: 0x060003A0 RID: 928 RVA: 0x00009F70 File Offset: 0x00008170
		[MonoTODO]
		public void AddKey(int keySize, bool exportable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a key from the RSA key container.</summary>
		// Token: 0x060003A1 RID: 929 RVA: 0x00009F78 File Offset: 0x00008178
		[MonoTODO]
		public void DeleteKey()
		{
			throw new NotImplementedException();
		}

		/// <summary>Exports an RSA key from the key container.</summary>
		/// <param name="xmlFileName">The file name and path to export the key to.</param>
		/// <param name="includePrivateParameters">true to indicate that private parameters are exported; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">An error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060003A2 RID: 930 RVA: 0x00009F80 File Offset: 0x00008180
		[MonoTODO]
		public void ExportKey(string xmlFileName, bool includePrivateParameters)
		{
			RSACryptoServiceProvider provider = this.GetProvider();
			string value = provider.ToXmlString(includePrivateParameters);
			FileStream stream = new FileStream(xmlFileName, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write(value);
			streamWriter.Close();
		}

		/// <summary>Imports an RSA key into the key container.</summary>
		/// <param name="xmlFileName">The file name and path to import the key from.</param>
		/// <param name="exportable">true to indicate that the key is exportable; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">An error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is write-only.-or- This operation is not supported on the current platform.-or- <paramref name="path" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format. </exception>
		// Token: 0x060003A3 RID: 931 RVA: 0x00009FBC File Offset: 0x000081BC
		[MonoTODO]
		public void ImportKey(string xmlFileName, bool exportable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the name of the Windows cryptography API (crypto API) cryptographic service provider (CSP).</summary>
		/// <returns>The name of the CryptoAPI cryptographic service provider.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00009FC4 File Offset: 0x000081C4
		public string CspProviderName
		{
			get
			{
				return this.cspProviderName;
			}
		}

		/// <summary>Gets the name of the key container.</summary>
		/// <returns>The name of the key container.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00009FCC File Offset: 0x000081CC
		public string KeyContainerName
		{
			get
			{
				return this.keyContainerName;
			}
		}

		/// <summary>Gets the public key used by the provider.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.RSAParameters" /> object that contains the public key used by the provider.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00009FD4 File Offset: 0x000081D4
		public RSAParameters RsaPublicKey
		{
			get
			{
				RSACryptoServiceProvider provider = this.GetProvider();
				return provider.ExportParameters(false);
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Configuration.RsaProtectedConfigurationProvider" /> object is using the machine key container.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.RsaProtectedConfigurationProvider" /> object is using the machine key container; otherwise, false.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00009FF0 File Offset: 0x000081F0
		public bool UseMachineContainer
		{
			get
			{
				return this.useMachineContainer;
			}
		}

		/// <summary>Gets a value that indicates whether the provider is using Optimal Asymmetric Encryption Padding (OAEP) key exchange data.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.RsaProtectedConfigurationProvider" /> object is using Optimal Asymmetric Encryption Padding (OAEP) key exchange data; otherwise, false.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00009FF8 File Offset: 0x000081F8
		public bool UseOAEP
		{
			get
			{
				return this.useOAEP;
			}
		}

		// Token: 0x0400011F RID: 287
		private string cspProviderName;

		// Token: 0x04000120 RID: 288
		private string keyContainerName;

		// Token: 0x04000121 RID: 289
		private bool useMachineContainer;

		// Token: 0x04000122 RID: 290
		private bool useOAEP;

		// Token: 0x04000123 RID: 291
		private RSACryptoServiceProvider rsa;
	}
}
