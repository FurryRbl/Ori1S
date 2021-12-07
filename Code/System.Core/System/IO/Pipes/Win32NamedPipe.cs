using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000082 RID: 130
	internal abstract class Win32NamedPipe : IPipe
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001A04C File Offset: 0x0001824C
		public string Name
		{
			get
			{
				if (this.name_cache != null)
				{
					return this.name_cache;
				}
				byte[] array = new byte[200];
				int num;
				int num2;
				int num3;
				int num4;
				while (Win32Marshal.GetNamedPipeHandleState(this.Handle, out num, out num2, out num3, out num4, array, array.Length))
				{
					if (array[array.Length - 1] == 0)
					{
						this.name_cache = Encoding.Default.GetString(array);
						return this.name_cache;
					}
					array = new byte[array.Length * 10];
				}
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new Win32Exception(lastWin32Error);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000630 RID: 1584
		public abstract SafePipeHandle Handle { get; }

		// Token: 0x06000631 RID: 1585 RVA: 0x0001A0E4 File Offset: 0x000182E4
		public void WaitForPipeDrain()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001B4 RID: 436
		private string name_cache;
	}
}
