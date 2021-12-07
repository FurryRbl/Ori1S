using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000029 RID: 41
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct IPCFailure
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00003A5C File Offset: 0x00001C5C
		internal static IPCFailure Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<IPCFailure>(data, dataSize);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003A65 File Offset: 0x00001C65
		public IPCFailure.EFailureType FailureType
		{
			get
			{
				return this.failureType;
			}
		}

		// Token: 0x040000D6 RID: 214
		private IPCFailure.EFailureType failureType;

		// Token: 0x0200002A RID: 42
		public enum EFailureType : byte
		{
			// Token: 0x040000D8 RID: 216
			FlushedCalbackQueue,
			// Token: 0x040000D9 RID: 217
			PipeFail
		}
	}
}
