using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000023 RID: 35
	public class ARC4Managed : RC4, IDisposable, ICryptoTransform
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00009F34 File Offset: 0x00008134
		public ARC4Managed()
		{
			this.state = new byte[256];
			this.m_disposed = false;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00009F54 File Offset: 0x00008154
		~ARC4Managed()
		{
			this.Dispose(true);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00009F90 File Offset: 0x00008190
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.x = 0;
				this.y = 0;
				if (this.key != null)
				{
					Array.Clear(this.key, 0, this.key.Length);
					this.key = null;
				}
				Array.Clear(this.state, 0, this.state.Length);
				this.state = null;
				GC.SuppressFinalize(this);
				this.m_disposed = true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000A004 File Offset: 0x00008204
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000A018 File Offset: 0x00008218
		public override byte[] Key
		{
			get
			{
				return (byte[])this.key.Clone();
			}
			set
			{
				this.key = (byte[])value.Clone();
				this.KeySetup(this.key);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000A038 File Offset: 0x00008238
		public bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A03C File Offset: 0x0000823C
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A048 File Offset: 0x00008248
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this.CreateEncryptor();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000A058 File Offset: 0x00008258
		public override void GenerateIV()
		{
			this.IV = new byte[0];
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000A068 File Offset: 0x00008268
		public override void GenerateKey()
		{
			this.Key = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000A080 File Offset: 0x00008280
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000A084 File Offset: 0x00008284
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000A088 File Offset: 0x00008288
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000A08C File Offset: 0x0000828C
		private void KeySetup(byte[] key)
		{
			byte b = 0;
			byte b2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.state[i] = (byte)i;
			}
			this.x = 0;
			this.y = 0;
			for (int j = 0; j < 256; j++)
			{
				b2 = key[(int)b] + this.state[j] + b2;
				byte b3 = this.state[j];
				this.state[j] = this.state[(int)b2];
				this.state[(int)b2] = b3;
				b = (byte)((int)(b + 1) % key.Length);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000A120 File Offset: 0x00008320
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

		// Token: 0x06000187 RID: 391 RVA: 0x0000A18C File Offset: 0x0000838C
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			if (outputOffset > outputBuffer.Length - inputCount)
			{
				throw new ArgumentException("outputBuffer", Locale.GetText("Overflow"));
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000A1FC File Offset: 0x000083FC
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = 0; i < inputCount; i++)
			{
				this.x += 1;
				this.y = this.state[(int)this.x] + this.y;
				byte b = this.state[(int)this.x];
				this.state[(int)this.x] = this.state[(int)this.y];
				this.state[(int)this.y] = b;
				byte b2 = this.state[(int)this.x] + this.state[(int)this.y];
				outputBuffer[outputOffset + i] = (inputBuffer[inputOffset + i] ^ this.state[(int)b2]);
			}
			return inputCount;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x040000A9 RID: 169
		private byte[] key;

		// Token: 0x040000AA RID: 170
		private byte[] state;

		// Token: 0x040000AB RID: 171
		private byte x;

		// Token: 0x040000AC RID: 172
		private byte y;

		// Token: 0x040000AD RID: 173
		private bool m_disposed;
	}
}
