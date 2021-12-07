using System;

namespace Mono.Audio
{
	// Token: 0x020002AE RID: 686
	internal abstract class AudioData
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060017CB RID: 6091
		public abstract int Channels { get; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060017CC RID: 6092
		public abstract int Rate { get; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060017CD RID: 6093
		public abstract AudioFormat Format { get; }

		// Token: 0x060017CE RID: 6094 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public virtual void Setup(AudioDevice dev)
		{
			dev.SetFormat(this.Format, this.Channels, this.Rate);
		}

		// Token: 0x060017CF RID: 6095
		public abstract void Play(AudioDevice dev);

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0004150C File Offset: 0x0003F70C
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00041514 File Offset: 0x0003F714
		public virtual bool IsStopped
		{
			get
			{
				return this.stopped;
			}
			set
			{
				this.stopped = value;
			}
		}

		// Token: 0x04000F28 RID: 3880
		protected const int buffer_size = 4096;

		// Token: 0x04000F29 RID: 3881
		private bool stopped;
	}
}
