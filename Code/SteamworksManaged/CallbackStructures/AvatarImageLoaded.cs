using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000144 RID: 324
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 20)]
	public struct AvatarImageLoaded
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0000FA68 File Offset: 0x0000DC68
		internal static AvatarImageLoaded Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<AvatarImageLoaded>(data, dataSize);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0000FA71 File Offset: 0x0000DC71
		public SteamID SteamID
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0000FA79 File Offset: 0x0000DC79
		public ImageHandle Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0000FA81 File Offset: 0x0000DC81
		public int Width
		{
			get
			{
				return this.wide;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0000FA89 File Offset: 0x0000DC89
		public int Height
		{
			get
			{
				return this.tall;
			}
		}

		// Token: 0x040005CB RID: 1483
		private SteamID steamID;

		// Token: 0x040005CC RID: 1484
		private ImageHandle image;

		// Token: 0x040005CD RID: 1485
		private int wide;

		// Token: 0x040005CE RID: 1486
		private int tall;
	}
}
