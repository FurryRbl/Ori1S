using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 store, which is a physical store where certificates are persisted and managed. This class cannot be inherited.</summary>
	// Token: 0x02000457 RID: 1111
	public sealed class X509Store
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the personal certificates of the current user store.</summary>
		// Token: 0x060027DD RID: 10205 RVA: 0x0007DCE4 File Offset: 0x0007BEE4
		public X509Store() : this("MY", StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified store name.</summary>
		/// <param name="storeName">A string value representing the store name. See <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" />  for more information. </param>
		// Token: 0x060027DE RID: 10206 RVA: 0x0007DCF4 File Offset: 0x0007BEF4
		public X509Store(string storeName) : this(storeName, StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> value.</summary>
		/// <param name="storeName">One of the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> values. </param>
		// Token: 0x060027DF RID: 10207 RVA: 0x0007DD00 File Offset: 0x0007BF00
		public X509Store(StoreName storeName) : this(storeName, StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> value.</summary>
		/// <param name="storeLocation">One of the <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values. </param>
		// Token: 0x060027E0 RID: 10208 RVA: 0x0007DD0C File Offset: 0x0007BF0C
		public X509Store(StoreLocation storeLocation) : this("MY", storeLocation)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> and <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values.</summary>
		/// <param name="storeName">One of the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> values. </param>
		/// <param name="storeLocation">One of the <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="storeLocation" /> is not a valid location or <paramref name="storeName" /> is not a valid name. </exception>
		// Token: 0x060027E1 RID: 10209 RVA: 0x0007DD1C File Offset: 0x0007BF1C
		public X509Store(StoreName storeName, StoreLocation storeLocation)
		{
			if (storeName < StoreName.AddressBook || storeName > StoreName.TrustedPublisher)
			{
				throw new ArgumentException("storeName");
			}
			if (storeLocation < StoreLocation.CurrentUser || storeLocation > StoreLocation.LocalMachine)
			{
				throw new ArgumentException("storeLocation");
			}
			if (storeName != StoreName.CertificateAuthority)
			{
				this._name = storeName.ToString();
			}
			else
			{
				this._name = "CA";
			}
			this._location = storeLocation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using an Intptr handle to an HCERTSTORE store.</summary>
		/// <param name="storeHandle">An <see cref="T:System.IntPtr" /> handle to an HCERTSTORE store.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="storeHandle" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="storeHandle" /> parameter points to an invalid context.</exception>
		// Token: 0x060027E2 RID: 10210 RVA: 0x0007DD9C File Offset: 0x0007BF9C
		[MonoTODO("Mono's stores are fully managed. All handles are invalid.")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public X509Store(IntPtr storeHandle)
		{
			if (storeHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("storeHandle");
			}
			throw new CryptographicException("Invalid handle.");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using a string representing a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> enumeration and a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> enumeration.</summary>
		/// <param name="storeName">A string representing a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> enumeration. </param>
		/// <param name="storeLocation">One of the <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="storeLocation" /> contains invalid values. </exception>
		// Token: 0x060027E3 RID: 10211 RVA: 0x0007DDCC File Offset: 0x0007BFCC
		public X509Store(string storeName, StoreLocation storeLocation)
		{
			if (storeLocation < StoreLocation.CurrentUser || storeLocation > StoreLocation.LocalMachine)
			{
				throw new ArgumentException("storeLocation");
			}
			this._name = storeName;
			this._location = storeLocation;
		}

		/// <summary>Returns a collection of certificates located in an X.509 certificate store.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x0007DDFC File Offset: 0x0007BFFC
		public X509Certificate2Collection Certificates
		{
			get
			{
				if (this.list == null)
				{
					this.list = new X509Certificate2Collection();
				}
				else if (this.store == null)
				{
					this.list.Clear();
				}
				return this.list;
			}
		}

		/// <summary>Gets the location of the X.509 certificate store.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x0007DE38 File Offset: 0x0007C038
		public StoreLocation Location
		{
			get
			{
				return this._location;
			}
		}

		/// <summary>Gets the name of the X.509 certificate store.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> values.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x0007DE40 File Offset: 0x0007C040
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x0007DE48 File Offset: 0x0007C048
		private X509Stores Factory
		{
			get
			{
				if (this._location == StoreLocation.CurrentUser)
				{
					return X509StoreManager.CurrentUser;
				}
				return X509StoreManager.LocalMachine;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x0007DE64 File Offset: 0x0007C064
		private bool IsOpen
		{
			get
			{
				return this.store != null;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x0007DE74 File Offset: 0x0007C074
		private bool IsReadOnly
		{
			get
			{
				return Environment.UnityWebSecurityEnabled || (this._flags & OpenFlags.ReadWrite) == OpenFlags.ReadOnly;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x0007DE90 File Offset: 0x0007C090
		internal X509Store Store
		{
			get
			{
				return this.store;
			}
		}

		/// <summary>Gets an <see cref="T:System.IntPtr" /> handle to an HCERTSTORE store.  </summary>
		/// <returns>An <see cref="T:System.IntPtr" /> handle to an HCERTSTORE store.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x0007DE98 File Offset: 0x0007C098
		[MonoTODO("Mono's stores are fully managed. Always returns IntPtr.Zero.")]
		public IntPtr StoreHandle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		/// <summary>Adds a certificate to an X.509 certificate store.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate could not be added to the store.</exception>
		// Token: 0x060027EC RID: 10220 RVA: 0x0007DEA0 File Offset: 0x0007C0A0
		public void Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(Locale.GetText("Store isn't opened."));
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(Locale.GetText("Store is read-only."));
			}
			if (!this.Exists(certificate))
			{
				try
				{
					this.store.Import(new X509Certificate(certificate.RawData));
				}
				finally
				{
					this.Certificates.Add(certificate);
				}
			}
		}

		/// <summary>Adds a collection of certificates to an X.509 certificate store.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060027ED RID: 10221 RVA: 0x0007DF48 File Offset: 0x0007C148
		[MonoTODO("Method isn't transactional (like documented)")]
		public void AddRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (certificates.Count == 0)
			{
				return;
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(Locale.GetText("Store isn't opened."));
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(Locale.GetText("Store is read-only."));
			}
			foreach (X509Certificate2 x509Certificate in certificates)
			{
				if (!this.Exists(x509Certificate))
				{
					try
					{
						this.store.Import(new X509Certificate(x509Certificate.RawData));
					}
					finally
					{
						this.Certificates.Add(x509Certificate);
					}
				}
			}
		}

		/// <summary>Closes an X.509 certificate store.</summary>
		// Token: 0x060027EE RID: 10222 RVA: 0x0007E018 File Offset: 0x0007C218
		public void Close()
		{
			this.store = null;
			if (this.list != null)
			{
				this.list.Clear();
			}
		}

		/// <summary>Opens an X.509 certificate store or creates a new store, depending on <see cref="T:System.Security.Cryptography.X509Certificates.OpenFlags" /> flag settings.</summary>
		/// <param name="flags">One the <see cref="T:System.Security.Cryptography.X509Certificates.OpenFlags" /> values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The store is unreadable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The store contains invalid values.</exception>
		// Token: 0x060027EF RID: 10223 RVA: 0x0007E038 File Offset: 0x0007C238
		public void Open(OpenFlags flags)
		{
			if (string.IsNullOrEmpty(this._name))
			{
				throw new CryptographicException(Locale.GetText("Invalid store name (null or empty)."));
			}
			string name = this._name;
			string storeName;
			if (name != null)
			{
				if (X509Store.<>f__switch$map1B == null)
				{
					X509Store.<>f__switch$map1B = new Dictionary<string, int>(1)
					{
						{
							"Root",
							0
						}
					};
				}
				int num;
				if (X509Store.<>f__switch$map1B.TryGetValue(name, out num))
				{
					if (num == 0)
					{
						storeName = "Trust";
						goto IL_8B;
					}
				}
			}
			storeName = this._name;
			IL_8B:
			bool create = (flags & OpenFlags.OpenExistingOnly) != OpenFlags.OpenExistingOnly;
			this.store = this.Factory.Open(storeName, create);
			if (this.store == null)
			{
				throw new CryptographicException(Locale.GetText("Store {0} doesn't exists.", new object[]
				{
					this._name
				}));
			}
			this._flags = flags;
			foreach (X509Certificate x509Certificate in this.store.Certificates)
			{
				this.Certificates.Add(new X509Certificate2(x509Certificate.RawData));
			}
		}

		/// <summary>Removes a certificate from an X.509 certificate store.</summary>
		/// <param name="certificate">The certificate to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060027F0 RID: 10224 RVA: 0x0007E194 File Offset: 0x0007C394
		public void Remove(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(Locale.GetText("Store isn't opened."));
			}
			if (!this.Exists(certificate))
			{
				return;
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(Locale.GetText("Store is read-only."));
			}
			try
			{
				this.store.Remove(new X509Certificate(certificate.RawData));
			}
			finally
			{
				this.Certificates.Remove(certificate);
			}
		}

		/// <summary>Removes a range of certificates from an X.509 certificate store.</summary>
		/// <param name="certificates">A range of certificates to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060027F1 RID: 10225 RVA: 0x0007E23C File Offset: 0x0007C43C
		[MonoTODO("Method isn't transactional (like documented)")]
		public void RemoveRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (certificates.Count == 0)
			{
				return;
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(Locale.GetText("Store isn't opened."));
			}
			bool flag = false;
			foreach (X509Certificate2 certificate in certificates)
			{
				if (this.Exists(certificate))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(Locale.GetText("Store is read-only."));
			}
			try
			{
				foreach (X509Certificate2 x509Certificate in certificates)
				{
					this.store.Remove(new X509Certificate(x509Certificate.RawData));
				}
			}
			finally
			{
				this.Certificates.RemoveRange(certificates);
			}
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x0007E338 File Offset: 0x0007C538
		private bool Exists(X509Certificate2 certificate)
		{
			if (this.store == null || this.list == null || certificate == null)
			{
				return false;
			}
			foreach (X509Certificate2 other in this.list)
			{
				if (certificate.Equals(other))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400189B RID: 6299
		private string _name;

		// Token: 0x0400189C RID: 6300
		private StoreLocation _location;

		// Token: 0x0400189D RID: 6301
		private X509Certificate2Collection list;

		// Token: 0x0400189E RID: 6302
		private OpenFlags _flags;

		// Token: 0x0400189F RID: 6303
		private X509Store store;
	}
}
