using System;
using System.IO;

namespace Mono.Audio
{
	// Token: 0x020002AF RID: 687
	internal class WavData : AudioData
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x00041520 File Offset: 0x0003F720
		public WavData(Stream data)
		{
			this.stream = data;
			byte[] array = new byte[44];
			int num = this.stream.Read(array, 0, 44);
			if (num != 44 || array[0] != 82 || array[1] != 73 || array[2] != 70 || array[3] != 70 || array[8] != 87 || array[9] != 65 || array[10] != 86 || array[11] != 69)
			{
				throw new Exception("incorrect format" + num);
			}
			if (array[12] != 102 || array[13] != 109 || array[14] != 116 || array[15] != 32)
			{
				throw new Exception("incorrect format (fmt)");
			}
			int num2 = (int)array[16];
			num2 |= (int)array[17] << 8;
			num2 |= (int)array[18] << 16;
			num2 |= (int)array[19] << 24;
			int num3 = (int)array[20] | (int)array[21] << 8;
			if (num3 != 1)
			{
				throw new Exception("incorrect format (not PCM)");
			}
			this.channels = (short)((int)array[22] | (int)array[23] << 8);
			this.sample_rate = (int)array[24];
			this.sample_rate |= (int)array[25] << 8;
			this.sample_rate |= (int)array[26] << 16;
			this.sample_rate |= (int)array[27] << 24;
			int num4 = (int)array[28];
			num4 |= (int)array[29] << 8;
			num4 |= (int)array[30] << 16;
			num4 |= (int)array[31] << 24;
			int num5 = (int)array[34] | (int)array[35] << 8;
			if (array[36] != 100 || array[37] != 97 || array[38] != 116 || array[39] != 97)
			{
				throw new Exception("incorrect format (data)");
			}
			int num6 = (int)array[40];
			num6 |= (int)array[41] << 8;
			num6 |= (int)array[42] << 16;
			num6 |= (int)array[43] << 24;
			this.data_len = num6;
			int num7 = num5;
			if (num7 != 8)
			{
				if (num7 != 16)
				{
					throw new Exception("bits per sample");
				}
				this.frame_divider = 2;
				this.format = AudioFormat.S16_LE;
			}
			else
			{
				this.frame_divider = 1;
				this.format = AudioFormat.U8;
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0004177C File Offset: 0x0003F97C
		public override void Play(AudioDevice dev)
		{
			int num = this.data_len;
			byte[] array = new byte[4096];
			this.stream.Position = 0L;
			int num2;
			while (!this.IsStopped && num >= 0 && (num2 = this.stream.Read(array, 0, Math.Min(array.Length, num))) > 0)
			{
				dev.PlaySample(array, num2 / (int)this.frame_divider);
				num -= num2;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000417F4 File Offset: 0x0003F9F4
		public override int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000417FC File Offset: 0x0003F9FC
		public override int Rate
		{
			get
			{
				return this.sample_rate;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00041804 File Offset: 0x0003FA04
		public override AudioFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x04000F2A RID: 3882
		private Stream stream;

		// Token: 0x04000F2B RID: 3883
		private short channels;

		// Token: 0x04000F2C RID: 3884
		private ushort frame_divider;

		// Token: 0x04000F2D RID: 3885
		private int sample_rate;

		// Token: 0x04000F2E RID: 3886
		private int data_len;

		// Token: 0x04000F2F RID: 3887
		private AudioFormat format;
	}
}
