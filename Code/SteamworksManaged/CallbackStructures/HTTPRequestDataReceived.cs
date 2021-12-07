using System;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x020000BB RID: 187
	public struct HTTPRequestDataReceived
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x0000918A File Offset: 0x0000738A
		internal static HTTPRequestDataReceived Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<HTTPRequestDataReceived>(data, dataSize);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00009193 File Offset: 0x00007393
		public HTTPRequestHandle Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000919B File Offset: 0x0000739B
		public ulong ContextValue
		{
			get
			{
				return this.contextValue;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000091A3 File Offset: 0x000073A3
		public uint Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x000091AB File Offset: 0x000073AB
		public uint BytesReceived
		{
			get
			{
				return this.bytesReceived;
			}
		}

		// Token: 0x04000343 RID: 835
		private HTTPRequestHandle request;

		// Token: 0x04000344 RID: 836
		private ulong contextValue;

		// Token: 0x04000345 RID: 837
		private uint offset;

		// Token: 0x04000346 RID: 838
		private uint bytesReceived;
	}
}
