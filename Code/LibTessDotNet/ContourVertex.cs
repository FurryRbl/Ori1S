using System;

namespace LibTessDotNet
{
	// Token: 0x0200000E RID: 14
	public struct ContourVertex
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00006069 File Offset: 0x00004269
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.Position, this.Data);
		}

		// Token: 0x0400003E RID: 62
		public Vec3 Position;

		// Token: 0x0400003F RID: 63
		public object Data;
	}
}
