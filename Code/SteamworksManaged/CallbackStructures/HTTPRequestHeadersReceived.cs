using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000BA RID: 186
	public struct HTTPRequestHeadersReceived
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x00009171 File Offset: 0x00007371
		internal static HTTPRequestHeadersReceived Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<HTTPRequestHeadersReceived>(data, dataSize);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000917A File Offset: 0x0000737A
		public HTTPRequestHandle Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00009182 File Offset: 0x00007382
		public ulong ContextValue
		{
			get
			{
				return this.contextValue;
			}
		}

		// Token: 0x04000341 RID: 833
		private HTTPRequestHandle request;

		// Token: 0x04000342 RID: 834
		private ulong contextValue;
	}
}
