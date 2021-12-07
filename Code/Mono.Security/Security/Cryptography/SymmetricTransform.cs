using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200003A RID: 58
	internal abstract class SymmetricTransform : IDisposable, ICryptoTransform
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000F810 File Offset: 0x0000DA10
		public SymmetricTransform(SymmetricAlgorithm symmAlgo, bool encryption, byte[] rgbIV)
		{
			this.algo = symmAlgo;
			this.encrypt = encryption;
			this.BlockSizeByte = this.algo.BlockSize >> 3;
			if (rgbIV == null)
			{
				rgbIV = KeyBuilder.IV(this.BlockSizeByte);
			}
			else
			{
				rgbIV = (byte[])rgbIV.Clone();
			}
			if (rgbIV.Length < this.BlockSizeByte)
			{
				string text = Locale.GetText("IV is too small ({0} bytes), it should be {1} bytes long.", new object[]
				{
					rgbIV.Length,
					this.BlockSizeByte
				});
				throw new CryptographicException(text);
			}
			this.temp = new byte[this.BlockSizeByte];
			Buffer.BlockCopy(rgbIV, 0, this.temp, 0, Math.Min(this.BlockSizeByte, rgbIV.Length));
			this.temp2 = new byte[this.BlockSizeByte];
			this.FeedBackByte = this.algo.FeedbackSize >> 3;
			if (this.FeedBackByte != 0)
			{
				this.FeedBackIter = this.BlockSizeByte / this.FeedBackByte;
			}
			this.workBuff = new byte[this.BlockSizeByte];
			this.workout = new byte[this.BlockSizeByte];
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000F93C File Offset: 0x0000DB3C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000F94C File Offset: 0x0000DB4C
		~SymmetricTransform()
		{
			this.Dispose(false);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F988 File Offset: 0x0000DB88
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					Array.Clear(this.temp, 0, this.BlockSizeByte);
					this.temp = null;
					Array.Clear(this.temp2, 0, this.BlockSizeByte);
					this.temp2 = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
		public virtual bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		public virtual int InputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public virtual int OutputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		protected virtual void Transform(byte[] input, byte[] output)
		{
			switch (this.algo.Mode)
			{
			case CipherMode.CBC:
				this.CBC(input, output);
				break;
			case CipherMode.ECB:
				this.ECB(input, output);
				break;
			case CipherMode.OFB:
				this.OFB(input, output);
				break;
			case CipherMode.CFB:
				this.CFB(input, output);
				break;
			case CipherMode.CTS:
				this.CTS(input, output);
				break;
			default:
				throw new NotImplementedException("Unkown CipherMode" + this.algo.Mode.ToString());
			}
		}

		// Token: 0x0600026C RID: 620
		protected abstract void ECB(byte[] input, byte[] output);

		// Token: 0x0600026D RID: 621 RVA: 0x0000FA98 File Offset: 0x0000DC98
		protected virtual void CBC(byte[] input, byte[] output)
		{
			if (this.encrypt)
			{
				for (int i = 0; i < this.BlockSizeByte; i++)
				{
					byte[] array = this.temp;
					int num = i;
					array[num] ^= input[i];
				}
				this.ECB(this.temp, output);
				Buffer.BlockCopy(output, 0, this.temp, 0, this.BlockSizeByte);
			}
			else
			{
				Buffer.BlockCopy(input, 0, this.temp2, 0, this.BlockSizeByte);
				this.ECB(input, output);
				for (int j = 0; j < this.BlockSizeByte; j++)
				{
					int num2 = j;
					output[num2] ^= this.temp[j];
				}
				Buffer.BlockCopy(this.temp2, 0, this.temp, 0, this.BlockSizeByte);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000FB64 File Offset: 0x0000DD64
		protected virtual void CFB(byte[] input, byte[] output)
		{
			if (this.encrypt)
			{
				for (int i = 0; i < this.FeedBackIter; i++)
				{
					this.ECB(this.temp, this.temp2);
					for (int j = 0; j < this.FeedBackByte; j++)
					{
						output[j + i] = (this.temp2[j] ^ input[j + i]);
					}
					Buffer.BlockCopy(this.temp, this.FeedBackByte, this.temp, 0, this.BlockSizeByte - this.FeedBackByte);
					Buffer.BlockCopy(output, i, this.temp, this.BlockSizeByte - this.FeedBackByte, this.FeedBackByte);
				}
			}
			else
			{
				for (int k = 0; k < this.FeedBackIter; k++)
				{
					this.encrypt = true;
					this.ECB(this.temp, this.temp2);
					this.encrypt = false;
					Buffer.BlockCopy(this.temp, this.FeedBackByte, this.temp, 0, this.BlockSizeByte - this.FeedBackByte);
					Buffer.BlockCopy(input, k, this.temp, this.BlockSizeByte - this.FeedBackByte, this.FeedBackByte);
					for (int l = 0; l < this.FeedBackByte; l++)
					{
						output[l + k] = (this.temp2[l] ^ input[l + k]);
					}
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		protected virtual void OFB(byte[] input, byte[] output)
		{
			throw new CryptographicException("OFB isn't supported by the framework");
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		protected virtual void CTS(byte[] input, byte[] output)
		{
			throw new CryptographicException("CTS isn't supported by the framework");
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		private void CheckInput(byte[] inputBuffer, int inputOffset, int inputCount)
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
				throw new ArgumentOutOfRangeException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputBuffer", Locale.GetText("Overflow"));
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000FD48 File Offset: 0x0000DF48
		public virtual int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			int num = outputBuffer.Length - inputCount - outputOffset;
			if (!this.encrypt && 0 > num && (this.algo.Padding == PaddingMode.None || this.algo.Padding == PaddingMode.Zeros))
			{
				throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
			}
			if (this.KeepLastBlock)
			{
				if (0 > num + this.BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
				}
			}
			else if (0 > num)
			{
				if (inputBuffer.Length - inputOffset - outputBuffer.Length != this.BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
				}
				inputCount = outputBuffer.Length - outputOffset;
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000FE70 File Offset: 0x0000E070
		private bool KeepLastBlock
		{
			get
			{
				return !this.encrypt && this.algo.Padding != PaddingMode.None && this.algo.Padding != PaddingMode.Zeros;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = inputOffset;
			int num2;
			if (inputCount != this.BlockSizeByte)
			{
				if (inputCount % this.BlockSizeByte != 0)
				{
					throw new CryptographicException("Invalid input block size.");
				}
				num2 = inputCount / this.BlockSizeByte;
			}
			else
			{
				num2 = 1;
			}
			if (this.KeepLastBlock)
			{
				num2--;
			}
			int num3 = 0;
			if (this.lastBlock)
			{
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, outputBuffer, outputOffset, this.BlockSizeByte);
				outputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
				this.lastBlock = false;
			}
			for (int i = 0; i < num2; i++)
			{
				Buffer.BlockCopy(inputBuffer, num, this.workBuff, 0, this.BlockSizeByte);
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, outputBuffer, outputOffset, this.BlockSizeByte);
				num += this.BlockSizeByte;
				outputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
			}
			if (this.KeepLastBlock)
			{
				Buffer.BlockCopy(inputBuffer, num, this.workBuff, 0, this.BlockSizeByte);
				this.lastBlock = true;
			}
			return num3;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		private void Random(byte[] buffer, int start, int length)
		{
			if (this._rng == null)
			{
				this._rng = RandomNumberGenerator.Create();
			}
			byte[] array = new byte[length];
			this._rng.GetBytes(array);
			Buffer.BlockCopy(array, 0, buffer, start, length);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010024 File Offset: 0x0000E224
		private void ThrowBadPaddingException(PaddingMode padding, int length, int position)
		{
			string text = string.Format(Locale.GetText("Bad {0} padding."), padding);
			if (length >= 0)
			{
				text += string.Format(Locale.GetText(" Invalid length {0}."), length);
			}
			if (position >= 0)
			{
				text += string.Format(Locale.GetText(" Error found at position {0}."), position);
			}
			throw new CryptographicException(text);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00010094 File Offset: 0x0000E294
		private byte[] FinalEncrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = inputCount / this.BlockSizeByte * this.BlockSizeByte;
			int num2 = inputCount - num;
			int i = num;
			switch (this.algo.Padding)
			{
			case PaddingMode.PKCS7:
			case PaddingMode.ANSIX923:
			case PaddingMode.ISO10126:
				i += this.BlockSizeByte;
				goto IL_A8;
			}
			if (inputCount == 0)
			{
				return new byte[0];
			}
			if (num2 != 0)
			{
				if (this.algo.Padding == PaddingMode.None)
				{
					throw new CryptographicException("invalid block length");
				}
				byte[] array = new byte[num + this.BlockSizeByte];
				Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				inputBuffer = array;
				inputOffset = 0;
				inputCount = array.Length;
				i = inputCount;
			}
			IL_A8:
			byte[] array2 = new byte[i];
			int num3 = 0;
			while (i > this.BlockSizeByte)
			{
				this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array2, num3);
				inputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
				i -= this.BlockSizeByte;
			}
			byte b = (byte)(this.BlockSizeByte - num2);
			switch (this.algo.Padding)
			{
			case PaddingMode.PKCS7:
			{
				int num4 = array2.Length;
				while (--num4 >= array2.Length - (int)b)
				{
					array2[num4] = b;
				}
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			}
			case PaddingMode.ANSIX923:
				array2[array2.Length - 1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			case PaddingMode.ISO10126:
				this.Random(array2, array2.Length - (int)b, (int)(b - 1));
				array2[array2.Length - 1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			}
			this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array2, num3);
			return array2;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000102A0 File Offset: 0x0000E4A0
		private byte[] FinalDecrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputCount % this.BlockSizeByte > 0)
			{
				throw new CryptographicException("Invalid input block size.");
			}
			int num = inputCount;
			if (this.lastBlock)
			{
				num += this.BlockSizeByte;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			while (inputCount > 0)
			{
				int num3 = this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array, num2);
				inputOffset += this.BlockSizeByte;
				num2 += num3;
				inputCount -= this.BlockSizeByte;
			}
			if (this.lastBlock)
			{
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, array, num2, this.BlockSizeByte);
				num2 += this.BlockSizeByte;
				this.lastBlock = false;
			}
			byte b = (num <= 0) ? 0 : array[num - 1];
			switch (this.algo.Padding)
			{
			case PaddingMode.PKCS7:
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.algo.Padding, (int)b, -1);
				}
				for (int i = (int)(b - 1); i > 0; i--)
				{
					if (array[num - 1 - i] != b)
					{
						this.ThrowBadPaddingException(this.algo.Padding, -1, i);
					}
				}
				num -= (int)b;
				break;
			case PaddingMode.ANSIX923:
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.algo.Padding, (int)b, -1);
				}
				for (int j = (int)(b - 1); j > 0; j--)
				{
					if (array[num - 1 - j] != 0)
					{
						this.ThrowBadPaddingException(this.algo.Padding, -1, j);
					}
				}
				num -= (int)b;
				break;
			case PaddingMode.ISO10126:
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.algo.Padding, (int)b, -1);
				}
				num -= (int)b;
				break;
			}
			if (num > 0)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				Array.Clear(array, 0, array.Length);
				return array2;
			}
			return new byte[0];
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000104DC File Offset: 0x0000E6DC
		public virtual byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (this.encrypt)
			{
				return this.FinalEncrypt(inputBuffer, inputOffset, inputCount);
			}
			return this.FinalDecrypt(inputBuffer, inputOffset, inputCount);
		}

		// Token: 0x04000106 RID: 262
		protected SymmetricAlgorithm algo;

		// Token: 0x04000107 RID: 263
		protected bool encrypt;

		// Token: 0x04000108 RID: 264
		private int BlockSizeByte;

		// Token: 0x04000109 RID: 265
		private byte[] temp;

		// Token: 0x0400010A RID: 266
		private byte[] temp2;

		// Token: 0x0400010B RID: 267
		private byte[] workBuff;

		// Token: 0x0400010C RID: 268
		private byte[] workout;

		// Token: 0x0400010D RID: 269
		private int FeedBackByte;

		// Token: 0x0400010E RID: 270
		private int FeedBackIter;

		// Token: 0x0400010F RID: 271
		private bool m_disposed;

		// Token: 0x04000110 RID: 272
		private bool lastBlock;

		// Token: 0x04000111 RID: 273
		private RandomNumberGenerator _rng;
	}
}
