using System;
using System.Runtime.InteropServices;
using ManagedSteam.Utility;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000154 RID: 340
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MatchMakingKeyValuePair
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0000FC94 File Offset: 0x0000DE94
		public MatchMakingKeyValuePair(string key, string value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				if (StringHelper.GetByteCountUtf8(value) > 256)
				{
					throw new ArgumentOutOfRangeException("key", StringMap.GetString(ErrorCodes.StringIsToBig, new object[]
					{
						256
					}));
				}
				this.key = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0000FCF7 File Offset: 0x0000DEF7
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (StringHelper.GetByteCountUtf8(value) > 256)
				{
					throw new ArgumentOutOfRangeException("value", StringMap.GetString(ErrorCodes.StringIsToBig, new object[]
					{
						256
					}));
				}
				this.value = value;
			}
		}

		// Token: 0x0400060D RID: 1549
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		private string key;

		// Token: 0x0400060E RID: 1550
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		private string value;
	}
}
