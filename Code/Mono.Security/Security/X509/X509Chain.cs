using System;
using System.Net;
using System.Security.Permissions;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x02000047 RID: 71
	public class X509Chain
	{
		// Token: 0x06000332 RID: 818 RVA: 0x000167A8 File Offset: 0x000149A8
		public X509Chain()
		{
			this.certs = new X509CertificateCollection();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000167BC File Offset: 0x000149BC
		public X509Chain(X509CertificateCollection chain) : this()
		{
			this._chain = new X509CertificateCollection();
			this._chain.AddRange(chain);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000334 RID: 820 RVA: 0x000167DC File Offset: 0x000149DC
		public X509CertificateCollection Chain
		{
			get
			{
				return this._chain;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000167E4 File Offset: 0x000149E4
		public X509Certificate Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000167EC File Offset: 0x000149EC
		public X509ChainStatusFlags Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000167F4 File Offset: 0x000149F4
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0001682C File Offset: 0x00014A2C
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
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			set
			{
				this.roots = value;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00016838 File Offset: 0x00014A38
		public void LoadCertificate(X509Certificate x509)
		{
			this.certs.Add(x509);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00016848 File Offset: 0x00014A48
		public void LoadCertificates(X509CertificateCollection collection)
		{
			this.certs.AddRange(collection);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00016858 File Offset: 0x00014A58
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

		// Token: 0x0600033C RID: 828 RVA: 0x000168D8 File Offset: 0x00014AD8
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

		// Token: 0x0600033D RID: 829 RVA: 0x00016AB4 File Offset: 0x00014CB4
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

		// Token: 0x0600033E RID: 830 RVA: 0x00016AE8 File Offset: 0x00014CE8
		private bool IsValid(X509Certificate cert)
		{
			if (!cert.IsCurrent)
			{
				this._status = X509ChainStatusFlags.NotTimeNested;
				return false;
			}
			if (ServicePointManager.CheckCertificateRevocationList)
			{
			}
			return true;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00016B0C File Offset: 0x00014D0C
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

		// Token: 0x06000340 RID: 832 RVA: 0x00016B88 File Offset: 0x00014D88
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

		// Token: 0x06000341 RID: 833 RVA: 0x00016C44 File Offset: 0x00014E44
		private bool IsTrusted(X509Certificate potentialTrusted)
		{
			return this.TrustAnchors.Contains(potentialTrusted);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00016C54 File Offset: 0x00014E54
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

		// Token: 0x04000183 RID: 387
		private X509CertificateCollection roots;

		// Token: 0x04000184 RID: 388
		private X509CertificateCollection certs;

		// Token: 0x04000185 RID: 389
		private X509Certificate _root;

		// Token: 0x04000186 RID: 390
		private X509CertificateCollection _chain;

		// Token: 0x04000187 RID: 391
		private X509ChainStatusFlags _status;
	}
}
