using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Represents the identity of a software publisher. This class cannot be inherited.</summary>
	// Token: 0x02000612 RID: 1554
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003B29 RID: 15145 RVA: 0x000CB0A4 File Offset: 0x000C92A4
		public PublisherIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> class with the specified Authenticode X.509v3 certificate.</summary>
		/// <param name="certificate">An X.509 certificate representing the software publisher's identity. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> parameter is not a valid certificate. </exception>
		// Token: 0x06003B2A RID: 15146 RVA: 0x000CB0B4 File Offset: 0x000C92B4
		public PublisherIdentityPermission(X509Certificate certificate)
		{
			this.Certificate = certificate;
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000CB0C4 File Offset: 0x000C92C4
		int IBuiltInPermission.GetTokenIndex()
		{
			return 10;
		}

		/// <summary>Gets or sets an Authenticode X.509v3 certificate that represents the identity of the software publisher.</summary>
		/// <returns>An X.509 certificate representing the identity of the software publisher.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> is not a valid certificate. </exception>
		/// <exception cref="T:System.NotSupportedException">The property cannot be set because the identity is ambiguous.</exception>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000CB0C8 File Offset: 0x000C92C8
		// (set) Token: 0x06003B2D RID: 15149 RVA: 0x000CB0D0 File Offset: 0x000C92D0
		public X509Certificate Certificate
		{
			get
			{
				return this.x509;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("X509Certificate");
				}
				this.x509 = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003B2E RID: 15150 RVA: 0x000CB0EC File Offset: 0x000C92EC
		public override IPermission Copy()
		{
			PublisherIdentityPermission publisherIdentityPermission = new PublisherIdentityPermission(PermissionState.None);
			if (this.x509 != null)
			{
				publisherIdentityPermission.Certificate = this.x509;
			}
			return publisherIdentityPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x06003B2F RID: 15151 RVA: 0x000CB118 File Offset: 0x000C9318
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attributes["X509v3Certificate"] as string;
			if (text != null)
			{
				byte[] data = CryptoConvert.FromHex(text);
				this.x509 = new X509Certificate(data);
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B30 RID: 15152 RVA: 0x000CB164 File Offset: 0x000C9364
		public override IPermission Intersect(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return null;
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
			{
				return new PublisherIdentityPermission(publisherIdentityPermission.x509);
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B31 RID: 15153 RVA: 0x000CB1C4 File Offset: 0x000C93C4
		public override bool IsSubsetOf(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			return publisherIdentityPermission != null && (this.x509 == null || (publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString()));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003B32 RID: 15154 RVA: 0x000CB218 File Offset: 0x000C9418
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.x509 != null)
			{
				securityElement.AddAttribute("X509v3Certificate", this.x509.GetRawCertDataString());
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The two permissions are not equal.</exception>
		// Token: 0x06003B33 RID: 15155 RVA: 0x000CB250 File Offset: 0x000C9450
		public override IPermission Union(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return this.Copy();
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null)
			{
				if (this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			else
			{
				if (this.x509 == null && publisherIdentityPermission.x509 != null)
				{
					return new PublisherIdentityPermission(publisherIdentityPermission.x509);
				}
				if (this.x509 != null && publisherIdentityPermission.x509 == null)
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			return null;
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000CB300 File Offset: 0x000C9500
		private PublisherIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PublisherIdentityPermission publisherIdentityPermission = target as PublisherIdentityPermission;
			if (publisherIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(PublisherIdentityPermission));
			}
			return publisherIdentityPermission;
		}

		// Token: 0x040019BF RID: 6591
		private const int version = 1;

		// Token: 0x040019C0 RID: 6592
		private X509Certificate x509;
	}
}
