using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Converts a <see cref="T:System.Security.Cryptography.CryptoStream" /> to base 64.</summary>
	// Token: 0x020005E8 RID: 1512
	[ComVisible(true)]
	public class ToBase64Transform : IDisposable, ICryptoTransform
	{
		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x060039B6 RID: 14774 RVA: 0x000C5DA0 File Offset: 0x000C3FA0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x060039B7 RID: 14775 RVA: 0x000C5DB0 File Offset: 0x000C3FB0
		~ToBase64Transform()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a value that indicates whether multiple blocks can be transformed.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000C5DEC File Offset: 0x000C3FEC
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000C5DF0 File Offset: 0x000C3FF0
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000C5DF4 File Offset: 0x000C3FF4
		public int InputBlockSize
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x000C5DF8 File Offset: 0x000C3FF8
		public int OutputBlockSize
		{
			get
			{
				return 4;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x060039BC RID: 14780 RVA: 0x000C5DFC File Offset: 0x000C3FFC
		public void Clear()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060039BD RID: 14781 RVA: 0x000C5E08 File Offset: 0x000C4008
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
				}
				this.m_disposed = true;
			}
		}

		/// <summary>Converts the specified region of the input byte array to base 64 and copies the result to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input to compute to base 64. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the result. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data size is not valid. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.-or-The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x060039BE RID: 14782 RVA: 0x000C5E24 File Offset: 0x000C4024
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("TransformBlock");
			}
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (inputCount < 0)
			{
				throw new ArgumentException("inputCount", "< 0");
			}
			if (inputCount > inputBuffer.Length)
			{
				throw new ArgumentException("inputCount", Locale.GetText("Overflow"));
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputOffset", Locale.GetText("Overflow"));
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			if (outputOffset > outputBuffer.Length - inputCount)
			{
				throw new ArgumentException("outputOffset", Locale.GetText("Overflow"));
			}
			ToBase64Transform.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
			return this.OutputBlockSize;
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000C5F24 File Offset: 0x000C4124
		internal static void InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			byte[] encodeTable = Base64Constants.EncodeTable;
			int num = (int)inputBuffer[inputOffset];
			int num2 = (int)inputBuffer[inputOffset + 1];
			int num3 = (int)inputBuffer[inputOffset + 2];
			outputBuffer[outputOffset] = encodeTable[num >> 2];
			outputBuffer[outputOffset + 1] = encodeTable[(num << 4 & 48) | num2 >> 4];
			outputBuffer[outputOffset + 2] = encodeTable[(num2 << 2 & 60) | num3 >> 6];
			outputBuffer[outputOffset + 3] = encodeTable[num3 & 63];
		}

		/// <summary>Converts the specified region of the specified byte array to base 64.</summary>
		/// <returns>The computed base 64 conversion.</returns>
		/// <param name="inputBuffer">The input to convert to base 64. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.-or-The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x060039C0 RID: 14784 RVA: 0x000C5F80 File Offset: 0x000C4180
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("TransformFinalBlock");
			}
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputCount < 0)
			{
				throw new ArgumentException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputCount", Locale.GetText("Overflow"));
			}
			if (inputCount > this.InputBlockSize)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Invalid input length"));
			}
			return ToBase64Transform.InternalTransformFinalBlock(inputBuffer, inputOffset, inputCount);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000C6010 File Offset: 0x000C4210
		internal static byte[] InternalTransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = 3;
			int num2 = 4;
			int num3 = inputCount / num;
			int num4 = inputCount % num;
			byte[] array = new byte[(inputCount == 0) ? 0 : ((inputCount + 2) / num * num2)];
			int num5 = 0;
			for (int i = 0; i < num3; i++)
			{
				ToBase64Transform.InternalTransformBlock(inputBuffer, inputOffset, num, array, num5);
				inputOffset += num;
				num5 += num2;
			}
			byte[] encodeTable = Base64Constants.EncodeTable;
			switch (num4)
			{
			case 1:
			{
				int num6 = (int)inputBuffer[inputOffset];
				array[num5] = encodeTable[num6 >> 2];
				array[num5 + 1] = encodeTable[num6 << 4 & 48];
				array[num5 + 2] = 61;
				array[num5 + 3] = 61;
				break;
			}
			case 2:
			{
				int num6 = (int)inputBuffer[inputOffset];
				int num7 = (int)inputBuffer[inputOffset + 1];
				array[num5] = encodeTable[num6 >> 2];
				array[num5 + 1] = encodeTable[(num6 << 4 & 48) | num7 >> 4];
				array[num5 + 2] = encodeTable[num7 << 2 & 60];
				array[num5 + 3] = 61;
				break;
			}
			}
			return array;
		}

		// Token: 0x0400190A RID: 6410
		private const int inputBlockSize = 3;

		// Token: 0x0400190B RID: 6411
		private const int outputBlockSize = 4;

		// Token: 0x0400190C RID: 6412
		private bool m_disposed;
	}
}
