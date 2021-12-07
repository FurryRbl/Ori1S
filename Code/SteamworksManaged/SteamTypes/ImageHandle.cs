using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000CB RID: 203
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ImageHandle : IEquatable<ImageHandle>
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x0000948C File Offset: 0x0000768C
		public ImageHandle(int value)
		{
			this.handle = value;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00009495 File Offset: 0x00007695
		public int AsInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000949D File Offset: 0x0000769D
		public bool IsValid
		{
			get
			{
				return this.handle != -1 && this.handle != 0;
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000094B6 File Offset: 0x000076B6
		public static bool operator ==(ImageHandle x, ImageHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000094C8 File Offset: 0x000076C8
		public static bool operator !=(ImageHandle x, ImageHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000094DD File Offset: 0x000076DD
		public bool Equals(ImageHandle other)
		{
			return this == other;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000094EB File Offset: 0x000076EB
		public override bool Equals(object obj)
		{
			return obj is ImageHandle && this == (ImageHandle)obj;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00009508 File Offset: 0x00007708
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00009515 File Offset: 0x00007715
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000378 RID: 888
		public static readonly ImageHandle Invalid = new ImageHandle(0);

		// Token: 0x04000379 RID: 889
		public static readonly ImageHandle NotLoaded = new ImageHandle(-1);

		// Token: 0x0400037A RID: 890
		private int handle;
	}
}
