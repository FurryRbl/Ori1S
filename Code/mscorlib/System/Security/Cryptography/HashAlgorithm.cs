using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of cryptographic hash algorithms must derive.</summary>
	// Token: 0x020005B0 RID: 1456
	[ComVisible(true)]
	public abstract class HashAlgorithm : IDisposable, ICryptoTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06003803 RID: 14339 RVA: 0x000B6090 File Offset: 0x000B4290
		protected HashAlgorithm()
		{
			this.disposed = false;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> and optionally releases the managed resources.</summary>
		// Token: 0x06003804 RID: 14340 RVA: 0x000B60A0 File Offset: 0x000B42A0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x000B60B0 File Offset: 0x000B42B0
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x000B60B4 File Offset: 0x000B42B4
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06003807 RID: 14343 RVA: 0x000B60B8 File Offset: 0x000B42B8
		public void Clear()
		{
			this.Dispose(true);
		}

		/// <summary>Computes the hash value for the specified byte array.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="buffer">The input to compute the hash code for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003808 RID: 14344 RVA: 0x000B60C4 File Offset: 0x000B42C4
		public byte[] ComputeHash(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.ComputeHash(buffer, 0, buffer.Length);
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="buffer">The input to compute the hash code for. </param>
		/// <param name="offset">The offset into the byte array from which to begin using data. </param>
		/// <param name="count">The number of bytes in the array to use as data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is an invalid value.-or-<paramref name="buffer" /> length is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003809 RID: 14345 RVA: 0x000B60E4 File Offset: 0x000B42E4
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("HashAlgorithm");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentException("count", "< 0");
			}
			if (offset > buffer.Length - count)
			{
				throw new ArgumentException("offset + count", Locale.GetText("Overflow"));
			}
			this.HashCore(buffer, offset, count);
			this.HashValue = this.HashFinal();
			this.Initialize();
			return this.HashValue;
		}

		/// <summary>Computes the hash value for the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <returns>The computed hash code.</returns>
		/// <param name="inputStream">The input to compute the hash code for. </param>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x0600380A RID: 14346 RVA: 0x000B6188 File Offset: 0x000B4388
		public byte[] ComputeHash(Stream inputStream)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("HashAlgorithm");
			}
			byte[] array = new byte[4096];
			for (int i = inputStream.Read(array, 0, 4096); i > 0; i = inputStream.Read(array, 0, 4096))
			{
				this.HashCore(array, 0, i);
			}
			this.HashValue = this.HashFinal();
			this.Initialize();
			return this.HashValue;
		}

		/// <summary>Creates an instance of the default implementation of a hash algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> instance, unless the default settings have been changed using the &lt;cryptoClass&gt; element.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600380B RID: 14347 RVA: 0x000B6200 File Offset: 0x000B4400
		public static HashAlgorithm Create()
		{
			return HashAlgorithm.Create("System.Security.Cryptography.HashAlgorithm");
		}

		/// <summary>Creates an instance of the specified implementation of a hash algorithm.</summary>
		/// <returns>A new instance of the specified hash algorithm, or null if <paramref name="hashName" /> is not a valid hash algorithm.</returns>
		/// <param name="hashName">The hash algorithm implementation to use. The following table shows the valid values for the <paramref name="hashName" /> parameter and the algorithms they map to. Parameter value Implements SHA <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.HashAlgorithm <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />System.Security.Cryptography.MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA-256 <see cref="T:System.Security.Cryptography.SHA256Managed" />System.Security.Cryptography.SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA-384 <see cref="T:System.Security.Cryptography.SHA384Managed" />System.Security.Cryptography.SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" />SHA-512 <see cref="T:System.Security.Cryptography.SHA512Managed" />System.Security.Cryptography.SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" /></param>
		// Token: 0x0600380C RID: 14348 RVA: 0x000B620C File Offset: 0x000B440C
		public static HashAlgorithm Create(string hashName)
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(hashName);
		}

		/// <summary>Gets the value of the computed hash code.</summary>
		/// <returns>The current value of the computed hash code.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
		///   <see cref="F:System.Security.Cryptography.HashAlgorithm.HashValue" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000B621C File Offset: 0x000B441C
		public virtual byte[] Hash
		{
			get
			{
				if (this.HashValue == null)
				{
					throw new CryptographicUnexpectedOperationException(Locale.GetText("No hash value computed."));
				}
				return this.HashValue;
			}
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the hash algorithm for computing the hash.</summary>
		/// <param name="array">The input to compute the hash code for. </param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data. </param>
		/// <param name="cbSize">The number of bytes in the byte array to use as data. </param>
		// Token: 0x0600380E RID: 14350
		protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code.</returns>
		// Token: 0x0600380F RID: 14351
		protected abstract byte[] HashFinal();

		/// <summary>Gets the size, in bits, of the computed hash code.</summary>
		/// <returns>The size, in bits, of the computed hash code.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x000B6240 File Offset: 0x000B4440
		public virtual int HashSize
		{
			get
			{
				return this.HashSizeValue;
			}
		}

		/// <summary>Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06003811 RID: 14353
		public abstract void Initialize();

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003812 RID: 14354 RVA: 0x000B6248 File Offset: 0x000B4448
		protected virtual void Dispose(bool disposing)
		{
			this.disposed = true;
		}

		/// <summary>When overridden in a derived class, gets the input block size.</summary>
		/// <returns>The input block size.</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06003813 RID: 14355 RVA: 0x000B6254 File Offset: 0x000B4454
		public virtual int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, gets the output block size.</summary>
		/// <returns>The output block size.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000B6258 File Offset: 0x000B4458
		public virtual int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>Computes the hash value for the specified region of the input byte array and copies the specified region of the input byte array to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input to compute the hash code for. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">A copy of the part of the input array used to compute the hash code. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003815 RID: 14357 RVA: 0x000B625C File Offset: 0x000B445C
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount < 0)
			{
				throw new ArgumentException("inputCount");
			}
			if (inputOffset < 0 || inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputBuffer");
			}
			if (outputBuffer != null)
			{
				if (outputOffset < 0)
				{
					throw new ArgumentOutOfRangeException("outputOffset", "< 0");
				}
				if (outputOffset > outputBuffer.Length - inputCount)
				{
					throw new ArgumentException("outputOffset + inputCount", Locale.GetText("Overflow"));
				}
			}
			this.HashCore(inputBuffer, inputOffset, inputCount);
			if (outputBuffer != null)
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <returns>An array that is a copy of the part of the input that is hashed.</returns>
		/// <param name="inputBuffer">The input to compute the hash code for. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06003816 RID: 14358 RVA: 0x000B6320 File Offset: 0x000B4520
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputCount < 0)
			{
				throw new ArgumentException("inputCount");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputOffset + inputCount", Locale.GetText("Overflow"));
			}
			byte[] array = new byte[inputCount];
			Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			this.HashCore(inputBuffer, inputOffset, inputCount);
			this.HashValue = this.HashFinal();
			this.Initialize();
			return array;
		}

		/// <summary>Represents the value of the computed hash code.</summary>
		// Token: 0x04001858 RID: 6232
		protected internal byte[] HashValue;

		/// <summary>Represents the size, in bits, of the computed hash code.</summary>
		// Token: 0x04001859 RID: 6233
		protected int HashSizeValue;

		/// <summary>Represents the state of the hash computation.</summary>
		// Token: 0x0400185A RID: 6234
		protected int State;

		// Token: 0x0400185B RID: 6235
		private bool disposed;
	}
}
