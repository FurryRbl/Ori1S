using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class for Triple Data Encryption Standard algorithms from which all <see cref="T:System.Security.Cryptography.TripleDES" /> implementations must derive.</summary>
	// Token: 0x020005E9 RID: 1513
	[ComVisible(true)]
	public abstract class TripleDES : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDES" /> class.</summary>
		// Token: 0x060039C2 RID: 14786 RVA: 0x000C6124 File Offset: 0x000C4324
		protected TripleDES()
		{
			this.KeySizeValue = 192;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = 8;
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(128, 192, 64);
			this.LegalBlockSizesValue = new KeySizes[1];
			this.LegalBlockSizesValue[0] = new KeySizes(64, 64, 0);
		}

		/// <summary>Gets or sets the secret key for the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>The secret key for the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the key to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to set a key whose length is invalid.-or- An attempt was made to set a weak key (see <see cref="M:System.Security.Cryptography.TripleDES.IsWeakKey(System.Byte[])" />). </exception>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000C6194 File Offset: 0x000C4394
		// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000C61E0 File Offset: 0x000C43E0
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
					while (TripleDES.IsWeakKey(this.KeyValue))
					{
						this.GenerateKey();
					}
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Key");
				}
				if (TripleDES.IsWeakKey(value))
				{
					throw new CryptographicException(Locale.GetText("Weak Key"));
				}
				this.KeyValue = (byte[])value.Clone();
			}
		}

		/// <summary>Determines whether the specified key is weak.</summary>
		/// <returns>true if the key is weak; otherwise, false.</returns>
		/// <param name="rgbKey">The secret key to test for weakness. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The size of the <paramref name="rgbKey" /> parameter is not valid. </exception>
		// Token: 0x060039C5 RID: 14789 RVA: 0x000C6220 File Offset: 0x000C4420
		public static bool IsWeakKey(byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new CryptographicException(Locale.GetText("Null Key"));
			}
			if (rgbKey.Length == 16)
			{
				for (int i = 0; i < 8; i++)
				{
					if (rgbKey[i] != rgbKey[i + 8])
					{
						return false;
					}
				}
			}
			else
			{
				if (rgbKey.Length != 24)
				{
					throw new CryptographicException(Locale.GetText("Wrong Key Length"));
				}
				bool flag = true;
				for (int j = 0; j < 8; j++)
				{
					if (rgbKey[j] != rgbKey[j + 8])
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					for (int k = 8; k < 16; k++)
					{
						if (rgbKey[k] != rgbKey[k + 8])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		/// <summary>Creates an instance of a cryptographic object to perform the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>An instance of a cryptographic object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060039C6 RID: 14790 RVA: 0x000C62E4 File Offset: 0x000C44E4
		public new static TripleDES Create()
		{
			return TripleDES.Create("System.Security.Cryptography.TripleDES");
		}

		/// <summary>Creates an instance of a cryptographic object to perform the specified implementation of the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm.</summary>
		/// <returns>An instance of a cryptographic object.</returns>
		/// <param name="str">The name of the specific implementation of <see cref="T:System.Security.Cryptography.TripleDES" /> to use. </param>
		// Token: 0x060039C7 RID: 14791 RVA: 0x000C62F0 File Offset: 0x000C44F0
		public new static TripleDES Create(string str)
		{
			return (TripleDES)CryptoConfig.CreateFromName(str);
		}
	}
}
