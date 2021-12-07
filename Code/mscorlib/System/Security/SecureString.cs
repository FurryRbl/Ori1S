using System;
using System.Runtime.ConstrainedExecution;

namespace System.Security
{
	/// <summary>Represents text that should be kept confidential. The text is encrypted for privacy when being used, and deleted from computer memory when no longer needed. This class cannot be inherited.</summary>
	// Token: 0x0200053F RID: 1343
	[MonoTODO("work in progress - encryption is missing")]
	public sealed class SecureString : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this instance.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x060034E0 RID: 13536 RVA: 0x000AE230 File Offset: 0x000AC430
		public SecureString()
		{
			this.Alloc(8, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecureString" /> class from a subarray of <see cref="T:System.Char" /> objects.</summary>
		/// <param name="value">A pointer to an array of <see cref="T:System.Char" /> objects.</param>
		/// <param name="length">The number of elements of <paramref name="value" /> to include in the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero or greater than 65536.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		/// <exception cref="T:System.NotSupportedException">This operation is not supported on this platform.</exception>
		// Token: 0x060034E1 RID: 13537 RVA: 0x000AE240 File Offset: 0x000AC440
		[CLSCompliant(false)]
		public unsafe SecureString(char* value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			this.length = length;
			this.Alloc(length, false);
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = *(value++);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			this.Encrypt();
		}

		/// <summary>Gets the number of characters in the current secure string.</summary>
		/// <returns>The number of <see cref="T:System.Char" /> objects in this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000AE2DC File Offset: 0x000AC4DC
		public int Length
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("SecureString");
				}
				return this.length;
			}
		}

		/// <summary>Appends a character to the end of the current secure string.</summary>
		/// <param name="c">A character to append to this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Performing this operation would make the length of this secure string greater than 65536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x060034E4 RID: 13540 RVA: 0x000AE2FC File Offset: 0x000AC4FC
		public void AppendChar(char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (this.length == 65536)
			{
				throw new ArgumentOutOfRangeException("length", "> 65536");
			}
			try
			{
				this.Decrypt();
				int num = this.length * 2;
				this.Alloc(++this.length, true);
				this.data[num++] = (byte)(c >> 8);
				this.data[num++] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Deletes the value of the current secure string.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		// Token: 0x060034E5 RID: 13541 RVA: 0x000AE3C8 File Offset: 0x000AC5C8
		public void Clear()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			Array.Clear(this.data, 0, this.data.Length);
			this.length = 0;
		}

		/// <summary>Creates a copy of the current secure string.</summary>
		/// <returns>A duplicate of this secure string.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x060034E6 RID: 13542 RVA: 0x000AE424 File Offset: 0x000AC624
		public SecureString Copy()
		{
			return new SecureString
			{
				data = (byte[])this.data.Clone(),
				length = this.length
			};
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Security.SecureString" /> object.</summary>
		// Token: 0x060034E7 RID: 13543 RVA: 0x000AE45C File Offset: 0x000AC65C
		public void Dispose()
		{
			this.disposed = true;
			if (this.data != null)
			{
				Array.Clear(this.data, 0, this.data.Length);
				this.data = null;
			}
			this.length = 0;
		}

		/// <summary>Inserts a character in this secure string at the specified index position.</summary>
		/// <param name="index">The index position where parameter <paramref name="c" /> is inserted.</param>
		/// <param name="c">The character to insert.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than the length of this secure string.-or-Performing this operation would make the length of this secure string greater than 65536 characters.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x060034E8 RID: 13544 RVA: 0x000AE4A0 File Offset: 0x000AC6A0
		public void InsertAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index > this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			if (this.length >= 65536)
			{
				string text = Locale.GetText("Maximum string size is '{0}'.", new object[]
				{
					65536
				});
				throw new ArgumentOutOfRangeException("index", text);
			}
			try
			{
				this.Decrypt();
				this.Alloc(++this.length, true);
				int num = index * 2;
				Buffer.BlockCopy(this.data, num, this.data, num + 2, this.data.Length - num - 2);
				this.data[num++] = (byte)(c >> 8);
				this.data[num] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Indicates whether this secure string is marked read-only.</summary>
		/// <returns>true if this secure string is marked read-only; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x060034E9 RID: 13545 RVA: 0x000AE5C0 File Offset: 0x000AC7C0
		public bool IsReadOnly()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			return this.read_only;
		}

		/// <summary>Makes the text value of this secure string read-only.   </summary>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		// Token: 0x060034EA RID: 13546 RVA: 0x000AE5E0 File Offset: 0x000AC7E0
		public void MakeReadOnly()
		{
			this.read_only = true;
		}

		/// <summary>Removes the character at the specified index position from this secure string.</summary>
		/// <param name="index">The index position of a character in this secure string.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x060034EB RID: 13547 RVA: 0x000AE5EC File Offset: 0x000AC7EC
		public void RemoveAt(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, index + 1, this.data, index, this.data.Length - index - 1);
				this.Alloc(--this.length, true);
			}
			finally
			{
				this.Encrypt();
			}
		}

		/// <summary>Replaces the existing character at the specified index position with another character.</summary>
		/// <param name="index">The index position of an existing character in this secure string</param>
		/// <param name="c">A character that replaces the existing character.</param>
		/// <exception cref="T:System.ObjectDisposedException">This secure string has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">This secure string is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero, or greater than or equal to the length of this secure string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred while encrypting or decrypting the value of this secure string.</exception>
		// Token: 0x060034EC RID: 13548 RVA: 0x000AE6B4 File Offset: 0x000AC8B4
		public void SetAt(int index, char c)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("SecureString");
			}
			if (this.read_only)
			{
				throw new InvalidOperationException(Locale.GetText("SecureString is read-only."));
			}
			if (index < 0 || index >= this.length)
			{
				throw new ArgumentOutOfRangeException("index", "< 0 || > length");
			}
			try
			{
				this.Decrypt();
				int num = index * 2;
				this.data[num++] = (byte)(c >> 8);
				this.data[num] = (byte)c;
			}
			finally
			{
				this.Encrypt();
			}
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000AE764 File Offset: 0x000AC964
		private void Encrypt()
		{
			if (this.data == null || this.data.Length > 0)
			{
			}
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000AE780 File Offset: 0x000AC980
		private void Decrypt()
		{
			if (this.data == null || this.data.Length > 0)
			{
			}
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000AE79C File Offset: 0x000AC99C
		private void Alloc(int length, bool realloc)
		{
			if (length < 0 || length > 65536)
			{
				throw new ArgumentOutOfRangeException("length", "< 0 || > 65536");
			}
			int num = (length >> 3) + (((length & 7) != 0) ? 1 : 0) << 4;
			if (realloc && this.data != null && num == this.data.Length)
			{
				return;
			}
			if (realloc)
			{
				byte[] array = new byte[num];
				Array.Copy(this.data, 0, array, 0, Math.Min(this.data.Length, array.Length));
				Array.Clear(this.data, 0, this.data.Length);
				this.data = array;
			}
			else
			{
				this.data = new byte[num];
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000AE858 File Offset: 0x000ACA58
		internal byte[] GetBuffer()
		{
			byte[] array = new byte[this.length << 1];
			try
			{
				this.Decrypt();
				Buffer.BlockCopy(this.data, 0, array, 0, array.Length);
			}
			finally
			{
				this.Encrypt();
			}
			return array;
		}

		// Token: 0x04001633 RID: 5683
		private const int BlockSize = 16;

		// Token: 0x04001634 RID: 5684
		private const int MaxSize = 65536;

		// Token: 0x04001635 RID: 5685
		private int length;

		// Token: 0x04001636 RID: 5686
		private bool disposed;

		// Token: 0x04001637 RID: 5687
		private bool read_only;

		// Token: 0x04001638 RID: 5688
		private byte[] data;
	}
}
