using System;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x020000D2 RID: 210
	internal class X509Stores
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x0003666C File Offset: 0x0003486C
		internal X509Stores(string path)
		{
			this._storePath = path;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0003667C File Offset: 0x0003487C
		public X509Store Personal
		{
			get
			{
				if (this._personal == null)
				{
					string path = Path.Combine(this._storePath, "My");
					this._personal = new X509Store(path, false);
				}
				return this._personal;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000366B8 File Offset: 0x000348B8
		public X509Store OtherPeople
		{
			get
			{
				if (this._other == null)
				{
					string path = Path.Combine(this._storePath, "AddressBook");
					this._other = new X509Store(path, false);
				}
				return this._other;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x000366F4 File Offset: 0x000348F4
		public X509Store IntermediateCA
		{
			get
			{
				if (this._intermediate == null)
				{
					string path = Path.Combine(this._storePath, "CA");
					this._intermediate = new X509Store(path, true);
				}
				return this._intermediate;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00036730 File Offset: 0x00034930
		public X509Store TrustedRoot
		{
			get
			{
				if (this._trusted == null)
				{
					string path = Path.Combine(this._storePath, "Trust");
					this._trusted = new X509Store(path, true);
				}
				return this._trusted;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0003676C File Offset: 0x0003496C
		public X509Store Untrusted
		{
			get
			{
				if (this._untrusted == null)
				{
					string path = Path.Combine(this._storePath, "Disallowed");
					this._untrusted = new X509Store(path, false);
				}
				return this._untrusted;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000367A8 File Offset: 0x000349A8
		public void Clear()
		{
			if (this._personal != null)
			{
				this._personal.Clear();
			}
			this._personal = null;
			if (this._other != null)
			{
				this._other.Clear();
			}
			this._other = null;
			if (this._intermediate != null)
			{
				this._intermediate.Clear();
			}
			this._intermediate = null;
			if (this._trusted != null)
			{
				this._trusted.Clear();
			}
			this._trusted = null;
			if (this._untrusted != null)
			{
				this._untrusted.Clear();
			}
			this._untrusted = null;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00036848 File Offset: 0x00034A48
		public X509Store Open(string storeName, bool create)
		{
			if (storeName == null)
			{
				throw new ArgumentNullException("storeName");
			}
			string path = Path.Combine(this._storePath, storeName);
			if (!create && !Directory.Exists(path))
			{
				return null;
			}
			return new X509Store(path, true);
		}

		// Token: 0x04000327 RID: 807
		private string _storePath;

		// Token: 0x04000328 RID: 808
		private X509Store _personal;

		// Token: 0x04000329 RID: 809
		private X509Store _other;

		// Token: 0x0400032A RID: 810
		private X509Store _intermediate;

		// Token: 0x0400032B RID: 811
		private X509Store _trusted;

		// Token: 0x0400032C RID: 812
		private X509Store _untrusted;

		// Token: 0x020000D3 RID: 211
		public class Names
		{
			// Token: 0x0400032D RID: 813
			public const string Personal = "My";

			// Token: 0x0400032E RID: 814
			public const string OtherPeople = "AddressBook";

			// Token: 0x0400032F RID: 815
			public const string IntermediateCA = "CA";

			// Token: 0x04000330 RID: 816
			public const string TrustedRoot = "Trust";

			// Token: 0x04000331 RID: 817
			public const string Untrusted = "Disallowed";
		}
	}
}
