using System;
using System.IO;

namespace Mono.Audio
{
	// Token: 0x020002B0 RID: 688
	internal class AuData : AudioData
	{
		// Token: 0x060017D7 RID: 6103 RVA: 0x0004180C File Offset: 0x0003FA0C
		public AuData(Stream data)
		{
			this.stream = data;
			byte[] array = new byte[24];
			int num = this.stream.Read(array, 0, 24);
			if (num != 24 || array[0] != 46 || array[1] != 115 || array[2] != 110 || array[3] != 100)
			{
				throw new Exception("incorrect format" + num);
			}
			int num2 = (int)array[7];
			num2 |= (int)array[6] << 8;
			num2 |= (int)array[5] << 16;
			num2 |= (int)array[4] << 24;
			this.data_len = (int)array[11];
			this.data_len |= (int)array[10] << 8;
			this.data_len |= (int)array[9] << 16;
			this.data_len |= (int)array[8] << 24;
			int num3 = (int)array[15];
			num3 |= (int)array[14] << 8;
			num3 |= (int)array[13] << 16;
			num3 |= (int)array[12] << 24;
			this.sample_rate = (int)array[19];
			this.sample_rate |= (int)array[18] << 8;
			this.sample_rate |= (int)array[17] << 16;
			this.sample_rate |= (int)array[16] << 24;
			int num4 = (int)array[23];
			num4 |= (int)array[22] << 8;
			num4 |= (int)array[21] << 16;
			num4 |= (int)array[20] << 24;
			this.channels = (short)num4;
			if (num2 < 24 || (num4 != 1 && num4 != 2))
			{
				throw new Exception("incorrect format offset" + num2);
			}
			if (num2 != 24)
			{
				for (int i = 24; i < num2; i++)
				{
					this.stream.ReadByte();
				}
			}
			int num5 = num3;
			if (num5 != 1)
			{
				throw new Exception("incorrect format encoding" + num3);
			}
			this.frame_divider = 1;
			this.format = AudioFormat.MU_LAW;
			if (this.data_len == -1)
			{
				this.data_len = (int)this.stream.Length - num2;
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00041A34 File Offset: 0x0003FC34
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

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00041AAC File Offset: 0x0003FCAC
		public override int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		public override int Rate
		{
			get
			{
				return this.sample_rate;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00041ABC File Offset: 0x0003FCBC
		public override AudioFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x04000F30 RID: 3888
		private Stream stream;

		// Token: 0x04000F31 RID: 3889
		private short channels;

		// Token: 0x04000F32 RID: 3890
		private ushort frame_divider;

		// Token: 0x04000F33 RID: 3891
		private int sample_rate;

		// Token: 0x04000F34 RID: 3892
		private int data_len;

		// Token: 0x04000F35 RID: 3893
		private AudioFormat format;
	}
}
