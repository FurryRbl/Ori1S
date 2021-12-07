using System;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Implements password-based key derivation functionality, PBKDF2, by using a pseudo-random number generator based on <see cref="T:System.Security.Cryptography.HMACSHA1" />.</summary>
	// Token: 0x020005C8 RID: 1480
	[ComVisible(true)]
	public class Rfc2898DeriveBytes : DeriveBytes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password and salt to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="salt">The key salt used to derive the key. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x0600389A RID: 14490 RVA: 0x000B918C File Offset: 0x000B738C
		public Rfc2898DeriveBytes(string password, byte[] salt) : this(password, salt, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="salt">The key salt used to derive the key. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x0600389B RID: 14491 RVA: 0x000B919C File Offset: 0x000B739C
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.Salt = salt;
			this.IterationCount = iterations;
			this._hmac = new HMACSHA1(Encoding.UTF8.GetBytes(password));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x0600389C RID: 14492 RVA: 0x000B91E4 File Offset: 0x000B73E4
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.Salt = salt;
			this.IterationCount = iterations;
			this._hmac = new HMACSHA1(password);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the password and salt size to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		// Token: 0x0600389D RID: 14493 RVA: 0x000B9224 File Offset: 0x000B7424
		public Rfc2898DeriveBytes(string password, int saltSize) : this(password, saltSize, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt size, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key. </param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate. </param>
		/// <param name="iterations">The number of iterations for the operation. </param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1. </exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="iterations " />is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x0600389E RID: 14494 RVA: 0x000B9234 File Offset: 0x000B7434
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (saltSize < 0)
			{
				throw new ArgumentOutOfRangeException("invalid salt length");
			}
			this.Salt = KeyBuilder.Key(saltSize);
			this.IterationCount = iterations;
			this._hmac = new HMACSHA1(Encoding.UTF8.GetBytes(password));
		}

		/// <summary>Gets or sets the number of iterations for the operation.</summary>
		/// <returns>The number of iterations for the operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of iterations is less than 1. </exception>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x000B9294 File Offset: 0x000B7494
		// (set) Token: 0x060038A0 RID: 14496 RVA: 0x000B929C File Offset: 0x000B749C
		public int IterationCount
		{
			get
			{
				return this._iteration;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("IterationCount < 1");
				}
				this._iteration = value;
			}
		}

		/// <summary>Gets or sets the key salt value for the operation.</summary>
		/// <returns>The key salt value for the operation.</returns>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes. </exception>
		/// <exception cref="T:System.ArgumentNullException">The salt is null. </exception>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x000B92B8 File Offset: 0x000B74B8
		// (set) Token: 0x060038A2 RID: 14498 RVA: 0x000B92CC File Offset: 0x000B74CC
		public byte[] Salt
		{
			get
			{
				return (byte[])this._salt.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Salt");
				}
				if (value.Length < 8)
				{
					throw new ArgumentException("Salt < 8 bytes");
				}
				this._salt = (byte[])value.Clone();
			}
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000B9310 File Offset: 0x000B7510
		private byte[] F(byte[] s, int c, int i)
		{
			s[s.Length - 4] = (byte)(i >> 24);
			s[s.Length - 3] = (byte)(i >> 16);
			s[s.Length - 2] = (byte)(i >> 8);
			s[s.Length - 1] = (byte)i;
			byte[] array = this._hmac.ComputeHash(s);
			byte[] buffer = array;
			for (int j = 1; j < c; j++)
			{
				byte[] array2 = this._hmac.ComputeHash(buffer);
				for (int k = 0; k < 20; k++)
				{
					array[k] ^= array2[k];
				}
				buffer = array2;
			}
			return array;
		}

		/// <summary>Returns the pseudo-random key for this object.</summary>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <param name="cb">The number of pseudo-random key bytes to generate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cb " />is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x060038A4 RID: 14500 RVA: 0x000B93A0 File Offset: 0x000B75A0
		public override byte[] GetBytes(int cb)
		{
			if (cb < 1)
			{
				throw new ArgumentOutOfRangeException("cb");
			}
			int num = cb / 20;
			int num2 = cb % 20;
			if (num2 != 0)
			{
				num++;
			}
			byte[] array = new byte[cb];
			int num3 = 0;
			if (this._pos > 0)
			{
				int num4 = Math.Min(20 - this._pos, cb);
				Buffer.BlockCopy(this._buffer, this._pos, array, 0, num4);
				if (num4 >= cb)
				{
					return array;
				}
				this._pos = 0;
				num3 = num4;
			}
			byte[] array2 = new byte[this._salt.Length + 4];
			Buffer.BlockCopy(this._salt, 0, array2, 0, this._salt.Length);
			for (int i = 1; i <= num; i++)
			{
				this._buffer = this.F(array2, this._iteration, ++this._f);
				int num5 = (i != num) ? 20 : (array.Length - num3);
				Buffer.BlockCopy(this._buffer, this._pos, array, num3, num5);
				num3 += this._pos + num5;
				this._pos = ((num5 != 20) ? num5 : 0);
			}
			return array;
		}

		/// <summary>Resets the state of the operation.</summary>
		// Token: 0x060038A5 RID: 14501 RVA: 0x000B94D8 File Offset: 0x000B76D8
		public override void Reset()
		{
			this._buffer = null;
			this._pos = 0;
			this._f = 0;
		}

		// Token: 0x04001891 RID: 6289
		private const int defaultIterations = 1000;

		// Token: 0x04001892 RID: 6290
		private int _iteration;

		// Token: 0x04001893 RID: 6291
		private byte[] _salt;

		// Token: 0x04001894 RID: 6292
		private HMACSHA1 _hmac;

		// Token: 0x04001895 RID: 6293
		private byte[] _buffer;

		// Token: 0x04001896 RID: 6294
		private int _pos;

		// Token: 0x04001897 RID: 6295
		private int _f;
	}
}
