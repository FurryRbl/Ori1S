using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the process model for implementing XML encryption.</summary>
	// Token: 0x0200003D RID: 61
	public class EncryptedXml
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		// Token: 0x06000180 RID: 384 RVA: 0x00007E2C File Offset: 0x0000602C
		[MonoTODO]
		public EncryptedXml()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		// Token: 0x06000181 RID: 385 RVA: 0x00007E64 File Offset: 0x00006064
		[MonoTODO]
		public EncryptedXml(XmlDocument document)
		{
			this.document = document;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document and evidence.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		/// <param name="evidence">An <see cref="T:System.Security.Policy.Evidence" /> object associated with the <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000182 RID: 386 RVA: 0x00007E98 File Offset: 0x00006098
		[MonoTODO]
		public EncryptedXml(XmlDocument document, Evidence evidence)
		{
			this.document = document;
			this.DocumentEvidence = evidence;
		}

		/// <summary>Gets or sets the evidence of the <see cref="T:System.Xml.XmlDocument" /> object from which the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object is constructed.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00007EE0 File Offset: 0x000060E0
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00007EE8 File Offset: 0x000060E8
		public Evidence DocumentEvidence
		{
			get
			{
				return this.documentEvidence;
			}
			set
			{
				this.documentEvidence = value;
			}
		}

		/// <summary>Gets or sets the encoding used for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00007EF4 File Offset: 0x000060F4
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00007EFC File Offset: 0x000060FC
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the cipher mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.CipherMode" /> values.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007F08 File Offset: 0x00006108
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00007F10 File Offset: 0x00006110
		public CipherMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		/// <summary>Gets or sets the padding mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values that specifies the type of padding used for encryption.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007F1C File Offset: 0x0000611C
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00007F24 File Offset: 0x00006124
		public PaddingMode Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
			}
		}

		/// <summary>Gets or sets the recipient of the encrypted key information.</summary>
		/// <returns>The recipient of the encrypted key information.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007F30 File Offset: 0x00006130
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00007F38 File Offset: 0x00006138
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
			set
			{
				this.recipient = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlResolver" /> object used by the Document Object Model (DOM) to resolve external XML references.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> object.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007F44 File Offset: 0x00006144
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007F4C File Offset: 0x0000614C
		public XmlResolver Resolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		/// <summary>Defines a mapping between a key name and a symmetric key or an asymmetric key.</summary>
		/// <param name="keyName">The name to map to <paramref name="keyObject" />.</param>
		/// <param name="keyObject">The symmetric key to map to <paramref name="keyName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyName" /> parameter is null.-or-The value of the <paramref name="keyObject" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyObject" /> parameter is not an RSA algorithm or a symmetric key. </exception>
		// Token: 0x0600018F RID: 399 RVA: 0x00007F58 File Offset: 0x00006158
		public void AddKeyNameMapping(string keyName, object keyObject)
		{
			this.keyNameMapping[keyName] = keyObject;
		}

		/// <summary>Resets all key name mapping.</summary>
		// Token: 0x06000190 RID: 400 RVA: 0x00007F68 File Offset: 0x00006168
		public void ClearKeyNameMappings()
		{
			this.keyNameMapping.Clear();
		}

		/// <summary>Decrypts an &lt;EncryptedData&gt; element using the specified symmetric algorithm.</summary>
		/// <returns>A byte array that contains the raw decrypted plain text.</returns>
		/// <param name="encryptedData">The data to decrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="encryptedData" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000191 RID: 401 RVA: 0x00007F78 File Offset: 0x00006178
		public byte[] DecryptData(EncryptedData encryptedData, SymmetricAlgorithm symAlg)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symAlg == null)
			{
				throw new ArgumentNullException("symAlg");
			}
			PaddingMode paddingMode = symAlg.Padding;
			byte[] result;
			try
			{
				symAlg.Padding = this.Padding;
				result = this.Transform(encryptedData.CipherData.CipherValue, symAlg.CreateDecryptor(), symAlg.BlockSize / 8, true);
			}
			finally
			{
				symAlg.Padding = paddingMode;
			}
			return result;
		}

		/// <summary>Decrypts all &lt;EncryptedData&gt; elements of the XML document that were specified during initialization of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic key used to decrypt the document was not found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000192 RID: 402 RVA: 0x0000800C File Offset: 0x0000620C
		public void DecryptDocument()
		{
			XmlNodeList elementsByTagName = this.document.GetElementsByTagName("EncryptedData", "http://www.w3.org/2001/04/xmlenc#");
			foreach (object obj in elementsByTagName)
			{
				XmlNode xmlNode = (XmlNode)obj;
				EncryptedData encryptedData = new EncryptedData();
				encryptedData.LoadXml((XmlElement)xmlNode);
				SymmetricAlgorithm decryptionKey = this.GetDecryptionKey(encryptedData, encryptedData.EncryptionMethod.KeyAlgorithm);
				this.ReplaceData((XmlElement)xmlNode, this.DecryptData(encryptedData, decryptionKey));
			}
		}

		/// <summary>Determines the key represented by the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> element.</summary>
		/// <returns>A byte array that contains the key.</returns>
		/// <param name="encryptedKey">The <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that contains the key to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedKey" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedKey" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael). </exception>
		// Token: 0x06000193 RID: 403 RVA: 0x000080C4 File Offset: 0x000062C4
		public virtual byte[] DecryptEncryptedKey(EncryptedKey encryptedKey)
		{
			if (encryptedKey == null)
			{
				throw new ArgumentNullException("encryptedKey");
			}
			object obj = null;
			foreach (object obj2 in encryptedKey.KeyInfo)
			{
				KeyInfoClause keyInfoClause = (KeyInfoClause)obj2;
				if (keyInfoClause is KeyInfoName)
				{
					obj = this.keyNameMapping[((KeyInfoName)keyInfoClause).Value];
					break;
				}
			}
			string keyAlgorithm = encryptedKey.EncryptionMethod.KeyAlgorithm;
			if (keyAlgorithm != null)
			{
				if (EncryptedXml.<>f__switch$map8 == null)
				{
					EncryptedXml.<>f__switch$map8 = new Dictionary<string, int>(2)
					{
						{
							"http://www.w3.org/2001/04/xmlenc#rsa-1_5",
							0
						},
						{
							"http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p",
							1
						}
					};
				}
				int num;
				if (EncryptedXml.<>f__switch$map8.TryGetValue(keyAlgorithm, out num))
				{
					if (num == 0)
					{
						return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (RSA)obj, false);
					}
					if (num == 1)
					{
						return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (RSA)obj, true);
					}
				}
			}
			return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (SymmetricAlgorithm)obj);
		}

		/// <summary>Decrypts an &lt;EncryptedKey&gt; element using a symmetric algorithm.</summary>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <param name="keyData">An array of bytes that represents an encrypted &lt;EncryptedKey&gt; element.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> element is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael). </exception>
		// Token: 0x06000194 RID: 404 RVA: 0x0000821C File Offset: 0x0000641C
		public static byte[] DecryptKey(byte[] keyData, SymmetricAlgorithm symAlg)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symAlg == null)
			{
				throw new ArgumentNullException("symAlg");
			}
			if (symAlg is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapDecrypt(symAlg.Key, keyData);
			}
			if (symAlg is Rijndael)
			{
				return SymmetricKeyWrap.AESKeyWrapDecrypt(symAlg.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Decrypts an &lt;EncryptedKey&gt; element using an asymmetric algorithm.</summary>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <param name="keyData">An array of bytes that represents an encrypted &lt;EncryptedKey&gt; element.</param>
		/// <param name="rsa">The asymmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="rsa" /> parameter is null.</exception>
		// Token: 0x06000195 RID: 405 RVA: 0x00008288 File Offset: 0x00006488
		[MonoTODO("Test this.")]
		public static byte[] DecryptKey(byte[] keyData, RSA rsa, bool fOAEP)
		{
			AsymmetricKeyExchangeDeformatter asymmetricKeyExchangeDeformatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeDeformatter = new RSAOAEPKeyExchangeDeformatter(rsa);
			}
			else
			{
				asymmetricKeyExchangeDeformatter = new RSAPKCS1KeyExchangeDeformatter(rsa);
			}
			return asymmetricKeyExchangeDeformatter.DecryptKeyExchange(keyData);
		}

		/// <summary>Encrypts the outer XML of an element using the specified key in the key mapping table.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that represents the encrypted XML data.</returns>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="keyName">A key name that can be found in the key mapping table.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="keyName" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyName" /> parameter does not match a registered key name pair.-or-The cryptographic key described by the <paramref name="keyName" /> parameter is not supported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000196 RID: 406 RVA: 0x000082B8 File Offset: 0x000064B8
		public EncryptedData Encrypt(XmlElement inputElement, string keyName)
		{
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
			symmetricAlgorithm.KeySize = 256;
			symmetricAlgorithm.GenerateKey();
			symmetricAlgorithm.GenerateIV();
			EncryptedData encryptedData = new EncryptedData();
			EncryptedKey encryptedKey = new EncryptedKey();
			object obj = this.keyNameMapping[keyName];
			encryptedKey.EncryptionMethod = new EncryptionMethod(EncryptedXml.GetKeyWrapAlgorithmUri(obj));
			if (obj is RSA)
			{
				encryptedKey.CipherData = new CipherData(EncryptedXml.EncryptKey(symmetricAlgorithm.Key, (RSA)obj, false));
			}
			else
			{
				encryptedKey.CipherData = new CipherData(EncryptedXml.EncryptKey(symmetricAlgorithm.Key, (SymmetricAlgorithm)obj));
			}
			encryptedKey.KeyInfo = new KeyInfo();
			encryptedKey.KeyInfo.AddClause(new KeyInfoName(keyName));
			encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
			encryptedData.EncryptionMethod = new EncryptionMethod(EncryptedXml.GetAlgorithmUri(symmetricAlgorithm));
			encryptedData.KeyInfo = new KeyInfo();
			encryptedData.KeyInfo.AddClause(new KeyInfoEncryptedKey(encryptedKey));
			encryptedData.CipherData = new CipherData(this.EncryptData(inputElement, symmetricAlgorithm, false));
			return encryptedData;
		}

		/// <summary>Encrypts the outer XML of an element using the specified X.509 certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> element that represents the encrypted XML data.</returns>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="certificate">The X.509 certificate to use for encryption.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="certificate" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The value of the <paramref name="certificate" /> parameter does not represent an RSA key algorithm.</exception>
		// Token: 0x06000197 RID: 407 RVA: 0x000083C4 File Offset: 0x000065C4
		[MonoTODO]
		public EncryptedData Encrypt(XmlElement inputElement, X509Certificate2 certificate)
		{
			throw new NotImplementedException();
		}

		/// <summary>Encrypts data in the specified byte array using the specified symmetric algorithm.</summary>
		/// <returns>A byte array of encrypted data.</returns>
		/// <param name="plaintext">The data to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="plaintext" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could not be encrypted using the specified parameters.</exception>
		// Token: 0x06000198 RID: 408 RVA: 0x000083CC File Offset: 0x000065CC
		public byte[] EncryptData(byte[] plainText, SymmetricAlgorithm symAlg)
		{
			if (plainText == null)
			{
				throw new ArgumentNullException("plainText");
			}
			if (symAlg == null)
			{
				throw new ArgumentNullException("symAlg");
			}
			PaddingMode paddingMode = symAlg.Padding;
			byte[] result;
			try
			{
				symAlg.Padding = this.Padding;
				result = this.EncryptDataCore(plainText, symAlg);
			}
			finally
			{
				symAlg.Padding = paddingMode;
			}
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008448 File Offset: 0x00006648
		private byte[] EncryptDataCore(byte[] plainText, SymmetricAlgorithm symAlg)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(symAlg.IV);
			binaryWriter.Write(this.Transform(plainText, symAlg.CreateEncryptor()));
			binaryWriter.Flush();
			byte[] result = memoryStream.ToArray();
			binaryWriter.Close();
			memoryStream.Close();
			return result;
		}

		/// <summary>Encrypts the specified element or its contents using the specified symmetric algorithm.</summary>
		/// <returns>A byte array that contains the encrypted data.</returns>
		/// <param name="inputElement">The element or its contents to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <param name="content">true to encrypt only the contents of the element; false to encrypt the entire element.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		// Token: 0x0600019A RID: 410 RVA: 0x0000849C File Offset: 0x0000669C
		public byte[] EncryptData(XmlElement inputElement, SymmetricAlgorithm symAlg, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (content)
			{
				return this.EncryptData(this.Encoding.GetBytes(inputElement.InnerXml), symAlg);
			}
			return this.EncryptData(this.Encoding.GetBytes(inputElement.OuterXml), symAlg);
		}

		/// <summary>Encrypts a key using a symmetric algorithm that a recipient uses to decrypt an &lt;EncryptedData&gt; element.</summary>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="symmetricAlgorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael). </exception>
		// Token: 0x0600019B RID: 411 RVA: 0x000084F4 File Offset: 0x000066F4
		public static byte[] EncryptKey(byte[] keyData, SymmetricAlgorithm symAlg)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symAlg == null)
			{
				throw new ArgumentNullException("symAlg");
			}
			if (symAlg is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapEncrypt(symAlg.Key, keyData);
			}
			if (symAlg is Rijndael)
			{
				return SymmetricKeyWrap.AESKeyWrapEncrypt(symAlg.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Encrypts the key that a recipient uses to decrypt an &lt;EncryptedData&gt; element.</summary>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="rsa">The asymmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is null.-or-The value of the <paramref name="rsa" /> parameter is null.</exception>
		// Token: 0x0600019C RID: 412 RVA: 0x00008560 File Offset: 0x00006760
		[MonoTODO("Test this.")]
		public static byte[] EncryptKey(byte[] keyData, RSA rsa, bool fOAEP)
		{
			AsymmetricKeyExchangeFormatter asymmetricKeyExchangeFormatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeFormatter = new RSAOAEPKeyExchangeFormatter(rsa);
			}
			else
			{
				asymmetricKeyExchangeFormatter = new RSAPKCS1KeyExchangeFormatter(rsa);
			}
			return asymmetricKeyExchangeFormatter.CreateKeyExchange(keyData);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008590 File Offset: 0x00006790
		private static SymmetricAlgorithm GetAlgorithm(string symAlgUri)
		{
			if (symAlgUri != null)
			{
				if (EncryptedXml.<>f__switch$map9 == null)
				{
					EncryptedXml.<>f__switch$map9 = new Dictionary<string, int>(9)
					{
						{
							"http://www.w3.org/2001/04/xmlenc#aes128-cbc",
							0
						},
						{
							"http://www.w3.org/2001/04/xmlenc#kw-aes128",
							0
						},
						{
							"http://www.w3.org/2001/04/xmlenc#aes192-cbc",
							1
						},
						{
							"http://www.w3.org/2001/04/xmlenc#kw-aes192",
							1
						},
						{
							"http://www.w3.org/2001/04/xmlenc#aes256-cbc",
							2
						},
						{
							"http://www.w3.org/2001/04/xmlenc#kw-aes256",
							2
						},
						{
							"http://www.w3.org/2001/04/xmlenc#des-cbc",
							3
						},
						{
							"http://www.w3.org/2001/04/xmlenc#tripledes-cbc",
							4
						},
						{
							"http://www.w3.org/2001/04/xmlenc#kw-tripledes",
							4
						}
					};
				}
				int num;
				if (EncryptedXml.<>f__switch$map9.TryGetValue(symAlgUri, out num))
				{
					SymmetricAlgorithm symmetricAlgorithm;
					switch (num)
					{
					case 0:
						symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
						symmetricAlgorithm.KeySize = 128;
						break;
					case 1:
						symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
						symmetricAlgorithm.KeySize = 192;
						break;
					case 2:
						symmetricAlgorithm = SymmetricAlgorithm.Create("Rijndael");
						symmetricAlgorithm.KeySize = 256;
						break;
					case 3:
						symmetricAlgorithm = SymmetricAlgorithm.Create("DES");
						break;
					case 4:
						symmetricAlgorithm = SymmetricAlgorithm.Create("TripleDES");
						break;
					default:
						goto IL_130;
					}
					return symmetricAlgorithm;
				}
			}
			IL_130:
			throw new CryptographicException("symAlgUri");
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000086DC File Offset: 0x000068DC
		private static string GetAlgorithmUri(SymmetricAlgorithm symAlg)
		{
			if (symAlg is Rijndael)
			{
				int keySize = symAlg.KeySize;
				if (keySize == 128)
				{
					return "http://www.w3.org/2001/04/xmlenc#aes128-cbc";
				}
				if (keySize == 192)
				{
					return "http://www.w3.org/2001/04/xmlenc#aes192-cbc";
				}
				if (keySize == 256)
				{
					return "http://www.w3.org/2001/04/xmlenc#aes256-cbc";
				}
			}
			else
			{
				if (symAlg is DES)
				{
					return "http://www.w3.org/2001/04/xmlenc#des-cbc";
				}
				if (symAlg is TripleDES)
				{
					return "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";
				}
			}
			throw new ArgumentException("symAlg");
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008764 File Offset: 0x00006964
		private static string GetKeyWrapAlgorithmUri(object keyAlg)
		{
			if (keyAlg is Rijndael)
			{
				int keySize = ((Rijndael)keyAlg).KeySize;
				if (keySize == 128)
				{
					return "http://www.w3.org/2001/04/xmlenc#kw-aes128";
				}
				if (keySize == 192)
				{
					return "http://www.w3.org/2001/04/xmlenc#kw-aes192";
				}
				if (keySize == 256)
				{
					return "http://www.w3.org/2001/04/xmlenc#kw-aes256";
				}
			}
			else
			{
				if (keyAlg is RSA)
				{
					return "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
				}
				if (keyAlg is TripleDES)
				{
					return "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
				}
			}
			throw new ArgumentException("keyAlg");
		}

		/// <summary>Retrieves the decryption initialization vector (IV) from an <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <returns>A byte array that contains the decryption initialization vector (IV).</returns>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the initialization vector (IV) to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The Uniform Resource Identifier (URI) that describes the cryptographic algorithm associated with the <paramref name="encryptedData" /> value.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedData" /> parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" />  property that is null.-or-The value of the <paramref name="symmetricAlgorithmUrisymAlgUri" /> parameter is not a supported algorithm.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001A0 RID: 416 RVA: 0x000087F4 File Offset: 0x000069F4
		public virtual byte[] GetDecryptionIV(EncryptedData encryptedData, string symAlgUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			SymmetricAlgorithm algorithm = EncryptedXml.GetAlgorithm(symAlgUri);
			byte[] array = new byte[algorithm.BlockSize / 8];
			Buffer.BlockCopy(encryptedData.CipherData.CipherValue, 0, array, 0, array.Length);
			return array;
		}

		/// <summary>Retrieves the decryption key from the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> object associated with the decryption key.</returns>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the decryption key to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The size of the decryption key to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encryptedData parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" /> property that is null.-or-The encrypted key cannot be retrieved using the specified parameters.</exception>
		// Token: 0x060001A1 RID: 417 RVA: 0x00008840 File Offset: 0x00006A40
		public virtual SymmetricAlgorithm GetDecryptionKey(EncryptedData encryptedData, string symAlgUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symAlgUri == null)
			{
				return null;
			}
			SymmetricAlgorithm algorithm = EncryptedXml.GetAlgorithm(symAlgUri);
			algorithm.IV = this.GetDecryptionIV(encryptedData, encryptedData.EncryptionMethod.KeyAlgorithm);
			KeyInfo keyInfo = encryptedData.KeyInfo;
			foreach (object obj in keyInfo)
			{
				KeyInfoClause keyInfoClause = (KeyInfoClause)obj;
				if (keyInfoClause is KeyInfoEncryptedKey)
				{
					algorithm.Key = this.DecryptEncryptedKey(((KeyInfoEncryptedKey)keyInfoClause).EncryptedKey);
					break;
				}
			}
			return algorithm;
		}

		/// <summary>Determines how to resolve internal Uniform Resource Identifier (URI) references.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that contains an ID indicating how internal Uniform Resource Identifiers (URIs) are to be resolved.</returns>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object that contains an element with an ID value.</param>
		/// <param name="idValue">A string that represents the ID value.</param>
		// Token: 0x060001A2 RID: 418 RVA: 0x00008910 File Offset: 0x00006B10
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			if (document == null || idValue == null)
			{
				return null;
			}
			XmlElement xmlElement = document.GetElementById(idValue);
			if (xmlElement == null)
			{
				xmlElement = (XmlElement)document.SelectSingleNode("//*[@Id='" + idValue + "']");
			}
			return xmlElement;
		}

		/// <summary>Replaces an &lt;EncryptedData&gt; element with a specified decrypted sequence of bytes.</summary>
		/// <param name="inputElement">The &lt;EncryptedData&gt; element to replace.</param>
		/// <param name="decryptedData">The decrypted data to replace <paramref name="inputElement" /> with.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="decryptedData" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001A3 RID: 419 RVA: 0x00008958 File Offset: 0x00006B58
		public void ReplaceData(XmlElement inputElement, byte[] decryptedData)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (decryptedData == null)
			{
				throw new ArgumentNullException("decryptedData");
			}
			XmlDocument ownerDocument = inputElement.OwnerDocument;
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.Encoding.GetString(decryptedData, 0, decryptedData.Length)));
			xmlTextReader.MoveToContent();
			XmlNode newChild = ownerDocument.ReadNode(xmlTextReader);
			inputElement.ParentNode.ReplaceChild(newChild, inputElement);
		}

		/// <summary>Replaces the specified element with the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="inputElement">The element to replace with an &lt;EncryptedData&gt; element.</param>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object to replace the <paramref name="inputElement" /> parameter with.</param>
		/// <param name="content">true to replace only the contents of the element; false to replace the entire element.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is null.-or-The value of the <paramref name="encryptedData" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001A4 RID: 420 RVA: 0x000089C8 File Offset: 0x00006BC8
		public static void ReplaceElement(XmlElement inputElement, EncryptedData encryptedData, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			XmlDocument ownerDocument = inputElement.OwnerDocument;
			inputElement.ParentNode.ReplaceChild(encryptedData.GetXml(ownerDocument), inputElement);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008A14 File Offset: 0x00006C14
		private byte[] Transform(byte[] data, ICryptoTransform transform)
		{
			return this.Transform(data, transform, 0, false);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008A20 File Offset: 0x00006C20
		private byte[] Transform(byte[] data, ICryptoTransform transform, int blockOctetCount, bool trimPadding)
		{
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(data, 0, data.Length);
			cryptoStream.FlushFinalBlock();
			int num = 0;
			if (trimPadding)
			{
				num = (int)memoryStream.GetBuffer()[(int)(checked((IntPtr)(unchecked(memoryStream.Length - 1L))))];
			}
			if (num > blockOctetCount)
			{
				num = 0;
			}
			byte[] array = new byte[memoryStream.Length - (long)blockOctetCount - (long)num];
			Array.Copy(memoryStream.GetBuffer(), blockOctetCount, array, 0, array.Length);
			cryptoStream.Close();
			memoryStream.Close();
			return array;
		}

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant. </summary>
		// Token: 0x040000C3 RID: 195
		public const string XmlEncAES128KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x040000C4 RID: 196
		public const string XmlEncAES128Url = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		// Token: 0x040000C5 RID: 197
		public const string XmlEncAES192KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x040000C6 RID: 198
		public const string XmlEncAES192Url = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		// Token: 0x040000C7 RID: 199
		public const string XmlEncAES256KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x040000C8 RID: 200
		public const string XmlEncAES256Url = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Digital Encryption Standard (DES) algorithm. This field is constant.</summary>
		// Token: 0x040000C9 RID: 201
		public const string XmlEncDESUrl = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption element content. This field is constant.</summary>
		// Token: 0x040000CA RID: 202
		public const string XmlEncElementContentUrl = "http://www.w3.org/2001/04/xmlenc#Content";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for an XML encryption element. This field is constant.</summary>
		// Token: 0x040000CB RID: 203
		public const string XmlEncElementUrl = "http://www.w3.org/2001/04/xmlenc#Element";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the XML encryption &lt;EncryptedKey&gt; element. This field is constant.</summary>
		// Token: 0x040000CC RID: 204
		public const string XmlEncEncryptedKeyUrl = "http://www.w3.org/2001/04/xmlenc#EncryptedKey";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption syntax and processing. This field is constant.</summary>
		// Token: 0x040000CD RID: 205
		public const string XmlEncNamespaceUrl = "http://www.w3.org/2001/04/xmlenc#";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Public Key Cryptography Standard (PKCS) Version 1.5 algorithm. This field is constant.</summary>
		// Token: 0x040000CE RID: 206
		public const string XmlEncRSA15Url = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Optimal Asymmetric Encryption Padding (OAEP) encryption algorithm. This field is constant.</summary>
		// Token: 0x040000CF RID: 207
		public const string XmlEncRSAOAEPUrl = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-256 algorithm. This field is constant.</summary>
		// Token: 0x040000D0 RID: 208
		public const string XmlEncSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-512 algorithm. This field is constant.</summary>
		// Token: 0x040000D1 RID: 209
		public const string XmlEncSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the TRIPLEDES key wrap algorithm. This field is constant.</summary>
		// Token: 0x040000D2 RID: 210
		public const string XmlEncTripleDESKeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Triple DES algorithm. This field is constant.</summary>
		// Token: 0x040000D3 RID: 211
		public const string XmlEncTripleDESUrl = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		// Token: 0x040000D4 RID: 212
		private Evidence documentEvidence;

		// Token: 0x040000D5 RID: 213
		private Encoding encoding = Encoding.UTF8;

		// Token: 0x040000D6 RID: 214
		internal Hashtable keyNameMapping = new Hashtable();

		// Token: 0x040000D7 RID: 215
		private CipherMode mode = CipherMode.CBC;

		// Token: 0x040000D8 RID: 216
		private PaddingMode padding = PaddingMode.ISO10126;

		// Token: 0x040000D9 RID: 217
		private string recipient;

		// Token: 0x040000DA RID: 218
		private XmlResolver resolver;

		// Token: 0x040000DB RID: 219
		private XmlDocument document;
	}
}
