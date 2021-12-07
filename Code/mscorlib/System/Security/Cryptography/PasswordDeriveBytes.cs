using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Derives a key from a password using an extension of the PBKDF1 algorithm.</summary>
	// Token: 0x020005C2 RID: 1474
	[ComVisible(true)]
	public class PasswordDeriveBytes : DeriveBytes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password and key salt to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		// Token: 0x0600386A RID: 14442 RVA: 0x000B7C50 File Offset: 0x000B5E50
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt)
		{
			this.Prepare(strPassword, rgbSalt, "SHA1", 100);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, and cryptographic service provider (CSP) parameters to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		/// <param name="cspParams">The CSP parameters for the operation. </param>
		// Token: 0x0600386B RID: 14443 RVA: 0x000B7C68 File Offset: 0x000B5E68
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams)
		{
			this.Prepare(strPassword, rgbSalt, "SHA1", 100);
			if (cspParams != null)
			{
				throw new NotSupportedException(Locale.GetText("CspParameters not supported by Mono for PasswordDeriveBytes."));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, hash name, and number of iterations to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		/// <param name="strHashName">The name of the hash algorithm for the operation. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		// Token: 0x0600386C RID: 14444 RVA: 0x000B7C98 File Offset: 0x000B5E98
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations)
		{
			this.Prepare(strPassword, rgbSalt, strHashName, iterations);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, hash name, number of iterations, and cryptographic service provider (CSP) parameters to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key. </param>
		/// <param name="rgbSalt">The key salt to use to derive the key. </param>
		/// <param name="strHashName">The name of the hash algorithm for the operation. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <param name="cspParams">The CSP parameters for the operation. </param>
		// Token: 0x0600386D RID: 14445 RVA: 0x000B7CAC File Offset: 0x000B5EAC
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams)
		{
			this.Prepare(strPassword, rgbSalt, strHashName, iterations);
			if (cspParams != null)
			{
				throw new NotSupportedException(Locale.GetText("CspParameters not supported by Mono for PasswordDeriveBytes."));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password and key salt to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		// Token: 0x0600386E RID: 14446 RVA: 0x000B7CE4 File Offset: 0x000B5EE4
		public PasswordDeriveBytes(byte[] password, byte[] salt)
		{
			this.Prepare(password, salt, "SHA1", 100);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, and cryptographic service provider (CSP) to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="cspParams">The cryptographic service provider (CSP) parameters for the operation.</param>
		// Token: 0x0600386F RID: 14447 RVA: 0x000B7CFC File Offset: 0x000B5EFC
		public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams)
		{
			this.Prepare(password, salt, "SHA1", 100);
			if (cspParams != null)
			{
				throw new NotSupportedException(Locale.GetText("CspParameters not supported by Mono for PasswordDeriveBytes."));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, hash name, and iterations to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="hashName">The hash algorithm to use to derive the key.</param>
		/// <param name="iterations">The iteration count to use to derive the key.</param>
		// Token: 0x06003870 RID: 14448 RVA: 0x000B7D2C File Offset: 0x000B5F2C
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations)
		{
			this.Prepare(password, salt, hashName, iterations);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, hash name, iterations, and cryptographic service provider (CSP) to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="hashName">The hash algorithm to use to derive the key.</param>
		/// <param name="iterations">The iteration count to use to derive the key.</param>
		/// <param name="cspParams">The cryptographic service provider (CSP) parameters for the operation.</param>
		// Token: 0x06003871 RID: 14449 RVA: 0x000B7D40 File Offset: 0x000B5F40
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
		{
			this.Prepare(password, salt, hashName, iterations);
			if (cspParams != null)
			{
				throw new NotSupportedException(Locale.GetText("CspParameters not supported by Mono for PasswordDeriveBytes."));
			}
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000B7D78 File Offset: 0x000B5F78
		~PasswordDeriveBytes()
		{
			if (this.initial != null)
			{
				Array.Clear(this.initial, 0, this.initial.Length);
				this.initial = null;
			}
			Array.Clear(this.password, 0, this.password.Length);
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x000B7DE8 File Offset: 0x000B5FE8
		private void Prepare(string strPassword, byte[] rgbSalt, string strHashName, int iterations)
		{
			if (strPassword == null)
			{
				throw new ArgumentNullException("strPassword");
			}
			byte[] bytes = Encoding.UTF8.GetBytes(strPassword);
			this.Prepare(bytes, rgbSalt, strHashName, iterations);
			Array.Clear(bytes, 0, bytes.Length);
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000B7E28 File Offset: 0x000B6028
		private void Prepare(byte[] password, byte[] rgbSalt, string strHashName, int iterations)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.password = (byte[])password.Clone();
			this.Salt = rgbSalt;
			this.HashName = strHashName;
			this.IterationCount = iterations;
			this.state = 0;
		}

		/// <summary>Gets or sets the name of the hash algorithm for the operation.</summary>
		/// <returns>The name of the hash algorithm for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The name of the hash value is fixed and an attempt is made to change this value. </exception>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000B7E74 File Offset: 0x000B6074
		// (set) Token: 0x06003876 RID: 14454 RVA: 0x000B7E7C File Offset: 0x000B607C
		public string HashName
		{
			get
			{
				return this.HashNameValue;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashName");
				}
				if (this.state != 0)
				{
					throw new CryptographicException(Locale.GetText("Can't change this property at this stage"));
				}
				this.HashNameValue = value;
			}
		}

		/// <summary>Gets or sets the number of iterations for the operation.</summary>
		/// <returns>The number of iterations for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The number of iterations is fixed and an attempt is made to change this value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property cannot be set because its value is out of range. This property requires a non-negative number.</exception>
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000B7EB4 File Offset: 0x000B60B4
		// (set) Token: 0x06003878 RID: 14456 RVA: 0x000B7EBC File Offset: 0x000B60BC
		public int IterationCount
		{
			get
			{
				return this.IterationsValue;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("> 0", "IterationCount");
				}
				if (this.state != 0)
				{
					throw new CryptographicException(Locale.GetText("Can't change this property at this stage"));
				}
				this.IterationsValue = value;
			}
		}

		/// <summary>Gets or sets the key salt value for the operation.</summary>
		/// <returns>The key salt value for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key salt value is fixed and an attempt is made to change this value. </exception>
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000B7EF8 File Offset: 0x000B60F8
		// (set) Token: 0x0600387A RID: 14458 RVA: 0x000B7F18 File Offset: 0x000B6118
		public byte[] Salt
		{
			get
			{
				if (this.SaltValue == null)
				{
					return null;
				}
				return (byte[])this.SaltValue.Clone();
			}
			set
			{
				if (this.state != 0)
				{
					throw new CryptographicException(Locale.GetText("Can't change this property at this stage"));
				}
				if (value != null)
				{
					this.SaltValue = (byte[])value.Clone();
				}
				else
				{
					this.SaltValue = null;
				}
			}
		}

		/// <summary>Derives a cryptographic key from the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> object.</summary>
		/// <returns>The derived key.</returns>
		/// <param name="algname">The algorithm name for which to derive the key. </param>
		/// <param name="alghashname">The hash algorithm name to use to derive the key. </param>
		/// <param name="keySize">The size of the key, in bits, to derive. </param>
		/// <param name="rgbIV">The initialization vector (IV) to use to derive the key. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="keySize" /> parameter is incorrect.-or- The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="algname" /> parameter is not a valid algorithm name.-or- The <paramref name="alghashname" /> parameter is not a valid hash algorithm name. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600387B RID: 14459 RVA: 0x000B7F64 File Offset: 0x000B6164
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize > 128)
			{
				throw new CryptographicException(Locale.GetText("Key Size can't be greater than 128 bits"));
			}
			throw new NotSupportedException(Locale.GetText("CspParameters not supported by Mono"));
		}

		/// <summary>Returns pseudo-random key bytes.</summary>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <param name="cb">The number of pseudo-random key bytes to generate. </param>
		// Token: 0x0600387C RID: 14460 RVA: 0x000B7F9C File Offset: 0x000B619C
		[Obsolete("see Rfc2898DeriveBytes for PKCS#5 v2 support")]
		public override byte[] GetBytes(int cb)
		{
			if (cb < 1)
			{
				throw new IndexOutOfRangeException("cb");
			}
			if (this.state == 0)
			{
				this.Reset();
				this.state = 1;
			}
			byte[] array = new byte[cb];
			int i = 0;
			int num = Math.Max(1, this.IterationsValue - 1);
			if (this.output == null)
			{
				this.output = this.initial;
				for (int j = 0; j < num - 1; j++)
				{
					this.output = this.hash.ComputeHash(this.output);
				}
			}
			while (i < cb)
			{
				byte[] array2;
				if (this.hashnumber == 0)
				{
					array2 = this.hash.ComputeHash(this.output);
				}
				else
				{
					if (this.hashnumber >= 1000)
					{
						throw new CryptographicException(Locale.GetText("too long"));
					}
					string text = Convert.ToString(this.hashnumber);
					array2 = new byte[this.output.Length + text.Length];
					for (int k = 0; k < text.Length; k++)
					{
						array2[k] = (byte)text[k];
					}
					Buffer.BlockCopy(this.output, 0, array2, text.Length, this.output.Length);
					array2 = this.hash.ComputeHash(array2);
				}
				int val = array2.Length - this.position;
				int num2 = Math.Min(cb - i, val);
				Buffer.BlockCopy(array2, this.position, array, i, num2);
				i += num2;
				this.position += num2;
				while (this.position >= array2.Length)
				{
					this.position -= array2.Length;
					this.hashnumber++;
				}
			}
			return array;
		}

		/// <summary>Resets the state of the operation.</summary>
		// Token: 0x0600387D RID: 14461 RVA: 0x000B8174 File Offset: 0x000B6374
		public override void Reset()
		{
			this.state = 0;
			this.position = 0;
			this.hashnumber = 0;
			this.hash = HashAlgorithm.Create(this.HashNameValue);
			if (this.SaltValue != null)
			{
				this.hash.TransformBlock(this.password, 0, this.password.Length, this.password, 0);
				this.hash.TransformFinalBlock(this.SaltValue, 0, this.SaltValue.Length);
				this.initial = this.hash.Hash;
			}
			else
			{
				this.initial = this.hash.ComputeHash(this.password);
			}
		}

		// Token: 0x0400187D RID: 6269
		private string HashNameValue;

		// Token: 0x0400187E RID: 6270
		private byte[] SaltValue;

		// Token: 0x0400187F RID: 6271
		private int IterationsValue;

		// Token: 0x04001880 RID: 6272
		private HashAlgorithm hash;

		// Token: 0x04001881 RID: 6273
		private int state;

		// Token: 0x04001882 RID: 6274
		private byte[] password;

		// Token: 0x04001883 RID: 6275
		private byte[] initial;

		// Token: 0x04001884 RID: 6276
		private byte[] output;

		// Token: 0x04001885 RID: 6277
		private int position;

		// Token: 0x04001886 RID: 6278
		private int hashnumber;
	}
}
