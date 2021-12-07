using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of symmetric algorithms must inherit.</summary>
	// Token: 0x020005E7 RID: 1511
	[ComVisible(true)]
	public abstract class SymmetricAlgorithm : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The implementation of the class derived from the symmetric algorithm is not valid.</exception>
		// Token: 0x06003997 RID: 14743 RVA: 0x000C5A2C File Offset: 0x000C3C2C
		protected SymmetricAlgorithm()
		{
			this.ModeValue = CipherMode.CBC;
			this.PaddingValue = PaddingMode.PKCS7;
			this.m_disposed = false;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> and optionally releases the managed resources. </summary>
		// Token: 0x06003998 RID: 14744 RVA: 0x000C5A4C File Offset: 0x000C3C4C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000C5A5C File Offset: 0x000C3C5C
		~SymmetricAlgorithm()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		// Token: 0x0600399A RID: 14746 RVA: 0x000C5A98 File Offset: 0x000C3C98
		public void Clear()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600399B RID: 14747 RVA: 0x000C5AA4 File Offset: 0x000C3CA4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
					this.KeyValue = null;
				}
				if (disposing)
				{
				}
				this.m_disposed = true;
			}
		}

		/// <summary>Gets or sets the block size, in bits, of the cryptographic operation.</summary>
		/// <returns>The block size, in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The block size is invalid. </exception>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000C5AF0 File Offset: 0x000C3CF0
		// (set) Token: 0x0600399D RID: 14749 RVA: 0x000C5AF8 File Offset: 0x000C3CF8
		public virtual int BlockSize
		{
			get
			{
				return this.BlockSizeValue;
			}
			set
			{
				if (!KeySizes.IsLegalKeySize(this.LegalBlockSizesValue, value))
				{
					throw new CryptographicException(Locale.GetText("block size not supported by algorithm"));
				}
				if (this.BlockSizeValue != value)
				{
					this.BlockSizeValue = value;
					this.IVValue = null;
				}
			}
		}

		/// <summary>Gets or sets the feedback size, in bits, of the cryptographic operation.</summary>
		/// <returns>The feedback size in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The feedback size is larger than the block size. </exception>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000C5B38 File Offset: 0x000C3D38
		// (set) Token: 0x0600399F RID: 14751 RVA: 0x000C5B40 File Offset: 0x000C3D40
		public virtual int FeedbackSize
		{
			get
			{
				return this.FeedbackSizeValue;
			}
			set
			{
				if (value <= 0 || value > this.BlockSizeValue)
				{
					throw new CryptographicException(Locale.GetText("feedback size larger than block size"));
				}
				this.FeedbackSizeValue = value;
			}
		}

		/// <summary>Gets or sets the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		/// <returns>The initialization vector.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the initialization vector to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to set the initialization vector to an invalid size. </exception>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000C5B78 File Offset: 0x000C3D78
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x000C5B9C File Offset: 0x000C3D9C
		public virtual byte[] IV
		{
			get
			{
				if (this.IVValue == null)
				{
					this.GenerateIV();
				}
				return (byte[])this.IVValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("IV");
				}
				if (value.Length << 3 != this.BlockSizeValue)
				{
					throw new CryptographicException(Locale.GetText("IV length is different than block size"));
				}
				this.IVValue = (byte[])value.Clone();
			}
		}

		/// <summary>Gets or sets the secret key for the symmetric algorithm.</summary>
		/// <returns>The secret key to use for the symmetric algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the key to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is invalid.</exception>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x000C5BEC File Offset: 0x000C3DEC
		// (set) Token: 0x060039A3 RID: 14755 RVA: 0x000C5C10 File Offset: 0x000C3E10
		public virtual byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Key");
				}
				int num = value.Length << 3;
				if (!KeySizes.IsLegalKeySize(this.LegalKeySizesValue, num))
				{
					throw new CryptographicException(Locale.GetText("Key size not supported by algorithm"));
				}
				this.KeySizeValue = num;
				this.KeyValue = (byte[])value.Clone();
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		/// <returns>The size, in bits, of the secret key used by the symmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is not valid. </exception>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000C5C70 File Offset: 0x000C3E70
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000C5C78 File Offset: 0x000C3E78
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (!KeySizes.IsLegalKeySize(this.LegalKeySizesValue, value))
				{
					throw new CryptographicException(Locale.GetText("Key size not supported by algorithm"));
				}
				this.KeySizeValue = value;
				this.KeyValue = null;
			}
		}

		/// <summary>Gets the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the block sizes supported by the algorithm.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000C5CAC File Offset: 0x000C3EAC
		public virtual KeySizes[] LegalBlockSizes
		{
			get
			{
				return this.LegalBlockSizesValue;
			}
		}

		/// <summary>Gets the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the key sizes supported by the algorithm.</returns>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000C5CB4 File Offset: 0x000C3EB4
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return this.LegalKeySizesValue;
			}
		}

		/// <summary>Gets or sets the mode for operation of the symmetric algorithm.</summary>
		/// <returns>The mode for operation of the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cipher mode is not one of the <see cref="T:System.Security.Cryptography.CipherMode" /> values. </exception>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000C5CBC File Offset: 0x000C3EBC
		// (set) Token: 0x060039A9 RID: 14761 RVA: 0x000C5CC4 File Offset: 0x000C3EC4
		public virtual CipherMode Mode
		{
			get
			{
				return this.ModeValue;
			}
			set
			{
				if (!Enum.IsDefined(this.ModeValue.GetType(), value))
				{
					throw new CryptographicException(Locale.GetText("Cipher mode not available"));
				}
				this.ModeValue = value;
			}
		}

		/// <summary>Gets or sets the padding mode used in the symmetric algorithm.</summary>
		/// <returns>The padding mode used in the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not one of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values. </exception>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000C5D00 File Offset: 0x000C3F00
		// (set) Token: 0x060039AB RID: 14763 RVA: 0x000C5D08 File Offset: 0x000C3F08
		public virtual PaddingMode Padding
		{
			get
			{
				return this.PaddingValue;
			}
			set
			{
				if (!Enum.IsDefined(this.PaddingValue.GetType(), value))
				{
					throw new CryptographicException(Locale.GetText("Padding mode not available"));
				}
				this.PaddingValue = value;
			}
		}

		/// <summary>Creates a symmetric decryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x060039AC RID: 14764 RVA: 0x000C5D44 File Offset: 0x000C3F44
		public virtual ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric decryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		// Token: 0x060039AD RID: 14765
		public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>Creates a symmetric encryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x060039AE RID: 14766 RVA: 0x000C5D58 File Offset: 0x000C3F58
		public virtual ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric encryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		// Token: 0x060039AF RID: 14767
		public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>When overridden in a derived class, generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x060039B0 RID: 14768
		public abstract void GenerateIV();

		/// <summary>When overridden in a derived class, generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to use for the algorithm.</summary>
		// Token: 0x060039B1 RID: 14769
		public abstract void GenerateKey();

		/// <summary>Determines whether the specified key size is valid for the current algorithm.</summary>
		/// <returns>true if the specified key size is valid for the current algorithm; otherwise, false.</returns>
		/// <param name="bitLength">The length, in bits, to check for a valid key size. </param>
		// Token: 0x060039B2 RID: 14770 RVA: 0x000C5D6C File Offset: 0x000C3F6C
		public bool ValidKeySize(int bitLength)
		{
			return KeySizes.IsLegalKeySize(this.LegalKeySizesValue, bitLength);
		}

		/// <summary>Creates a default cryptographic object used to perform the symmetric algorithm.</summary>
		/// <returns>A default cryptographic object used to perform the symmetric algorithm.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060039B3 RID: 14771 RVA: 0x000C5D7C File Offset: 0x000C3F7C
		public static SymmetricAlgorithm Create()
		{
			return SymmetricAlgorithm.Create("System.Security.Cryptography.SymmetricAlgorithm");
		}

		/// <summary>Creates the specified cryptographic object used to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the symmetric algorithm.</returns>
		/// <param name="algName">The name of the specific implementation of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class to use. </param>
		// Token: 0x060039B4 RID: 14772 RVA: 0x000C5D88 File Offset: 0x000C3F88
		public static SymmetricAlgorithm Create(string algName)
		{
			return (SymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>Represents the block size, in bits, of the cryptographic operation.</summary>
		// Token: 0x04001900 RID: 6400
		protected int BlockSizeValue;

		/// <summary>Represents the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		// Token: 0x04001901 RID: 6401
		protected byte[] IVValue;

		/// <summary>Represents the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		// Token: 0x04001902 RID: 6402
		protected int KeySizeValue;

		/// <summary>Represents the secret key for the symmetric algorithm.</summary>
		// Token: 0x04001903 RID: 6403
		protected byte[] KeyValue;

		/// <summary>Specifies the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x04001904 RID: 6404
		protected KeySizes[] LegalBlockSizesValue;

		/// <summary>Specifies the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x04001905 RID: 6405
		protected KeySizes[] LegalKeySizesValue;

		/// <summary>Represents the feedback size, in bits, of the cryptographic operation.</summary>
		// Token: 0x04001906 RID: 6406
		protected int FeedbackSizeValue;

		/// <summary>Represents the cipher mode used in the symmetric algorithm.</summary>
		// Token: 0x04001907 RID: 6407
		protected CipherMode ModeValue;

		/// <summary>Represents the padding mode used in the symmetric algorithm.</summary>
		// Token: 0x04001908 RID: 6408
		protected PaddingMode PaddingValue;

		// Token: 0x04001909 RID: 6409
		private bool m_disposed;
	}
}
