using System;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) must inherit.</summary>
	// Token: 0x020005A9 RID: 1449
	[ComVisible(true)]
	public abstract class DSA : AsymmetricAlgorithm
	{
		/// <summary>Creates the default cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the asymmetric algorithm.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060037C4 RID: 14276 RVA: 0x000B5110 File Offset: 0x000B3310
		public new static DSA Create()
		{
			return DSA.Create("System.Security.Cryptography.DSA");
		}

		/// <summary>Creates the specified cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the asymmetric algorithm.</returns>
		/// <param name="algName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.DSA" /> to use. </param>
		// Token: 0x060037C5 RID: 14277 RVA: 0x000B511C File Offset: 0x000B331C
		public new static DSA Create(string algName)
		{
			return (DSA)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>When overridden in a derived class, creates the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		// Token: 0x060037C6 RID: 14278
		public abstract byte[] CreateSignature(byte[] rgbHash);

		/// <summary>When overridden in a derived class, exports the <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x060037C7 RID: 14279
		public abstract DSAParameters ExportParameters(bool includePrivateParameters);

		// Token: 0x060037C8 RID: 14280 RVA: 0x000B512C File Offset: 0x000B332C
		internal void ZeroizePrivateKey(DSAParameters parameters)
		{
			if (parameters.X != null)
			{
				Array.Clear(parameters.X, 0, parameters.X.Length);
			}
		}

		/// <summary>Reconstructs a <see cref="T:System.Security.Cryptography.DSA" /> object from an XML string.</summary>
		/// <param name="xmlString">The XML string to use to reconstruct the <see cref="T:System.Security.Cryptography.DSA" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlString" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The format of the <paramref name="xmlString" /> parameter is not valid. </exception>
		// Token: 0x060037C9 RID: 14281 RVA: 0x000B515C File Offset: 0x000B335C
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DSAParameters parameters = default(DSAParameters);
			try
			{
				parameters.P = AsymmetricAlgorithm.GetNamedParam(xmlString, "P");
				parameters.Q = AsymmetricAlgorithm.GetNamedParam(xmlString, "Q");
				parameters.G = AsymmetricAlgorithm.GetNamedParam(xmlString, "G");
				parameters.J = AsymmetricAlgorithm.GetNamedParam(xmlString, "J");
				parameters.Y = AsymmetricAlgorithm.GetNamedParam(xmlString, "Y");
				parameters.X = AsymmetricAlgorithm.GetNamedParam(xmlString, "X");
				parameters.Seed = AsymmetricAlgorithm.GetNamedParam(xmlString, "Seed");
				byte[] namedParam = AsymmetricAlgorithm.GetNamedParam(xmlString, "PgenCounter");
				if (namedParam != null)
				{
					byte[] array = new byte[4];
					Buffer.BlockCopy(namedParam, 0, array, 0, namedParam.Length);
					parameters.Counter = BitConverterLE.ToInt32(array, 0);
				}
				this.ImportParameters(parameters);
			}
			catch
			{
				this.ZeroizePrivateKey(parameters);
				throw;
			}
			finally
			{
				this.ZeroizePrivateKey(parameters);
			}
		}

		/// <summary>When overridden in a derived class, imports the specified <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.DSA" />. </param>
		// Token: 0x060037CA RID: 14282
		public abstract void ImportParameters(DSAParameters parameters);

		/// <summary>Creates and returns an XML string representation of the current <see cref="T:System.Security.Cryptography.DSA" /> object.</summary>
		/// <returns>An XML string encoding of the current <see cref="T:System.Security.Cryptography.DSA" /> object.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x060037CB RID: 14283 RVA: 0x000B528C File Offset: 0x000B348C
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DSAParameters parameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<DSAKeyValue>");
				stringBuilder.Append("<P>");
				stringBuilder.Append(Convert.ToBase64String(parameters.P));
				stringBuilder.Append("</P>");
				stringBuilder.Append("<Q>");
				stringBuilder.Append(Convert.ToBase64String(parameters.Q));
				stringBuilder.Append("</Q>");
				stringBuilder.Append("<G>");
				stringBuilder.Append(Convert.ToBase64String(parameters.G));
				stringBuilder.Append("</G>");
				stringBuilder.Append("<Y>");
				stringBuilder.Append(Convert.ToBase64String(parameters.Y));
				stringBuilder.Append("</Y>");
				if (parameters.J != null)
				{
					stringBuilder.Append("<J>");
					stringBuilder.Append(Convert.ToBase64String(parameters.J));
					stringBuilder.Append("</J>");
				}
				if (parameters.Seed != null)
				{
					stringBuilder.Append("<Seed>");
					stringBuilder.Append(Convert.ToBase64String(parameters.Seed));
					stringBuilder.Append("</Seed>");
					stringBuilder.Append("<PgenCounter>");
					if (parameters.Counter != 0)
					{
						byte[] bytes = BitConverterLE.GetBytes(parameters.Counter);
						int num = bytes.Length;
						while (bytes[num - 1] == 0)
						{
							num--;
						}
						stringBuilder.Append(Convert.ToBase64String(bytes, 0, num));
					}
					else
					{
						stringBuilder.Append("AA==");
					}
					stringBuilder.Append("</PgenCounter>");
				}
				if (parameters.X != null)
				{
					stringBuilder.Append("<X>");
					stringBuilder.Append(Convert.ToBase64String(parameters.X));
					stringBuilder.Append("</X>");
				}
				else if (includePrivateParameters)
				{
					throw new ArgumentNullException("X");
				}
				stringBuilder.Append("</DSAKeyValue>");
			}
			catch
			{
				this.ZeroizePrivateKey(parameters);
				throw;
			}
			return stringBuilder.ToString();
		}

		/// <summary>When overridden in a derived class, verifies the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The hash of the data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbData" />. </param>
		// Token: 0x060037CC RID: 14284
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);
	}
}
