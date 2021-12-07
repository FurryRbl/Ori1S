using System;
using System.Runtime.InteropServices;
using ManagedSteam.Utility;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000018 RID: 24
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ServerNetworkAddress : IComparable<ServerNetworkAddress>
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000034F5 File Offset: 0x000016F5
		internal static ServerNetworkAddress Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<ServerNetworkAddress>(data, dataSize);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000034FE File Offset: 0x000016FE
		public ServerNetworkAddress(uint ip, ushort queryPort, ushort connectionPort)
		{
			this.ip = ip;
			this.queryPort = queryPort;
			this.connectionPort = connectionPort;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003515 File Offset: 0x00001715
		public void Init(uint ip, ushort queryPort, ushort connectionPort)
		{
			this.ip = ip;
			this.queryPort = queryPort;
			this.connectionPort = connectionPort;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000352C File Offset: 0x0000172C
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003534 File Offset: 0x00001734
		public ushort ConnectionPort
		{
			get
			{
				return this.connectionPort;
			}
			set
			{
				this.connectionPort = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000353D File Offset: 0x0000173D
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003545 File Offset: 0x00001745
		public ushort QueryPort
		{
			get
			{
				return this.queryPort;
			}
			set
			{
				this.queryPort = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000354E File Offset: 0x0000174E
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003556 File Offset: 0x00001756
		public uint IP
		{
			get
			{
				return this.ip;
			}
			set
			{
				this.ip = value;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003560 File Offset: 0x00001760
		public string GetConnectionAddressString()
		{
			string result;
			using (NativeBuffer nativeBuffer = NativeBuffer.CopyToNative<ServerNetworkAddress>(this))
			{
				IntPtr pointer = NativeMethods.MatchmakingServerNetworkAddress_GetConnectionString(nativeBuffer.UnmanagedMemory);
				result = NativeHelpers.ToStringAnsi(pointer);
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000035AC File Offset: 0x000017AC
		public string GetQueryAddressString()
		{
			string result;
			using (NativeBuffer nativeBuffer = NativeBuffer.CopyToNative<ServerNetworkAddress>(this))
			{
				IntPtr pointer = NativeMethods.MatchmakingServerNetworkAddress_GetQueryString(nativeBuffer.UnmanagedMemory);
				result = NativeHelpers.ToStringAnsi(pointer);
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000035F8 File Offset: 0x000017F8
		public int CompareTo(ServerNetworkAddress other)
		{
			if (this.ip < other.ip || (this.ip == other.ip && this.queryPort < other.queryPort))
			{
				return -1;
			}
			if (this.ip == other.ip && this.queryPort == other.queryPort)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0400005F RID: 95
		private ushort connectionPort;

		// Token: 0x04000060 RID: 96
		private ushort queryPort;

		// Token: 0x04000061 RID: 97
		private uint ip;
	}
}
