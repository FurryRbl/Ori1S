using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of keyed hash algorithms must derive. </summary>
	// Token: 0x020005BA RID: 1466
	[ComVisible(true)]
	public abstract class KeyedHashAlgorithm : HashAlgorithm
	{
		// Token: 0x06003843 RID: 14403 RVA: 0x000B68FC File Offset: 0x000B4AFC
		~KeyedHashAlgorithm()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the key to use in the hash algorithm.</summary>
		/// <returns>The key to use in the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to change the <see cref="P:System.Security.Cryptography.KeyedHashAlgorithm.Key" /> property after hashing has begun. </exception>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000B6938 File Offset: 0x000B4B38
		// (set) Token: 0x06003845 RID: 14405 RVA: 0x000B694C File Offset: 0x000B4B4C
		public virtual byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.State != 0)
				{
					throw new CryptographicException(Locale.GetText("Key can't be changed at this state."));
				}
				this.ZeroizeKey();
				this.KeyValue = (byte[])value.Clone();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.KeyedHashAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003846 RID: 14406 RVA: 0x000B698C File Offset: 0x000B4B8C
		protected override void Dispose(bool disposing)
		{
			this.ZeroizeKey();
			base.Dispose(disposing);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000B699C File Offset: 0x000B4B9C
		private void ZeroizeKey()
		{
			if (this.KeyValue != null)
			{
				Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
			}
		}

		/// <summary>Creates an instance of the default implementation of a keyed hash algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.HMACSHA1" /> instance, unless the default settings have been changed.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003848 RID: 14408 RVA: 0x000B69C0 File Offset: 0x000B4BC0
		public new static KeyedHashAlgorithm Create()
		{
			return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
		}

		/// <summary>Creates an instance of the specified implementation of a keyed hash algorithm.</summary>
		/// <returns>A new instance of the specified keyed hash algorithm.</returns>
		/// <param name="algName">The keyed hash algorithm implementation to use. The following table shows the valid values for the <paramref name="algName" /> parameter and the algorithms they map to.Parameter valueImplements System.Security.Cryptography.HMAC<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.KeyedHashAlgorithm<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />System.Security.Cryptography.HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />System.Security.Cryptography.HMACRIPEMD160<see cref="T:System.Security.Cryptography. HMACRIPEMD160" />HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.HMACSHA1<see cref="T:System.Security.Cryptography. HMACSHA1" />HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />System.Security.Cryptography.HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />System.Security.Cryptography.HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />System.Security.Cryptography.HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />MACTripleDES<see cref="T:System.Security.Cryptography. MACTripleDES" />System.Security.Cryptography.MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" /></param>
		// Token: 0x06003849 RID: 14409 RVA: 0x000B69CC File Offset: 0x000B4BCC
		public new static KeyedHashAlgorithm Create(string algName)
		{
			return (KeyedHashAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>The key to use in the hash algorithm.</summary>
		// Token: 0x04001865 RID: 6245
		protected byte[] KeyValue;
	}
}
