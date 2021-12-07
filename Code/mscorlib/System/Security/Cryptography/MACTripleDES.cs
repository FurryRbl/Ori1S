using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Message Authentication Code (MAC) using <see cref="T:System.Security.Cryptography.TripleDES" /> for the input data <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
	// Token: 0x020005BD RID: 1469
	[ComVisible(true)]
	public class MACTripleDES : KeyedHashAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MACTripleDES" /> class.</summary>
		// Token: 0x06003850 RID: 14416 RVA: 0x000B6AA0 File Offset: 0x000B4CA0
		public MACTripleDES()
		{
			this.Setup("TripleDES", null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MACTripleDES" /> class with the specified key data.</summary>
		/// <param name="rgbKey">The secret key for <see cref="T:System.Security.Cryptography.MACTripleDES" /> encryption. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbKey" /> parameter is null. </exception>
		// Token: 0x06003851 RID: 14417 RVA: 0x000B6AB4 File Offset: 0x000B4CB4
		public MACTripleDES(byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("rgbKey");
			}
			this.Setup("TripleDES", rgbKey);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MACTripleDES" /> class with the specified key data using the specified implementation of <see cref="T:System.Security.Cryptography.TripleDES" />.</summary>
		/// <param name="strTripleDES">The name of the <see cref="T:System.Security.Cryptography.TripleDES" /> implementation to use. </param>
		/// <param name="rgbKey">The secret key for <see cref="T:System.Security.Cryptography.MACTripleDES" /> encryption. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbKey" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strTripleDES" /> parameter is not a valid name of a <see cref="T:System.Security.Cryptography.TripleDES" /> implementation. </exception>
		// Token: 0x06003852 RID: 14418 RVA: 0x000B6ADC File Offset: 0x000B4CDC
		public MACTripleDES(string strTripleDES, byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("rgbKey");
			}
			if (strTripleDES == null)
			{
				this.Setup("TripleDES", rgbKey);
			}
			else
			{
				this.Setup(strTripleDES, rgbKey);
			}
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000B6B20 File Offset: 0x000B4D20
		private void Setup(string strTripleDES, byte[] rgbKey)
		{
			this.tdes = TripleDES.Create(strTripleDES);
			this.tdes.Padding = PaddingMode.Zeros;
			if (rgbKey != null)
			{
				this.tdes.Key = rgbKey;
			}
			this.HashSizeValue = this.tdes.BlockSize;
			this.Key = this.tdes.Key;
			this.mac = new MACAlgorithm(this.tdes);
			this.m_disposed = false;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.MACTripleDES" />.</summary>
		// Token: 0x06003854 RID: 14420 RVA: 0x000B6B94 File Offset: 0x000B4D94
		~MACTripleDES()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the padding mode used in the hashing algorithm.</summary>
		/// <returns>The padding mode used in the hashing algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The property cannot be set because the padding mode is invalid.</exception>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000B6BD0 File Offset: 0x000B4DD0
		// (set) Token: 0x06003856 RID: 14422 RVA: 0x000B6BE0 File Offset: 0x000B4DE0
		[ComVisible(false)]
		public PaddingMode Padding
		{
			get
			{
				return this.tdes.Padding;
			}
			set
			{
				this.tdes.Padding = value;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.MACTripleDES" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003857 RID: 14423 RVA: 0x000B6BF0 File Offset: 0x000B4DF0
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
				}
				if (this.tdes != null)
				{
					this.tdes.Clear();
				}
				if (disposing)
				{
					this.KeyValue = null;
					this.tdes = null;
				}
				base.Dispose(disposing);
				this.m_disposed = true;
			}
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.MACTripleDES" />.</summary>
		// Token: 0x06003858 RID: 14424 RVA: 0x000B6C60 File Offset: 0x000B4E60
		public override void Initialize()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("MACTripleDES");
			}
			this.State = 0;
			this.mac.Initialize(this.KeyValue);
		}

		/// <summary>Routes data written to the object into the <see cref="T:System.Security.Cryptography.TripleDES" /> encryptor for computing the Message Authentication Code (MAC).</summary>
		/// <param name="rgbData">The input data. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the array to use as data. </param>
		// Token: 0x06003859 RID: 14425 RVA: 0x000B6C9C File Offset: 0x000B4E9C
		protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("MACTripleDES");
			}
			if (this.State == 0)
			{
				this.Initialize();
				this.State = 1;
			}
			this.mac.Core(rgbData, ibStart, cbSize);
		}

		/// <summary>Returns the computed Message Authentication Code (MAC) after all data is written to the object.</summary>
		/// <returns>The computed MAC.</returns>
		// Token: 0x0600385A RID: 14426 RVA: 0x000B6CE8 File Offset: 0x000B4EE8
		protected override byte[] HashFinal()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("MACTripleDES");
			}
			this.State = 0;
			return this.mac.Final();
		}

		// Token: 0x0400186C RID: 6252
		private TripleDES tdes;

		// Token: 0x0400186D RID: 6253
		private MACAlgorithm mac;

		// Token: 0x0400186E RID: 6254
		private bool m_disposed;
	}
}
