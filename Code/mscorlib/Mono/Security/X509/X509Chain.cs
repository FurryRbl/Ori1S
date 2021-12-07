using System;
using System.Security.Permissions;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x020000CB RID: 203
	internal class X509Chain
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x00034950 File Offset: 0x00032B50
		public X509Chain()
		{
			this.certs = new X509CertificateCollection();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00034964 File Offset: 0x00032B64
		public X509Chain(X509CertificateCollection chain) : this()
		{
			this._chain = new X509CertificateCollection();
			this._chain.AddRange(chain);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00034984 File Offset: 0x00032B84
		public X509CertificateCollection Chain
		{
			get
			{
				return this._chain;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0003498C File Offset: 0x00032B8C
		public X509Certificate Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00034994 File Offset: 0x00032B94
		public X509ChainStatusFlags Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0003499C File Offset: 0x00032B9C
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x000349D4 File Offset: 0x00032BD4
		public X509CertificateCollection TrustAnchors
		{
			get
			{
				if (this.roots == null)
				{
					this.roots = new X509CertificateCollection();
					this.roots.AddRange(X509StoreManager.TrustedRootCertificates);
					return this.roots;
				}
				return this.roots;
			}
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			set
			{
				this.roots = value;
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000349E0 File Offset: 0x00032BE0
		public void LoadCertificate(X509Certificate x509)
		{
			this.certs.Add(x509);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000349F0 File Offset: 0x00032BF0
		public void LoadCertificates(X509CertificateCollection collection)
		{
			this.certs.AddRange(collection);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00034A00 File Offset: 0x00032C00
		public X509Certificate FindByIssuerName(string issuerName)
		{
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (x509Certificate.IssuerName == issuerName)
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00034A80 File Offset: 0x00032C80
		public bool Build(X509Certificate leaf)
		{
			this._status = X509ChainStatusFlags.NoError;
			if (this._chain == null)
			{
				this._chain = new X509CertificateCollection();
				X509Certificate x509Certificate = leaf;
				X509Certificate potentialRoot = x509Certificate;
				while (x509Certificate != null && !x509Certificate.IsSelfSigned)
				{
					potentialRoot = x509Certificate;
					this._chain.Add(x509Certificate);
					x509Certificate = this.FindCertificateParent(x509Certificate);
				}
				this._root = this.FindCertificateRoot(potentialRoot);
			}
			else
			{
				int count = this._chain.Count;
				if (count > 0)
				{
					if (this.IsParent(leaf, this._chain[0]))
					{
						int i;
						for (i = 1; i < count; i++)
						{
							if (!this.IsParent(this._chain[i - 1], this._chain[i]))
							{
								break;
							}
						}
						if (i == count)
						{
							this._root = this.FindCertificateRoot(this._chain[count - 1]);
						}
					}
				}
				else
				{
					this._root = this.FindCertificateRoot(leaf);
				}
			}
			if (this._chain != null && this._status == X509ChainStatusFlags.NoError)
			{
				foreach (X509Certificate cert in this._chain)
				{
					if (!this.IsValid(cert))
					{
						return false;
					}
				}
				if (!this.IsValid(leaf))
				{
					if (this._status == X509ChainStatusFlags.NotTimeNested)
					{
						this._status = X509ChainStatusFlags.NotTimeValid;
					}
					return false;
				}
				if (this._root != null && !this.IsValid(this._root))
				{
					return false;
				}
			}
			return this._status == X509ChainStatusFlags.NoError;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00034C5C File Offset: 0x00032E5C
		public void Reset()
		{
			this._status = X509ChainStatusFlags.NoError;
			this.roots = null;
			this.certs.Clear();
			if (this._chain != null)
			{
				this._chain.Clear();
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00034C90 File Offset: 0x00032E90
		private bool IsValid(X509Certificate cert)
		{
			if (!cert.IsCurrent)
			{
				this._status = X509ChainStatusFlags.NotTimeNested;
				return false;
			}
			return true;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00034CA8 File Offset: 0x00032EA8
		private X509Certificate FindCertificateParent(X509Certificate child)
		{
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (this.IsParent(child, x509Certificate))
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00034D24 File Offset: 0x00032F24
		private X509Certificate FindCertificateRoot(X509Certificate potentialRoot)
		{
			if (potentialRoot == null)
			{
				this._status = X509ChainStatusFlags.PartialChain;
				return null;
			}
			if (this.IsTrusted(potentialRoot))
			{
				return potentialRoot;
			}
			foreach (X509Certificate x509Certificate in this.TrustAnchors)
			{
				if (this.IsParent(potentialRoot, x509Certificate))
				{
					return x509Certificate;
				}
			}
			if (potentialRoot.IsSelfSigned)
			{
				this._status = X509ChainStatusFlags.UntrustedRoot;
				return potentialRoot;
			}
			this._status = X509ChainStatusFlags.PartialChain;
			return null;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00034DE0 File Offset: 0x00032FE0
		private bool IsTrusted(X509Certificate potentialTrusted)
		{
			return this.TrustAnchors.Contains(potentialTrusted);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00034DF0 File Offset: 0x00032FF0
		private bool IsParent(X509Certificate child, X509Certificate parent)
		{
			if (child.IssuerName != parent.SubjectName)
			{
				return false;
			}
			if (parent.Version > 2 && !this.IsTrusted(parent))
			{
				X509Extension x509Extension = parent.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					BasicConstraintsExtension basicConstraintsExtension = new BasicConstraintsExtension(x509Extension);
					if (!basicConstraintsExtension.CertificateAuthority)
					{
						this._status = X509ChainStatusFlags.InvalidBasicConstraints;
					}
				}
				else
				{
					this._status = X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (!child.VerifySignature(parent.RSA))
			{
				this._status = X509ChainStatusFlags.NotSignatureValid;
				return false;
			}
			return true;
		}

		// Token: 0x04000302 RID: 770
		private X509CertificateCollection roots;

		// Token: 0x04000303 RID: 771
		private X509CertificateCollection certs;

		// Token: 0x04000304 RID: 772
		private X509Certificate _root;

		// Token: 0x04000305 RID: 773
		private X509CertificateCollection _chain;

		// Token: 0x04000306 RID: 774
		private X509ChainStatusFlags _status;
	}
}
