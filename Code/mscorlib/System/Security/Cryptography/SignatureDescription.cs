using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains information about the properties of a digital signature.</summary>
	// Token: 0x020005E4 RID: 1508
	[ComVisible(true)]
	public class SignatureDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class.</summary>
		// Token: 0x06003987 RID: 14727 RVA: 0x000C57B8 File Offset: 0x000C39B8
		public SignatureDescription()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="el">The <see cref="T:System.Security.SecurityElement" /> from which to get the algorithms for the signature description. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="el" /> parameter is null.</exception>
		// Token: 0x06003988 RID: 14728 RVA: 0x000C57C0 File Offset: 0x000C39C0
		public SignatureDescription(SecurityElement el)
		{
			if (el == null)
			{
				throw new ArgumentNullException("el");
			}
			SecurityElement securityElement = el.SearchForChildByTag("Deformatter");
			this._DeformatterAlgorithm = ((securityElement != null) ? securityElement.Text : null);
			securityElement = el.SearchForChildByTag("Digest");
			this._DigestAlgorithm = ((securityElement != null) ? securityElement.Text : null);
			securityElement = el.SearchForChildByTag("Formatter");
			this._FormatterAlgorithm = ((securityElement != null) ? securityElement.Text : null);
			securityElement = el.SearchForChildByTag("Key");
			this._KeyAlgorithm = ((securityElement != null) ? securityElement.Text : null);
		}

		/// <summary>Gets or sets the deformatter algorithm for the signature description.</summary>
		/// <returns>The deformatter algorithm for the signature description.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x000C5874 File Offset: 0x000C3A74
		// (set) Token: 0x0600398A RID: 14730 RVA: 0x000C587C File Offset: 0x000C3A7C
		public string DeformatterAlgorithm
		{
			get
			{
				return this._DeformatterAlgorithm;
			}
			set
			{
				this._DeformatterAlgorithm = value;
			}
		}

		/// <summary>Gets or sets the digest algorithm for the signature description.</summary>
		/// <returns>The digest algorithm for the signature description.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000C5888 File Offset: 0x000C3A88
		// (set) Token: 0x0600398C RID: 14732 RVA: 0x000C5890 File Offset: 0x000C3A90
		public string DigestAlgorithm
		{
			get
			{
				return this._DigestAlgorithm;
			}
			set
			{
				this._DigestAlgorithm = value;
			}
		}

		/// <summary>Gets or sets the formatter algorithm for the signature description.</summary>
		/// <returns>The formatter algorithm for the signature description.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000C589C File Offset: 0x000C3A9C
		// (set) Token: 0x0600398E RID: 14734 RVA: 0x000C58A4 File Offset: 0x000C3AA4
		public string FormatterAlgorithm
		{
			get
			{
				return this._FormatterAlgorithm;
			}
			set
			{
				this._FormatterAlgorithm = value;
			}
		}

		/// <summary>Gets or sets the key algorithm for the signature description.</summary>
		/// <returns>The key algorithm for the signature description.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000C58B0 File Offset: 0x000C3AB0
		// (set) Token: 0x06003990 RID: 14736 RVA: 0x000C58B8 File Offset: 0x000C3AB8
		public string KeyAlgorithm
		{
			get
			{
				return this._KeyAlgorithm;
			}
			set
			{
				this._KeyAlgorithm = value;
			}
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.DeformatterAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance.</returns>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" />. </param>
		// Token: 0x06003991 RID: 14737 RVA: 0x000C58C4 File Offset: 0x000C3AC4
		public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			if (this._DeformatterAlgorithm == null)
			{
				throw new ArgumentNullException("DeformatterAlgorithm");
			}
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this._DeformatterAlgorithm);
			if (this._KeyAlgorithm == null)
			{
				throw new NullReferenceException("KeyAlgorithm");
			}
			asymmetricSignatureDeformatter.SetKey(key);
			return asymmetricSignatureDeformatter;
		}

		/// <summary>Creates a <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance using the <see cref="P:System.Security.Cryptography.SignatureDescription.DigestAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003992 RID: 14738 RVA: 0x000C5918 File Offset: 0x000C3B18
		public virtual HashAlgorithm CreateDigest()
		{
			if (this._DigestAlgorithm == null)
			{
				throw new ArgumentNullException("DigestAlgorithm");
			}
			return (HashAlgorithm)CryptoConfig.CreateFromName(this._DigestAlgorithm);
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.FormatterAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance.</returns>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" />. </param>
		// Token: 0x06003993 RID: 14739 RVA: 0x000C594C File Offset: 0x000C3B4C
		public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			if (this._FormatterAlgorithm == null)
			{
				throw new ArgumentNullException("FormatterAlgorithm");
			}
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this._FormatterAlgorithm);
			if (this._KeyAlgorithm == null)
			{
				throw new NullReferenceException("KeyAlgorithm");
			}
			asymmetricSignatureFormatter.SetKey(key);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x040018FC RID: 6396
		private string _DeformatterAlgorithm;

		// Token: 0x040018FD RID: 6397
		private string _DigestAlgorithm;

		// Token: 0x040018FE RID: 6398
		private string _FormatterAlgorithm;

		// Token: 0x040018FF RID: 6399
		private string _KeyAlgorithm;
	}
}
