using System;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x0200004F RID: 79
	public class X509Stores
	{
		// Token: 0x0600039F RID: 927 RVA: 0x000186A8 File Offset: 0x000168A8
		internal X509Stores(string path)
		{
			this._storePath = path;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000186B8 File Offset: 0x000168B8
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000186F4 File Offset: 0x000168F4
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00018730 File Offset: 0x00016930
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001876C File Offset: 0x0001696C
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000187A8 File Offset: 0x000169A8
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

		// Token: 0x060003A5 RID: 933 RVA: 0x000187E4 File Offset: 0x000169E4
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

		// Token: 0x060003A6 RID: 934 RVA: 0x00018884 File Offset: 0x00016A84
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

		// Token: 0x040001AA RID: 426
		private string _storePath;

		// Token: 0x040001AB RID: 427
		private X509Store _personal;

		// Token: 0x040001AC RID: 428
		private X509Store _other;

		// Token: 0x040001AD RID: 429
		private X509Store _intermediate;

		// Token: 0x040001AE RID: 430
		private X509Store _trusted;

		// Token: 0x040001AF RID: 431
		private X509Store _untrusted;

		// Token: 0x02000050 RID: 80
		public class Names
		{
			// Token: 0x040001B0 RID: 432
			public const string Personal = "My";

			// Token: 0x040001B1 RID: 433
			public const string OtherPeople = "AddressBook";

			// Token: 0x040001B2 RID: 434
			public const string IntermediateCA = "CA";

			// Token: 0x040001B3 RID: 435
			public const string TrustedRoot = "Trust";

			// Token: 0x040001B4 RID: 436
			public const string Untrusted = "Disallowed";
		}
	}
}
