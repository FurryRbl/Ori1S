using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Converts a <see cref="T:System.Security.Cryptography.CryptoStream" /> from base 64.</summary>
	// Token: 0x020005AF RID: 1455
	[ComVisible(true)]
	public class FromBase64Transform : IDisposable, ICryptoTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class.</summary>
		// Token: 0x060037F4 RID: 14324 RVA: 0x000B5B24 File Offset: 0x000B3D24
		public FromBase64Transform() : this(FromBase64TransformMode.IgnoreWhiteSpaces)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> class with the specified transformation mode.</summary>
		/// <param name="whitespaces">One of the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> values. </param>
		// Token: 0x060037F5 RID: 14325 RVA: 0x000B5B30 File Offset: 0x000B3D30
		public FromBase64Transform(FromBase64TransformMode whitespaces)
		{
			this.mode = whitespaces;
			this.accumulator = new byte[4];
			this.accPtr = 0;
			this.m_disposed = false;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> and optionally releases the managed resources.</summary>
		// Token: 0x060037F6 RID: 14326 RVA: 0x000B5B5C File Offset: 0x000B3D5C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" />.</summary>
		// Token: 0x060037F7 RID: 14327 RVA: 0x000B5B6C File Offset: 0x000B3D6C
		~FromBase64Transform()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a value that indicates whether multiple blocks can be transformed.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000B5BA8 File Offset: 0x000B3DA8
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x000B5BAC File Offset: 0x000B3DAC
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000B5BB0 File Offset: 0x000B3DB0
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000B5BB4 File Offset: 0x000B3DB4
		public int OutputBlockSize
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" />.</summary>
		// Token: 0x060037FC RID: 14332 RVA: 0x000B5BB8 File Offset: 0x000B3DB8
		public void Clear()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.FromBase64Transform" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060037FD RID: 14333 RVA: 0x000B5BC4 File Offset: 0x000B3DC4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.accumulator != null)
				{
					Array.Clear(this.accumulator, 0, this.accumulator.Length);
				}
				if (disposing)
				{
					this.accumulator = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000B5C10 File Offset: 0x000B3E10
		private byte lookup(byte input)
		{
			if ((int)input >= this.lookupTable.Length)
			{
				throw new FormatException(Locale.GetText("Invalid character in a Base-64 string."));
			}
			byte b = this.lookupTable[(int)input];
			if (b == 255)
			{
				throw new FormatException(Locale.GetText("Invalid character in a Base-64 string."));
			}
			return b;
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000B5C60 File Offset: 0x000B3E60
		private int ProcessBlock(byte[] output, int offset)
		{
			int num = 0;
			if (this.accumulator[3] == 61)
			{
				num++;
			}
			if (this.accumulator[2] == 61)
			{
				num++;
			}
			this.lookupTable = Base64Constants.DecodeTable;
			switch (num)
			{
			case 0:
			{
				int num2 = (int)this.lookup(this.accumulator[0]);
				int num3 = (int)this.lookup(this.accumulator[1]);
				int num4 = (int)this.lookup(this.accumulator[2]);
				int num5 = (int)this.lookup(this.accumulator[3]);
				output[offset++] = (byte)(num2 << 2 | num3 >> 4);
				output[offset++] = (byte)(num3 << 4 | num4 >> 2);
				output[offset] = (byte)(num4 << 6 | num5);
				break;
			}
			case 1:
			{
				int num2 = (int)this.lookup(this.accumulator[0]);
				int num3 = (int)this.lookup(this.accumulator[1]);
				int num4 = (int)this.lookup(this.accumulator[2]);
				output[offset++] = (byte)(num2 << 2 | num3 >> 4);
				output[offset] = (byte)(num3 << 4 | num4 >> 2);
				break;
			}
			case 2:
			{
				int num2 = (int)this.lookup(this.accumulator[0]);
				int num3 = (int)this.lookup(this.accumulator[1]);
				output[offset] = (byte)(num2 << 2 | num3 >> 4);
				break;
			}
			}
			return 3 - num;
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000B5DA8 File Offset: 0x000B3FA8
		private void CheckInputParameters(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount > inputBuffer.Length)
			{
				throw new OutOfMemoryException("inputCount " + Locale.GetText("Overflow"));
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputOffset", Locale.GetText("Overflow"));
			}
			if (inputCount < 0)
			{
				throw new OverflowException("inputCount < 0");
			}
		}

		/// <summary>Converts the specified region of the input byte array from base 64 and copies the result to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input to compute from base 64. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the result. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.FromBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.-or-<paramref name="inputBuffer" /> has an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		// Token: 0x06003801 RID: 14337 RVA: 0x000B5E34 File Offset: 0x000B4034
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("FromBase64Transform");
			}
			this.CheckInputParameters(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null || outputOffset < 0)
			{
				throw new FormatException("outputBuffer");
			}
			int num = 0;
			while (inputCount > 0)
			{
				if (this.accPtr < 4)
				{
					byte b = inputBuffer[inputOffset++];
					if (this.mode == FromBase64TransformMode.IgnoreWhiteSpaces)
					{
						if (!char.IsWhiteSpace((char)b))
						{
							this.accumulator[this.accPtr++] = b;
						}
					}
					else
					{
						this.accumulator[this.accPtr++] = b;
					}
				}
				if (this.accPtr == 4)
				{
					num += this.ProcessBlock(outputBuffer, outputOffset);
					outputOffset += 3;
					this.accPtr = 0;
				}
				inputCount--;
			}
			return num;
		}

		/// <summary>Converts the specified region of the specified byte array from base 64.</summary>
		/// <returns>The computed conversion.</returns>
		/// <param name="inputBuffer">The input to convert from base 64. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.FromBase64Transform" /> object has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputBuffer" /> has an invalid offset length.-or-<paramref name="inputCount" /> has an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is null.</exception>
		// Token: 0x06003802 RID: 14338 RVA: 0x000B5F18 File Offset: 0x000B4118
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("FromBase64Transform");
			}
			this.CheckInputParameters(inputBuffer, inputOffset, inputCount);
			int num = 0;
			int num2 = 0;
			if (this.mode == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				int num3 = inputOffset;
				for (int i = 0; i < inputCount; i++)
				{
					if (char.IsWhiteSpace((char)inputBuffer[num3]))
					{
						num++;
					}
					num3++;
				}
				if (num == inputCount)
				{
					return new byte[0];
				}
				int num4 = inputOffset + inputCount - 1;
				int j = Math.Min(2, inputCount);
				while (j > 0)
				{
					char c = (char)inputBuffer[num4--];
					if (c == '=')
					{
						num2++;
						j--;
					}
					else if (!char.IsWhiteSpace(c))
					{
						break;
					}
				}
			}
			else
			{
				if (inputBuffer[inputOffset + inputCount - 1] == 61)
				{
					num2++;
				}
				if (inputBuffer[inputOffset + inputCount - 2] == 61)
				{
					num2++;
				}
			}
			if (inputCount < 4 && num2 < 2)
			{
				if (this.accPtr > 2 && this.accumulator[3] == 61)
				{
					num2++;
				}
				if (this.accPtr > 1 && this.accumulator[2] == 61)
				{
					num2++;
				}
			}
			int num5 = (this.accPtr + inputCount - num >> 2) * 3 - num2;
			if (num5 <= 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[num5];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x04001852 RID: 6226
		private const byte TerminatorByte = 61;

		// Token: 0x04001853 RID: 6227
		private FromBase64TransformMode mode;

		// Token: 0x04001854 RID: 6228
		private byte[] accumulator;

		// Token: 0x04001855 RID: 6229
		private int accPtr;

		// Token: 0x04001856 RID: 6230
		private bool m_disposed;

		// Token: 0x04001857 RID: 6231
		private byte[] lookupTable;
	}
}
