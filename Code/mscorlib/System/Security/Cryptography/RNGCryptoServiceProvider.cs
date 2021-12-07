using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Implements a cryptographic Random Number Generator (RNG) using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x020005CF RID: 1487
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		// Token: 0x060038DF RID: 14559 RVA: 0x000C25E4 File Offset: 0x000C07E4
		public RNGCryptoServiceProvider()
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null);
			this.Check();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		/// <param name="rgb">A byte array. This value is ignored.</param>
		// Token: 0x060038E0 RID: 14560 RVA: 0x000C2600 File Offset: 0x000C0800
		public RNGCryptoServiceProvider(byte[] rgb)
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(rgb);
			this.Check();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class with the specified parameters.</summary>
		/// <param name="cspParams">The parameters to pass to the cryptographic service provider (CSP). </param>
		// Token: 0x060038E1 RID: 14561 RVA: 0x000C261C File Offset: 0x000C081C
		public RNGCryptoServiceProvider(CspParameters cspParams)
		{
			this._handle = RNGCryptoServiceProvider.RngInitialize(null);
			this.Check();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> class.</summary>
		/// <param name="str">The string input. This parameter is ignored.</param>
		// Token: 0x060038E2 RID: 14562 RVA: 0x000C2638 File Offset: 0x000C0838
		public RNGCryptoServiceProvider(string str)
		{
			if (str == null)
			{
				this._handle = RNGCryptoServiceProvider.RngInitialize(null);
			}
			else
			{
				this._handle = RNGCryptoServiceProvider.RngInitialize(Encoding.UTF8.GetBytes(str));
			}
			this.Check();
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000C2680 File Offset: 0x000C0880
		static RNGCryptoServiceProvider()
		{
			if (RNGCryptoServiceProvider.RngOpen())
			{
				RNGCryptoServiceProvider._lock = new object();
			}
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000C2698 File Offset: 0x000C0898
		private void Check()
		{
			if (this._handle == IntPtr.Zero)
			{
				throw new CryptographicException(Locale.GetText("Couldn't access random source."));
			}
		}

		// Token: 0x060038E5 RID: 14565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RngOpen();

		// Token: 0x060038E6 RID: 14566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RngInitialize(byte[] seed);

		// Token: 0x060038E7 RID: 14567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RngGetBytes(IntPtr handle, byte[] data);

		// Token: 0x060038E8 RID: 14568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RngClose(IntPtr handle);

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		// Token: 0x060038E9 RID: 14569 RVA: 0x000C26C0 File Offset: 0x000C08C0
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (RNGCryptoServiceProvider._lock == null)
			{
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data);
			}
			else
			{
				object @lock = RNGCryptoServiceProvider._lock;
				lock (@lock)
				{
					this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, data);
				}
			}
			this.Check();
		}

		/// <summary>Fills an array of bytes with a cryptographically strong sequence of random nonzero values.</summary>
		/// <param name="data">The array to fill with a cryptographically strong sequence of random nonzero values. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		// Token: 0x060038EA RID: 14570 RVA: 0x000C274C File Offset: 0x000C094C
		public override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = new byte[data.Length * 2];
			int i = 0;
			while (i < data.Length)
			{
				this._handle = RNGCryptoServiceProvider.RngGetBytes(this._handle, array);
				this.Check();
				for (int j = 0; j < array.Length; j++)
				{
					if (i == data.Length)
					{
						break;
					}
					if (array[j] != 0)
					{
						data[i++] = array[j];
					}
				}
			}
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000C27D4 File Offset: 0x000C09D4
		~RNGCryptoServiceProvider()
		{
			if (this._handle != IntPtr.Zero)
			{
				RNGCryptoServiceProvider.RngClose(this._handle);
				this._handle = IntPtr.Zero;
			}
		}

		// Token: 0x040018AF RID: 6319
		private static object _lock;

		// Token: 0x040018B0 RID: 6320
		private IntPtr _handle;
	}
}
