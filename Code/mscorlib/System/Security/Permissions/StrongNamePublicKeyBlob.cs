using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Represents the public key information (called a blob) for a strong name. This class cannot be inherited.</summary>
	// Token: 0x02000623 RID: 1571
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> class with raw bytes of the public key blob.</summary>
		/// <param name="publicKey">The array of bytes representing the raw public key data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="publicKey" /> parameter is null. </exception>
		// Token: 0x06003BE4 RID: 15332 RVA: 0x000CE000 File Offset: 0x000CC200
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			this.pubkey = publicKey;
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x000CE020 File Offset: 0x000CC220
		internal static StrongNamePublicKeyBlob FromString(string s)
		{
			if (s == null || s.Length == 0)
			{
				return null;
			}
			int num = s.Length / 2;
			byte[] array = new byte[num];
			int i = 0;
			int num2 = 0;
			while (i < s.Length)
			{
				byte b = StrongNamePublicKeyBlob.CharToByte(s[i]);
				byte b2 = StrongNamePublicKeyBlob.CharToByte(s[i + 1]);
				array[num2] = Convert.ToByte((int)(b * 16 + b2));
				i += 2;
				num2++;
			}
			return new StrongNamePublicKeyBlob(array);
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x000CE0A4 File Offset: 0x000CC2A4
		private static byte CharToByte(char c)
		{
			char c2 = char.ToLowerInvariant(c);
			if (char.IsDigit(c2))
			{
				return (byte)(c2 - '0');
			}
			return (byte)(c2 - 'a' + '\n');
		}

		/// <summary>Gets or sets a value indicating whether the current public key blob is equal to the specified public key blob.</summary>
		/// <returns>true if the public key blob of the current object is equal to the public key blob of the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object containing a public key blob. </param>
		// Token: 0x06003BE7 RID: 15335 RVA: 0x000CE0D4 File Offset: 0x000CC2D4
		public override bool Equals(object obj)
		{
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = obj as StrongNamePublicKeyBlob;
			if (strongNamePublicKeyBlob == null)
			{
				return false;
			}
			bool flag = this.pubkey.Length == strongNamePublicKeyBlob.pubkey.Length;
			if (flag)
			{
				for (int i = 0; i < this.pubkey.Length; i++)
				{
					if (this.pubkey[i] != strongNamePublicKeyBlob.pubkey[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		/// <summary>Returns a hash code based on the public key.</summary>
		/// <returns>The hash code based on the public key.</returns>
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000CE13C File Offset: 0x000CC33C
		public override int GetHashCode()
		{
			int num = 0;
			int i = 0;
			int num2 = Math.Min(this.pubkey.Length, 4);
			while (i < num2)
			{
				num = (num << 8) + (int)this.pubkey[i++];
			}
			return num;
		}

		/// <summary>Creates and returns a string representation of the public key blob.</summary>
		/// <returns>A hexadecimal version of the public key blob.</returns>
		// Token: 0x06003BE9 RID: 15337 RVA: 0x000CE17C File Offset: 0x000CC37C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.pubkey.Length; i++)
			{
				stringBuilder.Append(this.pubkey[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001A0D RID: 6669
		internal byte[] pubkey;
	}
}
