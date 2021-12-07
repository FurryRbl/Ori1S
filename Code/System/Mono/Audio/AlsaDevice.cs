using System;
using System.Runtime.InteropServices;

namespace Mono.Audio
{
	// Token: 0x020002B3 RID: 691
	internal class AlsaDevice : AudioDevice, IDisposable
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x00041B48 File Offset: 0x0003FD48
		public AlsaDevice(string name)
		{
			if (name == null)
			{
				name = "default";
			}
			int num = AlsaDevice.snd_pcm_open(ref this.handle, name, 0, 0);
			if (num < 0)
			{
				throw new Exception("no open " + num);
			}
		}

		// Token: 0x060017E3 RID: 6115
		[DllImport("libasound.so.2")]
		private static extern int snd_pcm_open(ref IntPtr handle, string pcm_name, int stream, int mode);

		// Token: 0x060017E4 RID: 6116
		[DllImport("libasound.so.2")]
		private static extern int snd_pcm_close(IntPtr handle);

		// Token: 0x060017E5 RID: 6117
		[DllImport("libasound.so.2")]
		private static extern int snd_pcm_drain(IntPtr handle);

		// Token: 0x060017E6 RID: 6118
		[DllImport("libasound.so.2")]
		private static extern int snd_pcm_writei(IntPtr handle, byte[] buf, int size);

		// Token: 0x060017E7 RID: 6119
		[DllImport("libasound.so.2")]
		private static extern int snd_pcm_set_params(IntPtr handle, int format, int access, int channels, int rate, int soft_resample, int latency);

		// Token: 0x060017E8 RID: 6120 RVA: 0x00041B94 File Offset: 0x0003FD94
		~AlsaDevice()
		{
			this.Dispose(false);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00041BD0 File Offset: 0x0003FDD0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00041BE0 File Offset: 0x0003FDE0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			if (this.handle != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_close(this.handle);
			}
			this.handle = IntPtr.Zero;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00041C20 File Offset: 0x0003FE20
		public override bool SetFormat(AudioFormat format, int channels, int rate)
		{
			int num = AlsaDevice.snd_pcm_set_params(this.handle, (int)format, 3, channels, rate, 1, 500000);
			return num == 0;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00041C48 File Offset: 0x0003FE48
		public override int PlaySample(byte[] buffer, int num_frames)
		{
			return AlsaDevice.snd_pcm_writei(this.handle, buffer, num_frames);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00041C64 File Offset: 0x0003FE64
		public override void Wait()
		{
			AlsaDevice.snd_pcm_drain(this.handle);
		}

		// Token: 0x04000F50 RID: 3920
		private IntPtr handle;
	}
}
