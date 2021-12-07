using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000010 RID: 16
	internal abstract class SymmetricTransform : IDisposable, ICryptoTransform
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00005ACC File Offset: 0x00003CCC
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

		// Token: 0x060000E5 RID: 229 RVA: 0x00005BF8 File Offset: 0x00003DF8
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005C08 File Offset: 0x00003E08
		~SymmetricTransform()
		{
			this.Dispose(false);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005C44 File Offset: 0x00003E44
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005C9C File Offset: 0x00003E9C
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public virtual bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public virtual int InputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005CAC File Offset: 0x00003EAC
		public virtual int OutputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005CB4 File Offset: 0x00003EB4
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

		// Token: 0x060000ED RID: 237
		protected abstract void ECB(byte[] input, byte[] output);

		// Token: 0x060000EE RID: 238 RVA: 0x00005D54 File Offset: 0x00003F54
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

		// Token: 0x060000EF RID: 239 RVA: 0x00005E20 File Offset: 0x00004020
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

		// Token: 0x060000F0 RID: 240 RVA: 0x00005F80 File Offset: 0x00004180
		protected virtual void OFB(byte[] input, byte[] output)
		{
			throw new CryptographicException("OFB isn't supported by the framework");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005F8C File Offset: 0x0000418C
		protected virtual void CTS(byte[] input, byte[] output)
		{
			throw new CryptographicException("CTS isn't supported by the framework");
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005F98 File Offset: 0x00004198
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

		// Token: 0x060000F3 RID: 243 RVA: 0x00006004 File Offset: 0x00004204
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000612C File Offset: 0x0000432C
		private bool KeepLastBlock
		{
			get
			{
				return !this.encrypt && this.algo.Padding != PaddingMode.None && this.algo.Padding != PaddingMode.Zeros;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000616C File Offset: 0x0000436C
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

		// Token: 0x060000F6 RID: 246 RVA: 0x000062A0 File Offset: 0x000044A0
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

		// Token: 0x060000F7 RID: 247 RVA: 0x000062E0 File Offset: 0x000044E0
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

		// Token: 0x060000F8 RID: 248 RVA: 0x00006350 File Offset: 0x00004550
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

		// Token: 0x060000F9 RID: 249 RVA: 0x0000655C File Offset: 0x0000475C
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

		// Token: 0x060000FA RID: 250 RVA: 0x00006798 File Offset: 0x00004998
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

		// Token: 0x04000037 RID: 55
		protected SymmetricAlgorithm algo;

		// Token: 0x04000038 RID: 56
		protected bool encrypt;

		// Token: 0x04000039 RID: 57
		private int BlockSizeByte;

		// Token: 0x0400003A RID: 58
		private byte[] temp;

		// Token: 0x0400003B RID: 59
		private byte[] temp2;

		// Token: 0x0400003C RID: 60
		private byte[] workBuff;

		// Token: 0x0400003D RID: 61
		private byte[] workout;

		// Token: 0x0400003E RID: 62
		private int FeedBackByte;

		// Token: 0x0400003F RID: 63
		private int FeedBackIter;

		// Token: 0x04000040 RID: 64
		private bool m_disposed;

		// Token: 0x04000041 RID: 65
		private bool lastBlock;

		// Token: 0x04000042 RID: 66
		private RandomNumberGenerator _rng;
	}
}
