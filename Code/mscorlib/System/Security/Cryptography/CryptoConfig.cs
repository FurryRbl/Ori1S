using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Xml;

namespace System.Security.Cryptography
{
	/// <summary>Accesses the cryptography configuration information.</summary>
	// Token: 0x0200059C RID: 1436
	[ComVisible(true)]
	public class CryptoConfig
	{
		// Token: 0x06003762 RID: 14178 RVA: 0x000B2BC0 File Offset: 0x000B0DC0
		private static void Initialize()
		{
			Hashtable hashtable = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			hashtable.Add("SHA", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("SHA1", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.SHA1", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.HashAlgorithm", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("MD5", "System.Security.Cryptography.MD5CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.MD5", "System.Security.Cryptography.MD5CryptoServiceProvider");
			hashtable.Add("SHA256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("SHA-256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("System.Security.Cryptography.SHA256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("SHA384", "System.Security.Cryptography.SHA384Managed");
			hashtable.Add("SHA-384", "System.Security.Cryptography.SHA384Managed");
			hashtable.Add("System.Security.Cryptography.SHA384", "System.Security.Cryptography.SHA384Managed");
			hashtable.Add("SHA512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("SHA-512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("System.Security.Cryptography.SHA512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("RSA", "System.Security.Cryptography.RSACryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.RSA", "System.Security.Cryptography.RSACryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.AsymmetricAlgorithm", "System.Security.Cryptography.RSACryptoServiceProvider");
			hashtable.Add("DSA", "System.Security.Cryptography.DSACryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.DSA", "System.Security.Cryptography.DSACryptoServiceProvider");
			hashtable.Add("DES", "System.Security.Cryptography.DESCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.DES", "System.Security.Cryptography.DESCryptoServiceProvider");
			hashtable.Add("3DES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("TripleDES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("Triple DES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.TripleDES", "System.Security.Cryptography.TripleDESCryptoServiceProvider");
			hashtable.Add("RC2", "System.Security.Cryptography.RC2CryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.RC2", "System.Security.Cryptography.RC2CryptoServiceProvider");
			hashtable.Add("Rijndael", "System.Security.Cryptography.RijndaelManaged");
			hashtable.Add("System.Security.Cryptography.Rijndael", "System.Security.Cryptography.RijndaelManaged");
			hashtable.Add("System.Security.Cryptography.SymmetricAlgorithm", "System.Security.Cryptography.RijndaelManaged");
			hashtable.Add("RandomNumberGenerator", "System.Security.Cryptography.RNGCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.RandomNumberGenerator", "System.Security.Cryptography.RNGCryptoServiceProvider");
			hashtable.Add("System.Security.Cryptography.KeyedHashAlgorithm", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("HMACSHA1", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("System.Security.Cryptography.HMACSHA1", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("MACTripleDES", "System.Security.Cryptography.MACTripleDES");
			hashtable.Add("System.Security.Cryptography.MACTripleDES", "System.Security.Cryptography.MACTripleDES");
			hashtable.Add("RIPEMD160", "System.Security.Cryptography.RIPEMD160Managed");
			hashtable.Add("RIPEMD-160", "System.Security.Cryptography.RIPEMD160Managed");
			hashtable.Add("System.Security.Cryptography.RIPEMD160", "System.Security.Cryptography.RIPEMD160Managed");
			hashtable.Add("System.Security.Cryptography.HMAC", "System.Security.Cryptography.HMACSHA1");
			hashtable.Add("HMACMD5", "System.Security.Cryptography.HMACMD5");
			hashtable.Add("System.Security.Cryptography.HMACMD5", "System.Security.Cryptography.HMACMD5");
			hashtable.Add("HMACRIPEMD160", "System.Security.Cryptography.HMACRIPEMD160");
			hashtable.Add("System.Security.Cryptography.HMACRIPEMD160", "System.Security.Cryptography.HMACRIPEMD160");
			hashtable.Add("HMACSHA256", "System.Security.Cryptography.HMACSHA256");
			hashtable.Add("System.Security.Cryptography.HMACSHA256", "System.Security.Cryptography.HMACSHA256");
			hashtable.Add("HMACSHA384", "System.Security.Cryptography.HMACSHA384");
			hashtable.Add("System.Security.Cryptography.HMACSHA384", "System.Security.Cryptography.HMACSHA384");
			hashtable.Add("HMACSHA512", "System.Security.Cryptography.HMACSHA512");
			hashtable.Add("System.Security.Cryptography.HMACSHA512", "System.Security.Cryptography.HMACSHA512");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#dsa-sha1", "System.Security.Cryptography.DSASignatureDescription");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#rsa-sha1", "System.Security.Cryptography.RSAPKCS1SHA1SignatureDescription");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#sha1", "System.Security.Cryptography.SHA1CryptoServiceProvider");
			hashtable.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315", "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#base64", "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/TR/1999/REC-xpath-19991116", "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/TR/1999/REC-xslt-19991116", "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature", "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2001/10/xml-exc-c14n#", "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2002/07/decrypt#XML", "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2001/04/xmlenc#sha256", "System.Security.Cryptography.SHA256Managed");
			hashtable.Add("http://www.w3.org/2001/04/xmlenc#sha512", "System.Security.Cryptography.SHA512Managed");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", "System.Security.Cryptography.HMACSHA256");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", "System.Security.Cryptography.HMACSHA384");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", "System.Security.Cryptography.HMACSHA512");
			hashtable.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160", "System.Security.Cryptography.HMACRIPEMD160");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# X509Data", "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# KeyName", "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue", "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue", "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("http://www.w3.org/2000/09/xmldsig# RetrievalMethod", "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			hashtable.Add("2.5.29.14", "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("2.5.29.15", "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("2.5.29.19", "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("2.5.29.37", "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			hashtable.Add("X509Chain", "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			Hashtable hashtable2 = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			hashtable2.Add("System.Security.Cryptography.SHA1CryptoServiceProvider", "1.3.14.3.2.26");
			hashtable2.Add("System.Security.Cryptography.SHA1Managed", "1.3.14.3.2.26");
			hashtable2.Add("SHA1", "1.3.14.3.2.26");
			hashtable2.Add("System.Security.Cryptography.SHA1", "1.3.14.3.2.26");
			hashtable2.Add("System.Security.Cryptography.MD5CryptoServiceProvider", "1.2.840.113549.2.5");
			hashtable2.Add("MD5", "1.2.840.113549.2.5");
			hashtable2.Add("System.Security.Cryptography.MD5", "1.2.840.113549.2.5");
			hashtable2.Add("System.Security.Cryptography.SHA256Managed", "2.16.840.1.101.3.4.2.1");
			hashtable2.Add("SHA256", "2.16.840.1.101.3.4.2.1");
			hashtable2.Add("System.Security.Cryptography.SHA256", "2.16.840.1.101.3.4.2.1");
			hashtable2.Add("System.Security.Cryptography.SHA384Managed", "2.16.840.1.101.3.4.2.2");
			hashtable2.Add("SHA384", "2.16.840.1.101.3.4.2.2");
			hashtable2.Add("System.Security.Cryptography.SHA384", "2.16.840.1.101.3.4.2.2");
			hashtable2.Add("System.Security.Cryptography.SHA512Managed", "2.16.840.1.101.3.4.2.3");
			hashtable2.Add("SHA512", "2.16.840.1.101.3.4.2.3");
			hashtable2.Add("System.Security.Cryptography.SHA512", "2.16.840.1.101.3.4.2.3");
			hashtable2.Add("TripleDESKeyWrap", "1.2.840.113549.1.9.16.3.6");
			hashtable2.Add("DES", "1.3.14.3.2.7");
			hashtable2.Add("TripleDES", "1.2.840.113549.3.7");
			hashtable2.Add("RC2", "1.2.840.113549.3.2");
			string machineConfigPath = Environment.GetMachineConfigPath();
			CryptoConfig.LoadConfig(machineConfigPath, hashtable, hashtable2);
			CryptoConfig.algorithms = hashtable;
			CryptoConfig.oid = hashtable2;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000B3248 File Offset: 0x000B1448
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
		private static void LoadConfig(string filename, Hashtable algorithms, Hashtable oid)
		{
			if (!File.Exists(filename))
			{
				return;
			}
			try
			{
				using (TextReader textReader = new StreamReader(filename))
				{
					CryptoConfig.CryptoHandler handler = new CryptoConfig.CryptoHandler(algorithms, oid);
					SmallXmlParser smallXmlParser = new SmallXmlParser();
					smallXmlParser.Parse(textReader, handler);
				}
			}
			catch
			{
			}
		}

		/// <summary>Creates a new instance of the specified cryptographic object.</summary>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06003764 RID: 14180 RVA: 0x000B32D0 File Offset: 0x000B14D0
		public static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name, null);
		}

		/// <summary>Creates a new instance of the specified cryptographic object with the specified arguments.</summary>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance. </param>
		/// <param name="args">The arguments used to create the specified cryptographic object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06003765 RID: 14181 RVA: 0x000B32DC File Offset: 0x000B14DC
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\"\n               Unrestricted=\"true\"/>\n")]
		public static object CreateFromName(string name, params object[] args)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.algorithms == null)
				{
					CryptoConfig.Initialize();
				}
			}
			object result;
			try
			{
				string text = (string)CryptoConfig.algorithms[name];
				if (text == null)
				{
					text = name;
				}
				Type type = Type.GetType(text);
				result = Activator.CreateInstance(type, args);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		/// <summary>Gets the object identifier (OID) of the algorithm corresponding to the specified simple name.</summary>
		/// <returns>The OID of the specified algorithm.</returns>
		/// <param name="name">The simple name of the algorithm for which to get the OID. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003766 RID: 14182 RVA: 0x000B3398 File Offset: 0x000B1598
		public static string MapNameToOID(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.oid == null)
				{
					CryptoConfig.Initialize();
				}
			}
			return (string)CryptoConfig.oid[name];
		}

		/// <summary>Encodes the specified object identifier (OID).</summary>
		/// <returns>A byte array containing the encoded OID.</returns>
		/// <param name="str">The OID to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">An error occurred while encoding the OID. </exception>
		// Token: 0x06003767 RID: 14183 RVA: 0x000B340C File Offset: 0x000B160C
		public static byte[] EncodeOID(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			char[] separator = new char[]
			{
				'.'
			};
			string[] array = str.Split(separator);
			if (array.Length < 2)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID must have at least two parts"));
			}
			byte[] array2 = new byte[str.Length];
			try
			{
				byte b = Convert.ToByte(array[0]);
				byte b2 = Convert.ToByte(array[1]);
				array2[2] = Convert.ToByte((int)(b * 40 + b2));
			}
			catch
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Invalid OID"));
			}
			int num = 3;
			for (int i = 2; i < array.Length; i++)
			{
				long num2 = Convert.ToInt64(array[i]);
				if (num2 > 127L)
				{
					byte[] array3 = CryptoConfig.EncodeLongNumber(num2);
					Buffer.BlockCopy(array3, 0, array2, num, array3.Length);
					num += array3.Length;
				}
				else
				{
					array2[num++] = Convert.ToByte(num2);
				}
			}
			int num3 = 2;
			byte[] array4 = new byte[num];
			array4[0] = 6;
			if (num > 127)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID > 127 bytes"));
			}
			array4[1] = Convert.ToByte(num - 2);
			Buffer.BlockCopy(array2, num3, array4, num3, num - num3);
			return array4;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000B356C File Offset: 0x000B176C
		private static byte[] EncodeLongNumber(long x)
		{
			if (x > 2147483647L || x < -2147483648L)
			{
				throw new OverflowException(Locale.GetText("Part of OID doesn't fit in Int32"));
			}
			long num = x;
			int num2 = 1;
			while (num > 127L)
			{
				num >>= 7;
				num2++;
			}
			byte[] array = new byte[num2];
			for (int i = 0; i < num2; i++)
			{
				num = x >> 7 * i;
				num &= 127L;
				if (i != 0)
				{
					num += 128L;
				}
				array[num2 - i - 1] = Convert.ToByte(num);
			}
			return array;
		}

		// Token: 0x04001779 RID: 6009
		private const string defaultNamespace = "System.Security.Cryptography.";

		// Token: 0x0400177A RID: 6010
		private const string defaultSHA1 = "System.Security.Cryptography.SHA1CryptoServiceProvider";

		// Token: 0x0400177B RID: 6011
		private const string defaultMD5 = "System.Security.Cryptography.MD5CryptoServiceProvider";

		// Token: 0x0400177C RID: 6012
		private const string defaultSHA256 = "System.Security.Cryptography.SHA256Managed";

		// Token: 0x0400177D RID: 6013
		private const string defaultSHA384 = "System.Security.Cryptography.SHA384Managed";

		// Token: 0x0400177E RID: 6014
		private const string defaultSHA512 = "System.Security.Cryptography.SHA512Managed";

		// Token: 0x0400177F RID: 6015
		private const string defaultRSA = "System.Security.Cryptography.RSACryptoServiceProvider";

		// Token: 0x04001780 RID: 6016
		private const string defaultDSA = "System.Security.Cryptography.DSACryptoServiceProvider";

		// Token: 0x04001781 RID: 6017
		private const string defaultDES = "System.Security.Cryptography.DESCryptoServiceProvider";

		// Token: 0x04001782 RID: 6018
		private const string default3DES = "System.Security.Cryptography.TripleDESCryptoServiceProvider";

		// Token: 0x04001783 RID: 6019
		private const string defaultRC2 = "System.Security.Cryptography.RC2CryptoServiceProvider";

		// Token: 0x04001784 RID: 6020
		private const string defaultAES = "System.Security.Cryptography.RijndaelManaged";

		// Token: 0x04001785 RID: 6021
		private const string defaultRNG = "System.Security.Cryptography.RNGCryptoServiceProvider";

		// Token: 0x04001786 RID: 6022
		private const string defaultHMAC = "System.Security.Cryptography.HMACSHA1";

		// Token: 0x04001787 RID: 6023
		private const string defaultMAC3DES = "System.Security.Cryptography.MACTripleDES";

		// Token: 0x04001788 RID: 6024
		private const string defaultDSASigDesc = "System.Security.Cryptography.DSASignatureDescription";

		// Token: 0x04001789 RID: 6025
		private const string defaultRSASigDesc = "System.Security.Cryptography.RSAPKCS1SHA1SignatureDescription";

		// Token: 0x0400178A RID: 6026
		private const string defaultRIPEMD160 = "System.Security.Cryptography.RIPEMD160Managed";

		// Token: 0x0400178B RID: 6027
		private const string defaultHMACMD5 = "System.Security.Cryptography.HMACMD5";

		// Token: 0x0400178C RID: 6028
		private const string defaultHMACRIPEMD160 = "System.Security.Cryptography.HMACRIPEMD160";

		// Token: 0x0400178D RID: 6029
		private const string defaultHMACSHA256 = "System.Security.Cryptography.HMACSHA256";

		// Token: 0x0400178E RID: 6030
		private const string defaultHMACSHA384 = "System.Security.Cryptography.HMACSHA384";

		// Token: 0x0400178F RID: 6031
		private const string defaultHMACSHA512 = "System.Security.Cryptography.HMACSHA512";

		// Token: 0x04001790 RID: 6032
		private const string defaultC14N = "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001791 RID: 6033
		private const string defaultC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001792 RID: 6034
		private const string defaultBase64 = "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001793 RID: 6035
		private const string defaultXPath = "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001794 RID: 6036
		private const string defaultXslt = "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001795 RID: 6037
		private const string defaultEnveloped = "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001796 RID: 6038
		private const string defaultXmlDecryption = "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001797 RID: 6039
		private const string defaultExcC14N = "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001798 RID: 6040
		private const string defaultExcC14NWithComments = "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001799 RID: 6041
		private const string defaultX509Data = "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400179A RID: 6042
		private const string defaultKeyName = "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400179B RID: 6043
		private const string defaultKeyValueDSA = "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400179C RID: 6044
		private const string defaultKeyValueRSA = "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400179D RID: 6045
		private const string defaultRetrievalMethod = "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400179E RID: 6046
		private const string managedSHA1 = "System.Security.Cryptography.SHA1Managed";

		// Token: 0x0400179F RID: 6047
		private const string oidSHA1 = "1.3.14.3.2.26";

		// Token: 0x040017A0 RID: 6048
		private const string oidMD5 = "1.2.840.113549.2.5";

		// Token: 0x040017A1 RID: 6049
		private const string oidSHA256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x040017A2 RID: 6050
		private const string oidSHA384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x040017A3 RID: 6051
		private const string oidSHA512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x040017A4 RID: 6052
		private const string oidDSA = "1.2.840.10040.4.1";

		// Token: 0x040017A5 RID: 6053
		private const string oidDES = "1.3.14.3.2.7";

		// Token: 0x040017A6 RID: 6054
		private const string oid3DES = "1.2.840.113549.3.7";

		// Token: 0x040017A7 RID: 6055
		private const string oidRC2 = "1.2.840.113549.3.2";

		// Token: 0x040017A8 RID: 6056
		private const string oid3DESKeyWrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x040017A9 RID: 6057
		private const string nameSHA1a = "SHA";

		// Token: 0x040017AA RID: 6058
		private const string nameSHA1b = "SHA1";

		// Token: 0x040017AB RID: 6059
		private const string nameSHA1c = "System.Security.Cryptography.SHA1";

		// Token: 0x040017AC RID: 6060
		private const string nameSHA1d = "System.Security.Cryptography.HashAlgorithm";

		// Token: 0x040017AD RID: 6061
		private const string nameMD5a = "MD5";

		// Token: 0x040017AE RID: 6062
		private const string nameMD5b = "System.Security.Cryptography.MD5";

		// Token: 0x040017AF RID: 6063
		private const string nameSHA256a = "SHA256";

		// Token: 0x040017B0 RID: 6064
		private const string nameSHA256b = "SHA-256";

		// Token: 0x040017B1 RID: 6065
		private const string nameSHA256c = "System.Security.Cryptography.SHA256";

		// Token: 0x040017B2 RID: 6066
		private const string nameSHA384a = "SHA384";

		// Token: 0x040017B3 RID: 6067
		private const string nameSHA384b = "SHA-384";

		// Token: 0x040017B4 RID: 6068
		private const string nameSHA384c = "System.Security.Cryptography.SHA384";

		// Token: 0x040017B5 RID: 6069
		private const string nameSHA512a = "SHA512";

		// Token: 0x040017B6 RID: 6070
		private const string nameSHA512b = "SHA-512";

		// Token: 0x040017B7 RID: 6071
		private const string nameSHA512c = "System.Security.Cryptography.SHA512";

		// Token: 0x040017B8 RID: 6072
		private const string nameRSAa = "RSA";

		// Token: 0x040017B9 RID: 6073
		private const string nameRSAb = "System.Security.Cryptography.RSA";

		// Token: 0x040017BA RID: 6074
		private const string nameRSAc = "System.Security.Cryptography.AsymmetricAlgorithm";

		// Token: 0x040017BB RID: 6075
		private const string nameDSAa = "DSA";

		// Token: 0x040017BC RID: 6076
		private const string nameDSAb = "System.Security.Cryptography.DSA";

		// Token: 0x040017BD RID: 6077
		private const string nameDESa = "DES";

		// Token: 0x040017BE RID: 6078
		private const string nameDESb = "System.Security.Cryptography.DES";

		// Token: 0x040017BF RID: 6079
		private const string name3DESa = "3DES";

		// Token: 0x040017C0 RID: 6080
		private const string name3DESb = "TripleDES";

		// Token: 0x040017C1 RID: 6081
		private const string name3DESc = "Triple DES";

		// Token: 0x040017C2 RID: 6082
		private const string name3DESd = "System.Security.Cryptography.TripleDES";

		// Token: 0x040017C3 RID: 6083
		private const string nameRC2a = "RC2";

		// Token: 0x040017C4 RID: 6084
		private const string nameRC2b = "System.Security.Cryptography.RC2";

		// Token: 0x040017C5 RID: 6085
		private const string nameAESa = "Rijndael";

		// Token: 0x040017C6 RID: 6086
		private const string nameAESb = "System.Security.Cryptography.Rijndael";

		// Token: 0x040017C7 RID: 6087
		private const string nameAESc = "System.Security.Cryptography.SymmetricAlgorithm";

		// Token: 0x040017C8 RID: 6088
		private const string nameRNGa = "RandomNumberGenerator";

		// Token: 0x040017C9 RID: 6089
		private const string nameRNGb = "System.Security.Cryptography.RandomNumberGenerator";

		// Token: 0x040017CA RID: 6090
		private const string nameKeyHasha = "System.Security.Cryptography.KeyedHashAlgorithm";

		// Token: 0x040017CB RID: 6091
		private const string nameHMACSHA1a = "HMACSHA1";

		// Token: 0x040017CC RID: 6092
		private const string nameHMACSHA1b = "System.Security.Cryptography.HMACSHA1";

		// Token: 0x040017CD RID: 6093
		private const string nameMAC3DESa = "MACTripleDES";

		// Token: 0x040017CE RID: 6094
		private const string nameMAC3DESb = "System.Security.Cryptography.MACTripleDES";

		// Token: 0x040017CF RID: 6095
		private const string name3DESKeyWrap = "TripleDESKeyWrap";

		// Token: 0x040017D0 RID: 6096
		private const string nameRIPEMD160a = "RIPEMD160";

		// Token: 0x040017D1 RID: 6097
		private const string nameRIPEMD160b = "RIPEMD-160";

		// Token: 0x040017D2 RID: 6098
		private const string nameRIPEMD160c = "System.Security.Cryptography.RIPEMD160";

		// Token: 0x040017D3 RID: 6099
		private const string nameHMACa = "HMAC";

		// Token: 0x040017D4 RID: 6100
		private const string nameHMACb = "System.Security.Cryptography.HMAC";

		// Token: 0x040017D5 RID: 6101
		private const string nameHMACMD5a = "HMACMD5";

		// Token: 0x040017D6 RID: 6102
		private const string nameHMACMD5b = "System.Security.Cryptography.HMACMD5";

		// Token: 0x040017D7 RID: 6103
		private const string nameHMACRIPEMD160a = "HMACRIPEMD160";

		// Token: 0x040017D8 RID: 6104
		private const string nameHMACRIPEMD160b = "System.Security.Cryptography.HMACRIPEMD160";

		// Token: 0x040017D9 RID: 6105
		private const string nameHMACSHA256a = "HMACSHA256";

		// Token: 0x040017DA RID: 6106
		private const string nameHMACSHA256b = "System.Security.Cryptography.HMACSHA256";

		// Token: 0x040017DB RID: 6107
		private const string nameHMACSHA384a = "HMACSHA384";

		// Token: 0x040017DC RID: 6108
		private const string nameHMACSHA384b = "System.Security.Cryptography.HMACSHA384";

		// Token: 0x040017DD RID: 6109
		private const string nameHMACSHA512a = "HMACSHA512";

		// Token: 0x040017DE RID: 6110
		private const string nameHMACSHA512b = "System.Security.Cryptography.HMACSHA512";

		// Token: 0x040017DF RID: 6111
		private const string urlXmlDsig = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x040017E0 RID: 6112
		private const string urlDSASHA1 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x040017E1 RID: 6113
		private const string urlRSASHA1 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x040017E2 RID: 6114
		private const string urlSHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x040017E3 RID: 6115
		private const string urlC14N = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";

		// Token: 0x040017E4 RID: 6116
		private const string urlC14NWithComments = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";

		// Token: 0x040017E5 RID: 6117
		private const string urlBase64 = "http://www.w3.org/2000/09/xmldsig#base64";

		// Token: 0x040017E6 RID: 6118
		private const string urlXPath = "http://www.w3.org/TR/1999/REC-xpath-19991116";

		// Token: 0x040017E7 RID: 6119
		private const string urlXslt = "http://www.w3.org/TR/1999/REC-xslt-19991116";

		// Token: 0x040017E8 RID: 6120
		private const string urlEnveloped = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x040017E9 RID: 6121
		private const string urlXmlDecryption = "http://www.w3.org/2002/07/decrypt#XML";

		// Token: 0x040017EA RID: 6122
		private const string urlExcC14NWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x040017EB RID: 6123
		private const string urlExcC14N = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x040017EC RID: 6124
		private const string urlSHA256 = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040017ED RID: 6125
		private const string urlSHA512 = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x040017EE RID: 6126
		private const string urlHMACSHA256 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x040017EF RID: 6127
		private const string urlHMACSHA384 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		// Token: 0x040017F0 RID: 6128
		private const string urlHMACSHA512 = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		// Token: 0x040017F1 RID: 6129
		private const string urlHMACRIPEMD160 = "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160";

		// Token: 0x040017F2 RID: 6130
		private const string urlX509Data = "http://www.w3.org/2000/09/xmldsig# X509Data";

		// Token: 0x040017F3 RID: 6131
		private const string urlKeyName = "http://www.w3.org/2000/09/xmldsig# KeyName";

		// Token: 0x040017F4 RID: 6132
		private const string urlKeyValueDSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue";

		// Token: 0x040017F5 RID: 6133
		private const string urlKeyValueRSA = "http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue";

		// Token: 0x040017F6 RID: 6134
		private const string urlRetrievalMethod = "http://www.w3.org/2000/09/xmldsig# RetrievalMethod";

		// Token: 0x040017F7 RID: 6135
		private const string oidX509SubjectKeyIdentifier = "2.5.29.14";

		// Token: 0x040017F8 RID: 6136
		private const string oidX509KeyUsage = "2.5.29.15";

		// Token: 0x040017F9 RID: 6137
		private const string oidX509BasicConstraints = "2.5.29.19";

		// Token: 0x040017FA RID: 6138
		private const string oidX509EnhancedKeyUsage = "2.5.29.37";

		// Token: 0x040017FB RID: 6139
		private const string nameX509SubjectKeyIdentifier = "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040017FC RID: 6140
		private const string nameX509KeyUsage = "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040017FD RID: 6141
		private const string nameX509BasicConstraints = "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040017FE RID: 6142
		private const string nameX509EnhancedKeyUsage = "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040017FF RID: 6143
		private const string nameX509Chain = "X509Chain";

		// Token: 0x04001800 RID: 6144
		private const string defaultX509Chain = "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001801 RID: 6145
		private static object lockObject = new object();

		// Token: 0x04001802 RID: 6146
		private static Hashtable algorithms;

		// Token: 0x04001803 RID: 6147
		private static Hashtable oid;

		// Token: 0x0200059D RID: 1437
		private class CryptoHandler : SmallXmlParser.IContentHandler
		{
			// Token: 0x06003769 RID: 14185 RVA: 0x000B3600 File Offset: 0x000B1800
			public CryptoHandler(Hashtable algorithms, Hashtable oid)
			{
				this.algorithms = algorithms;
				this.oid = oid;
				this.names = new Hashtable();
				this.classnames = new Hashtable();
			}

			// Token: 0x0600376A RID: 14186 RVA: 0x000B3638 File Offset: 0x000B1838
			public void OnStartParsing(SmallXmlParser parser)
			{
			}

			// Token: 0x0600376B RID: 14187 RVA: 0x000B363C File Offset: 0x000B183C
			public void OnEndParsing(SmallXmlParser parser)
			{
				foreach (object obj in this.names)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					try
					{
						this.algorithms.Add(dictionaryEntry.Key, this.classnames[dictionaryEntry.Value]);
					}
					catch
					{
					}
				}
				this.names.Clear();
				this.classnames.Clear();
			}

			// Token: 0x0600376C RID: 14188 RVA: 0x000B3704 File Offset: 0x000B1904
			private string Get(SmallXmlParser.IAttrList attrs, string name)
			{
				for (int i = 0; i < attrs.Names.Length; i++)
				{
					if (attrs.Names[i] == name)
					{
						return attrs.Values[i];
					}
				}
				return string.Empty;
			}

			// Token: 0x0600376D RID: 14189 RVA: 0x000B374C File Offset: 0x000B194C
			public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
			{
				switch (this.level)
				{
				case 0:
					if (name == "configuration")
					{
						this.level++;
					}
					break;
				case 1:
					if (name == "mscorlib")
					{
						this.level++;
					}
					break;
				case 2:
					if (name == "cryptographySettings")
					{
						this.level++;
					}
					break;
				case 3:
					if (name == "oidMap")
					{
						this.level++;
					}
					else if (name == "cryptoNameMapping")
					{
						this.level++;
					}
					break;
				case 4:
					if (name == "oidEntry")
					{
						this.oid.Add(this.Get(attrs, "name"), this.Get(attrs, "OID"));
					}
					else if (name == "nameEntry")
					{
						this.names.Add(this.Get(attrs, "name"), this.Get(attrs, "class"));
					}
					else if (name == "cryptoClasses")
					{
						this.level++;
					}
					break;
				case 5:
					if (name == "cryptoClass")
					{
						this.classnames.Add(attrs.Names[0], attrs.Values[0]);
					}
					break;
				}
			}

			// Token: 0x0600376E RID: 14190 RVA: 0x000B38F8 File Offset: 0x000B1AF8
			public void OnEndElement(string name)
			{
				switch (this.level)
				{
				case 1:
					if (name == "configuration")
					{
						this.level--;
					}
					break;
				case 2:
					if (name == "mscorlib")
					{
						this.level--;
					}
					break;
				case 3:
					if (name == "cryptographySettings")
					{
						this.level--;
					}
					break;
				case 4:
					if (name == "oidMap" || name == "cryptoNameMapping")
					{
						this.level--;
					}
					break;
				case 5:
					if (name == "cryptoClasses")
					{
						this.level--;
					}
					break;
				}
			}

			// Token: 0x0600376F RID: 14191 RVA: 0x000B39EC File Offset: 0x000B1BEC
			public void OnProcessingInstruction(string name, string text)
			{
			}

			// Token: 0x06003770 RID: 14192 RVA: 0x000B39F0 File Offset: 0x000B1BF0
			public void OnChars(string text)
			{
			}

			// Token: 0x06003771 RID: 14193 RVA: 0x000B39F4 File Offset: 0x000B1BF4
			public void OnIgnorableWhitespace(string text)
			{
			}

			// Token: 0x04001804 RID: 6148
			private Hashtable algorithms;

			// Token: 0x04001805 RID: 6149
			private Hashtable oid;

			// Token: 0x04001806 RID: 6150
			private Hashtable names;

			// Token: 0x04001807 RID: 6151
			private Hashtable classnames;

			// Token: 0x04001808 RID: 6152
			private int level;
		}
	}
}
