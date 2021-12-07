using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000029 RID: 41
	public abstract class DiffieHellman : AsymmetricAlgorithm
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000B6CC File Offset: 0x000098CC
		public new static DiffieHellman Create()
		{
			return DiffieHellman.Create("Mono.Security.Cryptography.DiffieHellman");
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public new static DiffieHellman Create(string algName)
		{
			return (DiffieHellman)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x060001B3 RID: 435
		public abstract byte[] CreateKeyExchange();

		// Token: 0x060001B4 RID: 436
		public abstract byte[] DecryptKeyExchange(byte[] keyex);

		// Token: 0x060001B5 RID: 437
		public abstract DHParameters ExportParameters(bool includePrivate);

		// Token: 0x060001B6 RID: 438
		public abstract void ImportParameters(DHParameters parameters);

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B6E8 File Offset: 0x000098E8
		private byte[] GetNamedParam(SecurityElement se, string param)
		{
			SecurityElement securityElement = se.SearchForChildByTag(param);
			if (securityElement == null)
			{
				return null;
			}
			return Convert.FromBase64String(securityElement.Text);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B710 File Offset: 0x00009910
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DHParameters parameters = default(DHParameters);
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xmlString);
				SecurityElement securityElement = securityParser.ToXml();
				if (securityElement.Tag != "DHKeyValue")
				{
					throw new CryptographicException();
				}
				parameters.P = this.GetNamedParam(securityElement, "P");
				parameters.G = this.GetNamedParam(securityElement, "G");
				parameters.X = this.GetNamedParam(securityElement, "X");
				this.ImportParameters(parameters);
			}
			finally
			{
				if (parameters.P != null)
				{
					Array.Clear(parameters.P, 0, parameters.P.Length);
				}
				if (parameters.G != null)
				{
					Array.Clear(parameters.G, 0, parameters.G.Length);
				}
				if (parameters.X != null)
				{
					Array.Clear(parameters.X, 0, parameters.X.Length);
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B830 File Offset: 0x00009A30
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DHParameters dhparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<DHKeyValue>");
				stringBuilder.Append("<P>");
				stringBuilder.Append(Convert.ToBase64String(dhparameters.P));
				stringBuilder.Append("</P>");
				stringBuilder.Append("<G>");
				stringBuilder.Append(Convert.ToBase64String(dhparameters.G));
				stringBuilder.Append("</G>");
				if (includePrivateParameters)
				{
					stringBuilder.Append("<X>");
					stringBuilder.Append(Convert.ToBase64String(dhparameters.X));
					stringBuilder.Append("</X>");
				}
				stringBuilder.Append("</DHKeyValue>");
			}
			finally
			{
				Array.Clear(dhparameters.P, 0, dhparameters.P.Length);
				Array.Clear(dhparameters.G, 0, dhparameters.G.Length);
				if (dhparameters.X != null)
				{
					Array.Clear(dhparameters.X, 0, dhparameters.X.Length);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
