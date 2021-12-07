using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of Hash-based Message Authentication Code (HMAC) must derive.</summary>
	// Token: 0x020005B1 RID: 1457
	[ComVisible(true)]
	public abstract class HMAC : KeyedHashAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMAC" /> class. </summary>
		// Token: 0x06003817 RID: 14359 RVA: 0x000B63A0 File Offset: 0x000B45A0
		protected HMAC()
		{
			this._disposed = false;
			this._blockSizeValue = 64;
		}

		/// <summary>Gets or sets the block size to use in the hash value.</summary>
		/// <returns>The block size to use in the hash value.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000B63B8 File Offset: 0x000B45B8
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x000B63C0 File Offset: 0x000B45C0
		protected int BlockSizeValue
		{
			get
			{
				return this._blockSizeValue;
			}
			set
			{
				this._blockSizeValue = value;
			}
		}

		/// <summary>Gets or sets the name of the hash algorithm to use for hashing.</summary>
		/// <returns>The name of the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The current hash algorithm cannot be changed.</exception>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000B63CC File Offset: 0x000B45CC
		// (set) Token: 0x0600381B RID: 14363 RVA: 0x000B63D4 File Offset: 0x000B45D4
		public string HashName
		{
			get
			{
				return this._hashName;
			}
			set
			{
				this._hashName = value;
				this._algo = HashAlgorithm.Create(this._hashName);
			}
		}

		/// <summary>Gets or sets the key to use in the hash algorithm.</summary>
		/// <returns>The key to use in the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt is made to change the <see cref="P:System.Security.Cryptography.HMAC.Key" /> property after hashing has begun. </exception>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600381C RID: 14364 RVA: 0x000B63F0 File Offset: 0x000B45F0
		// (set) Token: 0x0600381D RID: 14365 RVA: 0x000B6404 File Offset: 0x000B4604
		public override byte[] Key
		{
			get
			{
				return (byte[])base.Key.Clone();
			}
			set
			{
				if (value != null && value.Length > 64)
				{
					base.Key = this._algo.ComputeHash(value);
				}
				else
				{
					base.Key = (byte[])value.Clone();
				}
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x000B644C File Offset: 0x000B464C
		internal BlockProcessor Block
		{
			get
			{
				if (this._block == null)
				{
					this._block = new BlockProcessor(this._algo, this.BlockSizeValue >> 3);
				}
				return this._block;
			}
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000B6484 File Offset: 0x000B4684
		private byte[] KeySetup(byte[] key, byte padding)
		{
			byte[] array = new byte[this.BlockSizeValue];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = (key[i] ^ padding);
			}
			for (int j = key.Length; j < this.BlockSizeValue; j++)
			{
				array[j] = padding;
			}
			return array;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HMAC" /> class when a key change is legitimate and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003820 RID: 14368 RVA: 0x000B64D8 File Offset: 0x000B46D8
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the default <see cref="T:System.Security.Cryptography.HMAC" /> hash algorithm for computing the hash value.</summary>
		/// <param name="rgb">The input data. </param>
		/// <param name="ib">The offset into the byte array from which to begin using data. </param>
		/// <param name="cb">The number of bytes in the array to use as data. </param>
		// Token: 0x06003821 RID: 14369 RVA: 0x000B64EC File Offset: 0x000B46EC
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("HMACSHA1");
			}
			if (this.State == 0)
			{
				this.Initialize();
				this.State = 1;
			}
			this.Block.Core(rgb, ib, cb);
		}

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code in a byte array.</returns>
		// Token: 0x06003822 RID: 14370 RVA: 0x000B6538 File Offset: 0x000B4738
		protected override byte[] HashFinal()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("HMAC");
			}
			this.State = 0;
			this.Block.Final();
			byte[] hash = this._algo.Hash;
			byte[] array = this.KeySetup(this.Key, 92);
			this._algo.Initialize();
			this._algo.TransformBlock(array, 0, array.Length, array, 0);
			this._algo.TransformFinalBlock(hash, 0, hash.Length);
			byte[] hash2 = this._algo.Hash;
			this._algo.Initialize();
			Array.Clear(array, 0, array.Length);
			Array.Clear(hash, 0, hash.Length);
			return hash2;
		}

		/// <summary>Initializes an instance of the default implementation of <see cref="T:System.Security.Cryptography.HMAC" />.</summary>
		// Token: 0x06003823 RID: 14371 RVA: 0x000B65E4 File Offset: 0x000B47E4
		public override void Initialize()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("HMAC");
			}
			this.State = 0;
			this.Block.Initialize();
			byte[] array = this.KeySetup(this.Key, 54);
			this._algo.Initialize();
			this.Block.Core(array);
			Array.Clear(array, 0, array.Length);
		}

		/// <summary>Creates an instance of the default implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <returns>A new SHA-1 instance, unless the default settings have been changed by using the &lt;cryptoClass&gt; element.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003824 RID: 14372 RVA: 0x000B664C File Offset: 0x000B484C
		public new static HMAC Create()
		{
			return HMAC.Create("System.Security.Cryptography.HMAC");
		}

		/// <summary>Creates an instance of the specified implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <returns>A new instance of the specified HMAC implementation.</returns>
		/// <param name="algorithmName">The HMAC implementation to use. The following table shows the valid values for the <paramref name="algorithmName" /> parameter and the algorithms they map to.Parameter valueImplements System.Security.Cryptography.HMAC<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.KeyedHashAlgorithm<see cref="T:System.Security.Cryptography.HMACSHA1" />HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />System.Security.Cryptography.HMACMD5<see cref="T:System.Security.Cryptography.HMACMD5" />HMACRIPEMD160<see cref="T:System.Security.Cryptography.HMACRIPEMD160" />System.Security.Cryptography.HMACRIPEMD160<see cref="T:System.Security.Cryptography. HMACRIPEMD160" />HMACSHA1<see cref="T:System.Security.Cryptography.HMACSHA1" />System.Security.Cryptography.HMACSHA1<see cref="T:System.Security.Cryptography. HMACSHA1" />HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />System.Security.Cryptography.HMACSHA256<see cref="T:System.Security.Cryptography.HMACSHA256" />HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />System.Security.Cryptography.HMACSHA384<see cref="T:System.Security.Cryptography.HMACSHA384" />HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />System.Security.Cryptography.HMACSHA512<see cref="T:System.Security.Cryptography.HMACSHA512" />MACTripleDES<see cref="T:System.Security.Cryptography. MACTripleDES" />System.Security.Cryptography.MACTripleDES<see cref="T:System.Security.Cryptography.MACTripleDES" /></param>
		// Token: 0x06003825 RID: 14373 RVA: 0x000B6658 File Offset: 0x000B4858
		public new static HMAC Create(string algorithmName)
		{
			return (HMAC)CryptoConfig.CreateFromName(algorithmName);
		}

		// Token: 0x0400185C RID: 6236
		private bool _disposed;

		// Token: 0x0400185D RID: 6237
		private string _hashName;

		// Token: 0x0400185E RID: 6238
		private HashAlgorithm _algo;

		// Token: 0x0400185F RID: 6239
		private BlockProcessor _block;

		// Token: 0x04001860 RID: 6240
		private int _blockSizeValue;
	}
}
