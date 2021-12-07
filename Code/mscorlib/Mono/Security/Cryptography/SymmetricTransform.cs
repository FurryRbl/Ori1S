using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000BF RID: 191
	internal abstract class SymmetricTransform : IDisposable, ICryptoTransform
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002D344 File Offset: 0x0002B544
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

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002D470 File Offset: 0x0002B670
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002D480 File Offset: 0x0002B680
		~SymmetricTransform()
		{
			this.Dispose(false);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002D4BC File Offset: 0x0002B6BC
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

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0002D514 File Offset: 0x0002B714
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002D518 File Offset: 0x0002B718
		public virtual bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002D51C File Offset: 0x0002B71C
		public virtual int InputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0002D524 File Offset: 0x0002B724
		public virtual int OutputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002D52C File Offset: 0x0002B72C
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

		// Token: 0x06000AAE RID: 2734
		protected abstract void ECB(byte[] input, byte[] output);

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002D5CC File Offset: 0x0002B7CC
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

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002D698 File Offset: 0x0002B898
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

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002D7F8 File Offset: 0x0002B9F8
		protected virtual void OFB(byte[] input, byte[] output)
		{
			throw new CryptographicException("OFB isn't supported by the framework");
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002D804 File Offset: 0x0002BA04
		protected virtual void CTS(byte[] input, byte[] output)
		{
			throw new CryptographicException("CTS isn't supported by the framework");
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002D810 File Offset: 0x0002BA10
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

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002D87C File Offset: 0x0002BA7C
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002D9A4 File Offset: 0x0002BBA4
		private bool KeepLastBlock
		{
			get
			{
				return !this.encrypt && this.algo.Padding != PaddingMode.None && this.algo.Padding != PaddingMode.Zeros;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
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

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002DB18 File Offset: 0x0002BD18
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

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002DB58 File Offset: 0x0002BD58
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

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
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

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002DDD4 File Offset: 0x0002BFD4
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

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002E010 File Offset: 0x0002C210
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

		// Token: 0x04000284 RID: 644
		protected SymmetricAlgorithm algo;

		// Token: 0x04000285 RID: 645
		protected bool encrypt;

		// Token: 0x04000286 RID: 646
		private int BlockSizeByte;

		// Token: 0x04000287 RID: 647
		private byte[] temp;

		// Token: 0x04000288 RID: 648
		private byte[] temp2;

		// Token: 0x04000289 RID: 649
		private byte[] workBuff;

		// Token: 0x0400028A RID: 650
		private byte[] workout;

		// Token: 0x0400028B RID: 651
		private int FeedBackByte;

		// Token: 0x0400028C RID: 652
		private int FeedBackIter;

		// Token: 0x0400028D RID: 653
		private bool m_disposed;

		// Token: 0x0400028E RID: 654
		private bool lastBlock;

		// Token: 0x0400028F RID: 655
		private RandomNumberGenerator _rng;
	}
}
