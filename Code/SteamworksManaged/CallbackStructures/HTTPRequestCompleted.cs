using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000B9 RID: 185
	public struct HTTPRequestCompleted
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x00009148 File Offset: 0x00007348
		internal static HTTPRequestCompleted Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<HTTPRequestCompleted>(data, dataSize);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00009151 File Offset: 0x00007351
		public HTTPRequestHandle Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00009159 File Offset: 0x00007359
		public ulong ContextValue
		{
			get
			{
				return this.contextValue;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00009161 File Offset: 0x00007361
		public bool RequestSuccessful
		{
			get
			{
				return this.requestSuccessful;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00009169 File Offset: 0x00007369
		public HTTPStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x0400033D RID: 829
		private HTTPRequestHandle request;

		// Token: 0x0400033E RID: 830
		private ulong contextValue;

		// Token: 0x0400033F RID: 831
		private bool requestSuccessful;

		// Token: 0x04000340 RID: 832
		private HTTPStatusCode statusCode;
	}
}
