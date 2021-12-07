using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000613 RID: 1555
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003B35 RID: 15157 RVA: 0x000CB334 File Offset: 0x000C9534
		public PublisherIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets a certification file containing an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of an X.509 certificate file (usually has the extension.cer).</returns>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x000CB340 File Offset: 0x000C9540
		// (set) Token: 0x06003B37 RID: 15159 RVA: 0x000CB348 File Offset: 0x000C9548
		public string CertFile
		{
			get
			{
				return this.certFile;
			}
			set
			{
				this.certFile = value;
			}
		}

		/// <summary>Gets or sets a signed file from which to extract an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of a file signed with the Authenticode signature.</returns>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x000CB354 File Offset: 0x000C9554
		// (set) Token: 0x06003B39 RID: 15161 RVA: 0x000CB35C File Offset: 0x000C955C
		public string SignedFile
		{
			get
			{
				return this.signedFile;
			}
			set
			{
				this.signedFile = value;
			}
		}

		/// <summary>Gets or sets an Authenticode X.509v3 certificate identifying the publisher of the calling code.</summary>
		/// <returns>A hexadecimal representation of the X.509 certificate.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x000CB368 File Offset: 0x000C9568
		// (set) Token: 0x06003B3B RID: 15163 RVA: 0x000CB370 File Offset: 0x000C9570
		public string X509Certificate
		{
			get
			{
				return this.x509data;
			}
			set
			{
				this.x509data = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.PublisherIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> that corresponds to this attribute.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003B3C RID: 15164 RVA: 0x000CB37C File Offset: 0x000C957C
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.x509data != null)
			{
				byte[] data = CryptoConvert.FromHex(this.x509data);
				X509Certificate certificate = new X509Certificate(data);
				return new PublisherIdentityPermission(certificate);
			}
			if (this.certFile != null)
			{
				X509Certificate certificate = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.certFile);
				return new PublisherIdentityPermission(certificate);
			}
			if (this.signedFile != null)
			{
				X509Certificate certificate = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.signedFile);
				return new PublisherIdentityPermission(certificate);
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x040019C1 RID: 6593
		private string certFile;

		// Token: 0x040019C2 RID: 6594
		private string signedFile;

		// Token: 0x040019C3 RID: 6595
		private string x509data;
	}
}
